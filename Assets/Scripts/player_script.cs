using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using UnityEngine.InputSystem;

public class player_script : MonoBehaviour, IConfigs
{
    public Rigidbody who;
    public Camera cam;
    public float camera_sensitivity = 53;
    public Image Vignette;
    public AudioSource EwICantMusic;
    public Slider energyBar;
    public Transform hand;
    private Item item;
    private float cam_x = 0;
    private bool Alive = true;
    private float timer = 0;
    private float walkSpeed = 4;
    private float runSpeed = 8;
    private float jumpSpeed = 4;
    private bool running = false;
    private float energy = 2;
    private float walkCost = 0.5F;
    private float runCost = 1.5F;
    private bool sprintToggle = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vignette.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Alive)
        {
            float speed = new float(); // Yes I just made a new float cry about it
            RunEngine(ref speed);
            Movement(speed);
            // Energy usage
            if (running)
            {
                energy -= Time.deltaTime * runCost;
            }
            else if (InputSystem.actions.FindAction("Move").IsPressed())
            {
                energy -= Time.deltaTime * walkCost;
                
            }
            energy += Time.deltaTime;
            if (energy > 2)
            {
                energy = 2;
            }
            else if (energy < 0)
            {
                energy = 0;
            }
            energyBar.value = energy / 2;
            // Holding items
            ItemHandler();
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 2;
            }
            Vignette.color = new Color(1, 0.5F, 0.5F, timer * 0.5F);
        }
        if (timer == 2)
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (EwICantMusic.isPlaying == false)
        {
            EwICantMusic.Play();
        }
    }

    private void ItemHandler()
    {
        // Do you want to pick it up?
        if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
        {
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit);
            if (hit.collider.tag == "is_item")
            {
                item = hit.collider.GetComponent<Item>();
                hit.collider.enabled = false;
            }
        }
        // If you have an item hold it
        if (item != null)
        {
            item.transform.position = hand.position;
            item.transform.rotation = hand.rotation;
            item.transform.localScale = hand.localScale;
        }
    }

    private void Movement(float speed)
    {
        if (IsGrounded() && InputSystem.actions.FindAction("Jump").IsPressed())
        {
            who.velocity = new Vector3(0, jumpSpeed, 0);
        }
        float y = who.velocity.y;
        who.velocity = transform.right * InputSystem.actions.FindAction("Move").ReadValue<Vector2>().y * speed + transform.forward * -1 * InputSystem.actions.FindAction("Move").ReadValue<Vector2>().x * speed;
        who.velocity = new Vector3(who.velocity.x, y, who.velocity.z);
        //who.transform.Translate(new Vector3(InputSystem.actions.FindAction("Move").ReadValue<Vector2>().y * speed * Time.deltaTime, 0, 0), Space.Self);
        //who.transform.Translate(new Vector3(0, 0, InputSystem.actions.FindAction("Move").ReadValue<Vector2>().x * speed * -1 * Time.deltaTime), Space.Self);
        who.transform.Rotate(new Vector3(0, 1, 0), InputSystem.actions.FindAction("Look").ReadValue<Vector2>().x * 0.1F * camera_sensitivity / 10);
        cam_x -= InputSystem.actions.FindAction("Look").ReadValue<Vector2>().y * 0.1F * camera_sensitivity / 10;
        cam_x = Math.Clamp(cam_x, -90, 90);
        cam.transform.localEulerAngles = new Vector3(cam_x, 90, 0);
    }

    private void RunEngine(ref float speed)
    {
        // Can you run (move key down, has energy)? Also means letting go of move key aids in toggle sprint :)
        if (InputSystem.actions.FindAction("Move").ReadValue<Vector2>().magnitude > 0 && (energy > 1 || (running && energy > 0)))
        {
            if (sprintToggle) //TODO toggle sprint toggle
            {
                if (InputSystem.actions.FindAction("Sprint").WasPressedThisFrame())
                {
                    if (running)
                    {
                        running = false;
                    }
                    else
                    {
                        running  = true;
                    }
                }
            }
            else
            {
                if (InputSystem.actions.FindAction("Sprint").IsPressed())
                {
                    running = true;
                }
                else
                {
                    running = false;
                }
            }
        }
        else
        {
            running = false;
        }
        // If running -> speed
        if (running)
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, new Vector3(0, -1, 0), 1.1F);
    }

    public void Death()
    {
        Alive = false;
    }

    //Configs boi
    public void LoadConfigs(Configs configs)
    {
        this.camera_sensitivity = configs.turnRateConfig;
        this.cam.fieldOfView = configs.fovConfig / this.cam.aspect; //FOV is vertical, div horizontal by width/height or mul by height/width
        this.sprintToggle = configs.sprintToggle;
    }

    public void SaveConfigs(ref Configs configs)
    {
        configs.turnRateConfig = this.camera_sensitivity;
        configs.fovConfig = this.cam.fieldOfView * this.cam.aspect; //FOV is measured vertically so i multiply by width/height
    }
}

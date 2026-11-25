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
    private float cam_x = 0;
    private bool Alive = true;
    private float timer = 0;
    private float walkSpeed = 4;
    private float runSpeed = 8;
    private float jumpSpeed = 4;
    private bool running = false;
    private float energy = 2;
    private float walkCost = 1;
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
            Debug.Log(energy);
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

    private void Movement(float speed)
    {
        if (IsGrounded() && Input.GetKey(KeyCode.Space))
        {
            who.velocity = new Vector3(0, jumpSpeed, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            who.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.A))
        {
            who.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            who.transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            who.transform.Translate(new Vector3(0, 0, -1 * speed * Time.deltaTime), Space.Self);
        }
        who.transform.Rotate(new Vector3(0, 1, 0), UnityEngine.Input.GetAxis("Mouse X") * camera_sensitivity / 10);
        cam_x -= UnityEngine.Input.GetAxis("Mouse Y") * camera_sensitivity / 10;
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
                if (InputSystem.actions.FindAction("Sprint").IsPressed() || running)
                {
                    running = true;
                }
                else
                {
                    running = false;
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

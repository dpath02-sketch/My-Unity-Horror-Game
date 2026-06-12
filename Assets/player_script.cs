using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class player_script : MonoBehaviour
{
    public Rigidbody who;
    public Camera cam;
    public float camera_sensitivity = 5.3F;
    private float cam_x = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && UnityEngine.Input.GetKey("space"))
        {
            who.velocity = new Vector3(0, 4F, 0);
        }
        if (UnityEngine.Input.GetKey("w"))
        {
            who.transform.Translate(new Vector3(4 * Time.deltaTime, 0, 0), Space.Self);
        }
        if (UnityEngine.Input.GetKey("a"))
        {
            who.transform.Translate(new Vector3(0, 0, 4 * Time.deltaTime), Space.Self);
        }
        if (UnityEngine.Input.GetKey("s"))
        {
            who.transform.Translate(new Vector3(-4 * Time.deltaTime, 0, 0), Space.Self);
        }
        if (UnityEngine.Input.GetKey("d"))
        {
            who.transform.Translate(new Vector3(0, 0, -4 * Time.deltaTime), Space.Self);
        }
        who.transform.Rotate(new Vector3(0, 1, 0), UnityEngine.Input.GetAxis("Mouse X") * camera_sensitivity);
        cam_x -= UnityEngine.Input.GetAxis("Mouse Y") * camera_sensitivity;
        cam_x = Math.Clamp(cam_x, -90, 90);
        cam.transform.localEulerAngles = new Vector3(cam_x, 90, 0);
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, new Vector3(0, -1, 0), 1.1F);
    }
}

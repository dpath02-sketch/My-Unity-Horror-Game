using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class enemy1 : MonoBehaviour
{
    private int rotations = 0;
    private float wait = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (wait < 0)
        {
            if (rotations == 4)
            {
                Destroy(gameObject);
            }
            transform.Translate(new Vector3(Time.deltaTime * 8, 0, 0));
    ;       if (MathF.Abs(transform.position.x) > 7.2F || MathF.Abs(transform.position.z) > 7.2F)
            {
                transform.position = new Vector3(transform.position.x / MathF.Abs(transform.position.x) * 7.2F, transform.position.y, transform.position.z / MathF.Abs(transform.position.z) * 7.2F);
                transform.Rotate(new Vector3(0, 1, 0), 90F);
                rotations += 1;
            }
        }
        else
        {
            wait -= Time.deltaTime;
        }
        // at least it works but charging at bro would be kool
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "is_player")
        {
            //TODO death. therefor main menu, death screen, etc
            //TODO also actualy model. with the idea of ramming try a 4 legged creature with eyes on the front
            Debug.Log("Dead");
        }
    }
}

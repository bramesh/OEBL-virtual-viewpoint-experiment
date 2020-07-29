using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CursorMovement : MonoBehaviour
{

    // Enable if using mouse control
    private Vector3 mousePosition;
    private Vector3 direction = new Vector3();
    public float controlFactor = 0.001f;
    bool firstClick = true;

    public Rigidbody rb;
    private float startTime;
    bool timerStarted = false;

    //Collecting distances
    private float x_location = 0f;
    private float z_location = 0f;

    public GameManager gameManager;

    public float controlForce = 500f;

    // Enable if using mouse control
    public bool trackpadMovement = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if( Input.GetKey("left"))
        {
            // Get the start time of movement
            if(!timerStarted)
            {
                timerStarted = true;
                gameManager.StartTimer();
            }
            rb.AddForce(-controlForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange); // Force control
            //transform.Translate(-controlForce, 0, 0); // Translation control
        }

        if( Input.GetKey("right"))
        {
            if(!timerStarted)
            {
                timerStarted = true;
                gameManager.StartTimer();
            }
            rb.AddForce(controlForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            //transform.Translate(controlForce, 0, 0);
        }

        if( Input.GetKey("up"))
        {
            if(!timerStarted)
            {
                timerStarted = true;
                gameManager.StartTimer();
            }
            rb.AddForce(0, 0, controlForce * Time.deltaTime, ForceMode.VelocityChange);
            //transform.Translate(0, 0, controlForce);
        }

        if( Input.GetKey("down"))
        {
            if(!timerStarted)
            {
                timerStarted = true;
                gameManager.StartTimer();
            }
            rb.AddForce(0, 0, -controlForce * Time.deltaTime, ForceMode.VelocityChange);
            //transform.Translate(0, 0, -controlForce);
        }
        */

        direction.x = 0f;
        direction.y = 0f;
        direction.z = 0f;

        if (Input.GetKey("left"))
        {
            if (!timerStarted)
            {
                timerStarted = true;
                gameManager.StartTimer();
            }

            direction.x += -1f;
        }

        if (Input.GetKey("right"))
        {
            if (!timerStarted)
            {
                timerStarted = true;
                gameManager.StartTimer();
            }

            direction.x += 1f;
        }

        if (Input.GetKey("up"))
        {
            if (!timerStarted)
            {
                timerStarted = true;
                gameManager.StartTimer();
            }

            direction.z += 1f;
        }

        if (Input.GetKey("down"))
        {
            if (!timerStarted)
            {
                timerStarted = true;
                gameManager.StartTimer();
            }

            direction.z += -1f;
        }

        if (direction.magnitude > 0)
        {
            rb.AddForce(controlForce*direction.x / direction.magnitude, 0, controlForce*direction.z / direction.magnitude, ForceMode.VelocityChange);
        }

        if (trackpadMovement)
        {
            // If the left button is clicked
            if (Input.GetMouseButton(0))
            {
                // Get the start time of movement
                if (!timerStarted)
                {
                    timerStarted = true;
                    gameManager.StartTimer();
                }

                if (firstClick)
                {
                    mousePosition = Input.mousePosition;
                    firstClick = false;
                }
                else
                {
                    direction = Input.mousePosition - mousePosition;
                    //rb.AddForce(direction.x * 10, 0, direction.y * 10);
                    transform.Translate(direction.x * controlFactor, 0, direction.y * controlFactor);
                    mousePosition = Input.mousePosition;
                }

            }
            else
            {
                firstClick = true;
            }
        }
        

        if (rb.position.y < 0)
        {
            gameManager.Restart();
        }

        // Add position
        gameManager.AddDistance(x_location, z_location, rb.position.x, rb.position.z);
        x_location = rb.position.x;
        z_location = rb.position.z;
    }
}

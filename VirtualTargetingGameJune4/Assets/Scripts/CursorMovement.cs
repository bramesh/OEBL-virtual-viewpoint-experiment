using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;

public class CursorMovement : MonoBehaviour
{
    // Vector for storing force data
    private Vector3 force;
    public float controlForce = 3f;
    public float accelerationCoef = 0.4f;
    public float maxSpeed = 7f;

    public Rigidbody rb;
    private float startTime;
    bool timerStarted = false;

    //Collecting distances
    private float x_location = 0f;
    private float z_location = 0f;

    // For single keystroke movement
    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool upPressed = false;
    private bool downPressed = false;
    private bool[] keysLast = new bool[] { false, false, false, false };
    private bool[] keysCurrent = new bool[] { false, false, false, false };

    public GameManager gameManager;

    // Enable if using mouse control
    public bool trackpadMovement = false;

    // Enable if using mouse control
    public float controlFactor = 0.001f;
    private Vector3 mousePosition;
    private Vector3 direction = new Vector3();
    bool firstClick = true;

    void Start()
    {
        GlobalControl.Instance.loadTime = Time.time;
        GlobalControl.Instance.trajectory = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
 
        direction.x = 0f;
        direction.y = 0f;
        direction.z = 0f;

        leftPressed = Input.GetKey("left");
        rightPressed = Input.GetKey("right");
        upPressed = Input.GetKey("up");
        downPressed = Input.GetKey("down");
        keysCurrent[0] = leftPressed;
        keysCurrent[1] = rightPressed;
        keysCurrent[2] = upPressed;
        keysCurrent[3] = downPressed;

        for (int i = 0; i < 4; i++)
        {
            if (keysCurrent[i] && !keysLast[i])
            {
                if (!timerStarted)
                {
                    timerStarted = true;
                    gameManager.StartTimer();
                }
                if (i == 0)
                {
                    rb.AddForce(-controlForce, 0, 0, ForceMode.VelocityChange);
                    GlobalControl.Instance.trajectory += " L ";
                    GlobalControl.Instance.trajectory += Time.time;
                } else if (i == 1)
                {
                    rb.AddForce(controlForce, 0, 0, ForceMode.VelocityChange);
                    GlobalControl.Instance.trajectory += " R ";
                    GlobalControl.Instance.trajectory += Time.time;
                } else if (i == 2)
                {
                    rb.AddForce(0, 0, controlForce, ForceMode.VelocityChange);
                    GlobalControl.Instance.trajectory += " U ";
                    GlobalControl.Instance.trajectory += Time.time;
                } else
                {
                    rb.AddForce(0, 0, -controlForce, ForceMode.VelocityChange);
                    GlobalControl.Instance.trajectory += " D ";
                    GlobalControl.Instance.trajectory += Time.time;
                }
                break;
            }
        }

        keysLast[0] = keysCurrent[0];
        keysLast[1] = keysCurrent[1];
        keysLast[2] = keysCurrent[2];
        keysLast[3] = keysCurrent[3];

        /*
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
            force.x = accelerationCoef * controlForce * direction.x / direction.magnitude;
            force.y = 0f;
            force.z = accelerationCoef * controlForce * direction.z / direction.magnitude;

            if (rb.velocity.magnitude > 0.1f)
            {
                force = force * rb.velocity.magnitude;
            }

            if (rb.velocity.magnitude > maxSpeed)
            {
                force = maxSpeed * force / force.magnitude;
            }

            rb.AddForce(force.x, force.y, force.z, ForceMode.VelocityChange);
            //tansform.Translate(controlFactor * direction.x / direction.magnitude, 0, controlFactor * direction.z / direction.magnitude);
        }
        */

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

        gameManager.AddDistance(x_location, z_location, rb.position.x, rb.position.z);
        x_location = rb.position.x;
        z_location = rb.position.z;
        
    }
}

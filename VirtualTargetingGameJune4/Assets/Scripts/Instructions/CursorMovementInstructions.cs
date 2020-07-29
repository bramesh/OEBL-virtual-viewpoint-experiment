using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorMovementInstructions : MonoBehaviour
{

    // Enable if using mouse control
    private Vector3 mousePosition;
    private Vector3 direction;

    public float controlFactor = 0.001f;
    bool firstClick = true;

    public Rigidbody rb;

    public float controlForce = 500f;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("left"))
        {
            rb.AddForce(-controlForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange); // Force control
            //transform.Translate(-controlForce, 0, 0); // Translation control
        }

        if (Input.GetKey("right"))
        {
            rb.AddForce(controlForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            //transform.Translate(controlForce, 0, 0);
        }

        if (Input.GetKey("up"))
        {
            rb.AddForce(0, 0, controlForce * Time.deltaTime, ForceMode.VelocityChange);
            //transform.Translate(0, 0, controlForce);
        }

        if (Input.GetKey("down"))
        {
            rb.AddForce(0, 0, -controlForce * Time.deltaTime, ForceMode.VelocityChange);
            //transform.Translate(0, 0, -controlForce);
        }

        // Enable if using mouse control

        // If the left button is clicked
        if (Input.GetMouseButton(0))
        {
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

        if (rb.position.y < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
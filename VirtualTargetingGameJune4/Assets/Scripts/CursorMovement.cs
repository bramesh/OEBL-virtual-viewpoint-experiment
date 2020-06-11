using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{

    // Enable if using mouse control
    private Vector3 mousePosition;
    private Vector3 direction;
    public float controlFactor = 0.001f;
    bool firstClick = true;

    public Rigidbody rb;
    private float startTime;
    bool timerStarted = false;

    public GameManager gameManager;

    public float controlForce = 500f;


    // Update is called once per frame
    void FixedUpdate()
    {
      if (GlobalControl.Instance.cursorMovement){
        if( Input.GetKey("left")){
          // Get the start time of movement
          if(!timerStarted){
            timerStarted = true;
            gameManager.StartTimer();
          }
          rb.AddForce(-controlForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange); // Force control
          //transform.Translate(-controlForce, 0, 0); // Translation control
        }

        if( Input.GetKey("right")){
          if(!timerStarted){
            timerStarted = true;
            gameManager.StartTimer();
          }
          rb.AddForce(controlForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
          //transform.Translate(controlForce, 0, 0);
        }

        if( Input.GetKey("up")){
          if(!timerStarted){
            timerStarted = true;
            gameManager.StartTimer();
          }
          rb.AddForce(0, 0, controlForce * Time.deltaTime, ForceMode.VelocityChange);
          //transform.Translate(0, 0, controlForce);
        }

        if( Input.GetKey("down")){
          if(!timerStarted){
            timerStarted = true;
            gameManager.StartTimer();
          }
          rb.AddForce(0, 0, -controlForce * Time.deltaTime, ForceMode.VelocityChange);
          //transform.Translate(0, 0, -controlForce);
        }

        // Enable if using mouse control

        // If the left button is clicked
        if( Input.GetMouseButton(0) ){
          // Get the start time of movement
          if(!timerStarted){
            timerStarted = true;
            gameManager.StartTimer();
          }
          if( firstClick ){
            mousePosition = Input.mousePosition;
            firstClick = false;
          } else {
            direction = Input.mousePosition - mousePosition;
            //rb.AddForce(direction.x * 10, 0, direction.y * 10);
            transform.Translate(direction.x * controlFactor, 0, direction.y * controlFactor);
            mousePosition = Input.mousePosition;
          }
        } else {
          firstClick = true;
        }
      } else {
        rb.AddForce(0, -1000, 0);
      }




      if( rb.position.y < 0){
        if (GlobalControl.Instance.TargetID == 2) {
          GlobalControl.Instance.ID2Count = GlobalControl.Instance.ID2Count - 1;
        } else if (GlobalControl.Instance.TargetID == 4) {
          GlobalControl.Instance.ID4Count = GlobalControl.Instance.ID4Count - 1;
        } else if (GlobalControl.Instance.TargetID == 6) {
          GlobalControl.Instance.ID6Count = GlobalControl.Instance.ID6Count - 1;
        }
        gameManager.Restart();
      }
    }

}

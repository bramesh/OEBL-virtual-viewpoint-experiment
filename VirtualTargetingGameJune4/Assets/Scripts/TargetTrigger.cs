using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetTrigger : MonoBehaviour
{

    public GameManager gameManager;
    public float stayInTriggerFor = 0.5f;

    // To change target color when entered
    Renderer rend;
    Color darkGreen = new Color(144f/256f, 238f/256f, 144f/256f);
    Color lightGreen = new Color(14f/256f, 161f/256f, 14f/256f);

    private float localStartTime;
    private bool inTrigger;

    void Start() {
      rend = GetComponent<Renderer>();
    }

    public void OnTriggerEnter(){
      // Grab time when trigger is hit
      inTrigger = true;
      localStartTime = Time.time;
    }

    public void OnTriggerExit(){
      // If trigger is exited restart timer
      inTrigger = false;
      localStartTime = 0;
    }

    void Update() {
      // Run next trial if 0.5 sec elapses
      if (inTrigger) {
        rend.material.color = darkGreen;
        if ((Time.time - localStartTime) > stayInTriggerFor) {
          gameManager.EndTimer();
          gameManager.CompleteLevel();
        }
      } else {
        rend.material.color = lightGreen;
      }
    }

}

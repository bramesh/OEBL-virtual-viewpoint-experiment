using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

  public float restartDelay = 0.1f;

  private float startTime;
  private float endTime;

  private static System.Random rng = new System.Random(); // To randomize viewpoints
  bool validViewpoint; // To track when a new viewpoint has been found

  // Function to start timer
  public void StartTimer(){
    startTime = Time.time;
  }

  // Function to end timer
  public void EndTimer(){
    endTime = Time.time;
  }

  // Function to be run when level is completed
  public void CompleteLevel(){
    GlobalControl.Instance.cursorMovement = false;
    Debug.Log("Trial complete.");
    Debug.Log(GlobalControl.Instance.trialNumber);
    GlobalControl.Instance.viewpointOrder[0] = 1;

    // Add trial time to list
    GlobalControl.Instance.trialTimes[GlobalControl.Instance.trialNumber] = endTime - startTime;

    // If all trials have been completed, load the credit scene
    if(GlobalControl.Instance.trialNumber >= ((GlobalControl.numberOfTrials*GlobalControl.numberOfViewpoints)-1)) {
      Debug.Log("Here");
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // If the trial is the last for the current viewpoint
    if ((GlobalControl.Instance.trialNumber + 1)%GlobalControl.numberOfTrials == 0){

      for (int i = 0; i < 30; i++){
        Debug.Log(GlobalControl.Instance.trialIDs[i]);
      }
      validViewpoint = false;

      // Get the next random viewpoint
      while(!validViewpoint){
        if (!GlobalControl.Instance.viewpointOrder.Contains(0)) {
          break;
        }
        GlobalControl.Instance.nextCameraPosition = rng.Next(1, 17);
        if (!GlobalControl.Instance.viewpointOrder.Contains(GlobalControl.Instance.nextCameraPosition)){
          Debug.Log("Viewpoint complete.");

          // Valid viewpoint has been found
          validViewpoint = true;

          // Record the next camera position
          GlobalControl.Instance.viewpointOrder[(GlobalControl.Instance.trialNumber + 1)/GlobalControl.numberOfTrials] = GlobalControl.Instance.nextCameraPosition;
        }
      }
    }

    // Increment the trial number
    GlobalControl.Instance.trialNumber = GlobalControl.Instance.trialNumber + 1;

    Debug.Log("EndCompleteLevel");

    Invoke("Restart", restartDelay);
  }

  // Reloads the current scene
  public void Restart() {
    GlobalControl.Instance.cursorMovement = true;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    Debug.Log("Camera Position" + GlobalControl.Instance.nextCameraPosition);
  }

  void Update() {
    if (GlobalControl.Instance.nextCameraPosition == 0) {
      GlobalControl.Instance.nextCameraPosition = 1;
      GlobalControl.Instance.viewpointOrder[0] = 1;
    }
    SetCameraPose(GlobalControl.Instance.nextCameraPosition);
  }

  // Set the camera pose for a given viewpoint
  public void SetCameraPose(int viewpoint) {
    if (viewpoint == 1){
      Camera.main.transform.position = new Vector3(0f, 7.5f, -13f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 0f, 0f);
    } else if (viewpoint == 2){
      Camera.main.transform.position = new Vector3(-6.5f, 7.5f, -11.26f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 30f, 0f);
    } else if (viewpoint == 3){
      Camera.main.transform.position = new Vector3(-11.26f, 7.5f, -6.5f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 60f, 0f);
    } else if (viewpoint == 4){
      Camera.main.transform.position = new Vector3(-13f, 7.5f, 0f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 90f, 0f);
    } else if (viewpoint == 5){
      Camera.main.transform.position = new Vector3(-11.26f, 7.5f, 6.5f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 120f, 0f);
    } else if (viewpoint == 6){
      Camera.main.transform.position = new Vector3(-6.5f, 7.5f, 11.26f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 150f, 0f);
    } else if (viewpoint == 7){
      Camera.main.transform.position = new Vector3(0f, 7.5f, 13f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 180f, 0f);
    } else if (viewpoint == 8){
      Camera.main.transform.position = new Vector3(6.5f, 7.5f, 11.26f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 210f, 0f);
    } else if (viewpoint == 9){
      Camera.main.transform.position = new Vector3(11.26f, 7.5f, 6.5f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 240f, 0f);
    } else if (viewpoint == 10){
      Camera.main.transform.position = new Vector3(13f, 7.5f, 0f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 270f, 0f);
    } else if (viewpoint == 11){
      Camera.main.transform.position = new Vector3(11.26f, 7.5f, -6.5f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 300f, 0f);
    } else if (viewpoint == 12){
      Camera.main.transform.position = new Vector3(6.5f, 7.5f, -11.26f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 330f, 0f);
    } else if (viewpoint == 13){
      Camera.main.transform.position = new Vector3(0f, 13f, -7.5f);
      Camera.main.transform.rotation = Quaternion.Euler(60f, 0f, 0f);
    } else if (viewpoint == 14){
      Camera.main.transform.position = new Vector3(0f, 15f, 0f);
      Camera.main.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    } else if (viewpoint == 15){
      Camera.main.transform.position = new Vector3(0f, 13f, 7.5f);
      Camera.main.transform.rotation = Quaternion.Euler(120f, 0f, 0f);
    } else if (viewpoint == 16){
      Camera.main.transform.position = new Vector3(0f, 7.5f, 13f);
      Camera.main.transform.rotation = Quaternion.Euler(150f, 0f, 0f);
    } else {
      Camera.main.transform.position = new Vector3(0f, 7.5f, -13f);
      Camera.main.transform.rotation = Quaternion.Euler(30f, 0f, 0f);
    }
  }

}

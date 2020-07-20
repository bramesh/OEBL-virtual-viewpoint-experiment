using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{

    public float restartDelay = 0.1f;

    private float startTime;
    private float endTime;

    private float distance = 0;
    private double x_diff;
    private double z_diff;

    private float polar_angle;
    private float azimuthal_angle;

    private float x_location;
    private float y_location;
    private float z_location;
    private float residual_distance;
    private float x_rotation;
    private float y_rotation;
    private float z_rotation;

    private bool is_running = false;

    public void StartTimer()
    {
        startTime = Time.time;
        ZeroDistance();
    }

    public void ZeroDistance()
    {
        distance = 0f;
    }

    public void EndTimer()
    {
        endTime = Time.time;
    }

    public void AddDistance(float firstX, float firstZ, float secondX, float secondZ)
    {
        x_diff = (double)(firstX - secondX);
        z_diff = (double)(firstZ - secondZ);

        distance = distance + (float)Math.Sqrt(x_diff * x_diff + z_diff * z_diff);
    }

    public void CompleteLevel()
    {
        if (is_running)
        {
            return;
        }
        else
        {
            is_running = true;
            StartCoroutine(CallCompleteLevel());
        }
        

    }

    // Function to be run when level is completed
    IEnumerator CallCompleteLevel() {

        Debug.Log("Trial Complete: " + GlobalControl.Instance.trialNumber.ToString());
        Debug.Log("Viewpoint Complete: " + (GlobalControl.Instance.viewpointNumber - 1).ToString());

        // Add trial time to list
        // GlobalControl.Instance.trialTimes[GlobalControl.Instance.trialNumber] = endTime - startTime;

        // Write time to database
        WWWForm form = new WWWForm();
        form.AddField("id", GlobalControl.Instance.subjectID);
        form.AddField("trialNumber", GlobalControl.Instance.trialNumber);
        form.AddField("polarAngle", GlobalControl.Instance.cameraPositions[GlobalControl.Instance.viewpointNumber - 1][0]);
        form.AddField("azimuthalAngle", GlobalControl.Instance.cameraPositions[GlobalControl.Instance.viewpointNumber - 1][1]);
        form.AddField("time", (endTime - startTime).ToString());
        form.AddField("distance", distance.ToString());

        //WWW www = new WWW("http://oeblviewpoints.000webhostapp.com//trial.php", form);
        //yield return www;
        //http://localhost:8888/virtualexperiment/trial.php

        using (UnityWebRequest www = UnityWebRequest.Post("http://oeblviewpoints.000webhostapp.com//trial.php", form))
        {
            yield return www.SendWebRequest();
        }

        // If all trials have been completed, load the credit scene
        if (GlobalControl.Instance.trialNumber == (GlobalControl.numberOfTrials * GlobalControl.numberOfViewpoints - 1))
        {
            Debug.Log("Here");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            yield break;
        }

        // If the trial is the last for the current viewpoint
        if ((GlobalControl.Instance.trialNumber + 1) % GlobalControl.numberOfTrials == 0)
        {
            Debug.Log("Herez");
            GlobalControl.Instance.trialNumber = GlobalControl.Instance.trialNumber + 1;
            GlobalControl.Instance.viewpointNumber = GlobalControl.Instance.viewpointNumber + 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            yield break;
        }

        // Increment the trial number
        GlobalControl.Instance.trialNumber = GlobalControl.Instance.trialNumber + 1;

        Debug.Log("EndCompleteLevel");

        is_running = false;

        //Invoke("Restart", restartDelay);
        Restart();
    }

  // Reloads the current scene
  public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

  void Update()
    {
        SetCameraPose();
    }

  // Set the camera pose for a given viewpoint
  public void SetCameraPose() {
        polar_angle = GlobalControl.Instance.cameraPositions[GlobalControl.Instance.viewpointNumber - 1][0] * (float) Math.PI / 180f;
        azimuthal_angle = GlobalControl.Instance.cameraPositions[GlobalControl.Instance.viewpointNumber - 1][1] * (float)Math.PI / 180f;

        y_location = 15f * (float) Math.Cos((double)azimuthal_angle);
        residual_distance = (float) Math.Sqrt((double)(225f - y_location*y_location));
        x_location = Math.Sign((double) azimuthal_angle) * residual_distance * (float) Math.Sin( (double) polar_angle);
        z_location = Math.Sign((double)azimuthal_angle) * residual_distance * (float) Math.Cos( (double) polar_angle);

        x_rotation = 90f + GlobalControl.Instance.cameraPositions[GlobalControl.Instance.viewpointNumber - 1][1];
        y_rotation = GlobalControl.Instance.cameraPositions[GlobalControl.Instance.viewpointNumber - 1][0];
        z_rotation = 0f;

        Camera.main.transform.position = new Vector3(x_location, y_location, z_location);
        Camera.main.transform.rotation = Quaternion.Euler(x_rotation, y_rotation, z_rotation);

  }

}

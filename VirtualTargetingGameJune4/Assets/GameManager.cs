using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Diagnostics;
using System.IO;

public class GameManager : MonoBehaviour
{

    public float restartDelay = 0.1f;

    private float startTime;
    private float endTime;

    // For recording distance and path
    private float distance = 0;
    private List<List<float>> path = new List<List<float>>();
    
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

    // Start the trial timer and zero trial distance
    public void StartTimer()
    {
        GlobalControl.Instance.firstMovement = Time.time;
        startTime = Time.time;
        ZeroDistance();
    }

    // Helper function to zero distance
    public void ZeroDistance()
    {
        path = new List<List<float>>();
        path.Add(new List<float> { 0, 0 });
        distance = 0f;
    }

    // End the trial timer
    public void EndTimer()
    {
        GlobalControl.Instance.completionTime = Time.time;
        endTime = Time.time;
    }

    // Add the distance traveled since last update
    public void AddDistance(float firstX, float firstZ, float secondX, float secondZ)
    {
        x_diff = (double)(firstX - secondX);
        z_diff = (double)(firstZ - secondZ);
        
        //Enable if using path by coordinates
        //path.Add(new List<float> { secondX, secondZ });

        distance = distance + (float)Math.Sqrt(x_diff * x_diff + z_diff * z_diff);
    }

    // Start coroutine
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

        UnityEngine.Debug.Log("Trial Complete: " + GlobalControl.Instance.trialNumber.ToString());

        // Write time to database
        WWWForm form = new WWWForm();
        form.AddField("id", GlobalControl.Instance.subjectID);
        form.AddField("trialNumber", GlobalControl.Instance.trialNumber);
        form.AddField("polarAngle", GlobalControl.Instance.cameraPositions[GlobalControl.Instance.viewpointNumber - 1][0]);
        form.AddField("azimuthalAngle", GlobalControl.Instance.cameraPositions[GlobalControl.Instance.viewpointNumber - 1][1]);
        form.AddField("indexOfDifficulty", GlobalControl.Instance.targetConfig[GlobalControl.Instance.trialNumber % GlobalControl.numberOfTrials][0].ToString());
        form.AddField("targetDirection", ((180 / Mathf.PI) * GlobalControl.Instance.targetConfig[GlobalControl.Instance.trialNumber % GlobalControl.numberOfTrials][1]).ToString());
        form.AddField("time", (endTime - startTime).ToString());
        form.AddField("distance", distance.ToString());
        form.AddField("path", GlobalControl.Instance.trajectory);
        form.AddField("loadTime", GlobalControl.Instance.loadTime.ToString());
        form.AddField("firstMovement", GlobalControl.Instance.firstMovement.ToString());
        form.AddField("completionTime", GlobalControl.Instance.completionTime.ToString());

        //WWW www = new WWW("http://oeblviewpoints.000webhostapp.com//trial.php", form);
        //yield return www;
        //http://localhost:8888/virtualexperiment/trial.php

        using (UnityWebRequest www = UnityWebRequest.Post("http://oeblviewpoints.com//trial.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "0")
            {
                UnityEngine.Debug.Log("Trial data inserted.");
            }
            else
            {
                UnityEngine.Debug.Log("Trial data push failed. Error #" + www.downloadHandler.text);
            }
        }

        // If all trials have been completed, load the credit scene
        if (GlobalControl.Instance.trialNumber == (GlobalControl.numberOfTrials * GlobalControl.numberOfViewpoints - 1))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
            yield break;
        }

        // If the trial is the last for the current viewpoint
        if ((GlobalControl.Instance.trialNumber + 1) % GlobalControl.numberOfTrials == 0)
        {
            // Scramble target configs
            WelcomeMenu.Randomizer.Randomize(GlobalControl.Instance.targetConfig);

            GlobalControl.Instance.trialNumber = GlobalControl.Instance.trialNumber + 1;
            GlobalControl.Instance.viewpointNumber = GlobalControl.Instance.viewpointNumber + 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            yield break;
        }

        // Increment the trial number
        GlobalControl.Instance.trialNumber = GlobalControl.Instance.trialNumber + 1;

        is_running = false;

        // Enable if using catch trials
        /*
        if (Array.Exists(GlobalControl.Instance.catchTrialLocations, element => element == GlobalControl.Instance.trialNumber))
        {
            UnityEngine.Debug.Log("In catch trial if");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2 + Array.IndexOf(GlobalControl.Instance.catchTrialLocations, GlobalControl.Instance.trialNumber));
        } else
        {
            Restart();
        } */

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

        y_location = 20f * (float) Math.Cos((double)azimuthal_angle);
        residual_distance = (float) Math.Sqrt((double)(400f - y_location*y_location));
        x_location = Math.Sign((double) azimuthal_angle) * residual_distance * (float) Math.Sin( (double) polar_angle);
        z_location = Math.Sign((double)azimuthal_angle) * residual_distance * (float) Math.Cos( (double) polar_angle);

        x_rotation = 90f + GlobalControl.Instance.cameraPositions[GlobalControl.Instance.viewpointNumber - 1][1];
        y_rotation = GlobalControl.Instance.cameraPositions[GlobalControl.Instance.viewpointNumber - 1][0];
        z_rotation = 0f;

        Camera.main.transform.position = new Vector3(x_location, y_location, z_location);
        Camera.main.transform.rotation = Quaternion.Euler(x_rotation, y_rotation, z_rotation);

  }

    private String PathToString(List<List<float>> pathList)
    {
        String output = "";

        foreach (List<float> list in pathList)
        {
            output = String.Concat(output, "(", String.Format("{0:0.00}", list[0]), ",", String.Format("{0:0.00}", list[1]), ") ");
        }

        UnityEngine.Debug.Log(output);

        // Remove last space
        output = output.Substring(0, output.Length - 1);

        UnityEngine.Debug.Log(output);
        return output;

    }

}

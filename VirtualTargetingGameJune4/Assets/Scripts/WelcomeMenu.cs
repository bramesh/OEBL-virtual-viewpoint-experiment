using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WelcomeMenu : MonoBehaviour
{
    public InputField idField;

    public Button nextButton;

    public Text informationDisplay;

    static int[] cam1 = new int[2] { 0, -60 };
    static int[] cam2 = new int[2] { 45, -60 };
    static int[] cam3 = new int[2] { 90, -60 };
    static int[] cam4 = new int[2] { 135, -60 };
    static int[] cam5 = new int[2] { 180, -60 };
    static int[] cam6 = new int[2] { 225, -60 };
    static int[] cam7 = new int[2] { 270, -60 };
    static int[] cam8 = new int[2] { 315, -60 };
    static int[] cam9 = new int[2] { 0, 0 };
    static int[] cam10 = new int[2] { 45, 0 };
    static int[] cam11 = new int[2] { 90, 0 };
    static int[] cam12 = new int[2] { 135, 0 };
    static int[] cam13 = new int[2] { 180, 0 };
    static int[] cam14 = new int[2] { 225, 0 };
    static int[] cam15 = new int[2] { 270, 0 };
    static int[] cam16 = new int[2] { 315, 0 };
    static int[] cam17 = new int[2] { 0, 60 };
    static int[] cam18 = new int[2] { 45, 60 };
    static int[] cam19 = new int[2] { 90, 60 };
    static int[] cam20 = new int[2] { 135, 60 };
    static int[] cam21 = new int[2] { 180, 60 };
    static int[] cam22 = new int[2] { 225, 60 };
    static int[] cam23 = new int[2] { 270, 60 };
    static int[] cam24 = new int[2] { 315, 60 };

    public void CallLogin()
    {
        if (Int32.Parse(idField.text)%2 == 0)
        {
            GlobalControl.Instance.cameraPositions = new int[][] { cam1, cam4, cam6, cam7, cam10, cam11, cam13, cam16, cam17, cam20, cam22, cam23 };
        } else
        {
            GlobalControl.Instance.cameraPositions = new int[][] { cam2, cam3, cam5, cam8, cam9, cam12, cam14, cam15, cam18, cam19, cam21, cam24 };
        }

        StartCoroutine(LogIn());
    }

    IEnumerator LogIn()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", idField.text);
        //WWW www = new WWW("http://oeblviewpoints.000webhostapp.com//login.php", form);
        //yield return www;
        //http://localhost:8888/virtualexperiment/login.php

        //if (www.text == "0")
        //{
        //    Debug.Log("User logged in successfully.");
        //    GlobalControl.Instance.subjectID = idField.text;
        //    Randomizer.Randomize(GlobalControl.Instance.cameraPositions);
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //} else if (www.text == "")
        //{
        //    informationDisplay.text = ("Server communication failed.");
        //}
        //else
        //{
        //    informationDisplay.text = ("User login failed. Error #" + www.text);
        //}

        using (UnityWebRequest www = UnityWebRequest.Post("http://oeblviewpoints.000webhostapp.com//login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                informationDisplay.text = "Server communication failed.";
            } else if (www.downloadHandler.text == "0")
            {
                Debug.Log("User logged in successfully.");
                GlobalControl.Instance.subjectID = idField.text;
                Randomizer.Randomize(GlobalControl.Instance.cameraPositions);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                informationDisplay.text = ("User login failed. Error #" + www.downloadHandler.text);
            }
        }
    }

    public void VerifyInputs()
    {
        nextButton.interactable = (idField.text.Length > 0);
    }

    public class Randomizer
    {
        public static void Randomize<T>(T[] items)
        {
            System.Random rand = new System.Random();

            // For each spot in the array, pick
            // a random item to swap into that spot.
            for (int i = 0; i < items.Length - 1; i++)
            {
                int j = rand.Next(i, items.Length);
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
        }
    }
}

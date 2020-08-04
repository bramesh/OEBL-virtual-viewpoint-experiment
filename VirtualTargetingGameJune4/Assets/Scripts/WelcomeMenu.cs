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

    System.Random rnd = new System.Random();

    int[] changedCameraPosition;

    private int[][] cameraPositions = new int[][] { cam2, cam3, cam4, cam5, cam6, cam7, cam8, cam9, cam10, cam11, cam12, cam13, cam14, cam15, cam16, cam17, cam18, cam19, cam20, cam21, cam22, cam23, cam24 };

    public void CallLogin()
    {
        // Define camera positions
        changedCameraPosition = cameraPositions[Int32.Parse(idField.text) % 23];
        GlobalControl.Instance.cameraPositions = new int[][] { cam1, cam1, cam1, changedCameraPosition, changedCameraPosition, changedCameraPosition };

        // Scramble target configs before first viewpoint
        Randomizer.Randomize(GlobalControl.Instance.targetConfig);

        // Set the random scenes after which to run catch trials
        while (Array.Exists(GlobalControl.Instance.catchTrialLocations, element => element == 0))
        {
            for (int i = 0; i < 3; i++)
            {
                int nextLocation = rnd.Next(5, GlobalControl.numberOfTrials * GlobalControl.numberOfViewpoints - 5);
                if (GlobalControl.Instance.catchTrialLocations[i] == 0 && !Array.Exists(GlobalControl.Instance.catchTrialLocations, element => element == nextLocation))
                {
                    GlobalControl.Instance.catchTrialLocations[i] = nextLocation;
                }
            }
        }

        StartCoroutine(LogIn());
    }

    IEnumerator LogIn()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", idField.text);

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
                //Randomizer.Randomize(GlobalControl.Instance.cameraPositions);
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

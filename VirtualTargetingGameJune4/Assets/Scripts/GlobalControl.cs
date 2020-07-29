using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public static int numberOfTrials = 24;              // #trials per viewpoint
    public static int numberOfViewpoints = 6;           // #viewpoints per experiment

    public int trialNumber;                             // n trials per viewpoint
    public int viewpointNumber;                         // number of viewpoints per experiment
    public string subjectID = "";                       // Unique subject identifier


    // Generate random target ID and direction order
    static float[] set1 = new float[2] { 2, 0 };
    static float[] set2 = new float[2] { 2, Mathf.PI / 4f };
    static float[] set3 = new float[2] { 2, Mathf.PI / 2f };
    static float[] set4 = new float[2] { 2, 3*Mathf.PI / 4f };
    static float[] set5 = new float[2] { 2, Mathf.PI };
    static float[] set6 = new float[2] { 2, 5 * Mathf.PI / 4f };
    static float[] set7 = new float[2] { 2, 3 * Mathf.PI / 2f };
    static float[] set8 = new float[2] { 2, 7 * Mathf.PI / 4f };
    static float[] set9 = new float[2] { 3, 0 };
    static float[] set10 = new float[2] { 3, Mathf.PI / 4f };
    static float[] set11 = new float[2] { 3, Mathf.PI / 2f };
    static float[] set12 = new float[2] { 3, 3 * Mathf.PI / 4f };
    static float[] set13 = new float[2] { 3, Mathf.PI };
    static float[] set14 = new float[2] { 3, 5 * Mathf.PI / 4f };
    static float[] set15 = new float[2] { 3, 3 * Mathf.PI / 2f };
    static float[] set16 = new float[2] { 3, 7 * Mathf.PI / 4f };
    static float[] set17 = new float[2] { 4, 0 };
    static float[] set18 = new float[2] { 4, Mathf.PI / 4f };
    static float[] set19 = new float[2] { 4, Mathf.PI / 2f };
    static float[] set20 = new float[2] { 4, 3 * Mathf.PI / 4f };
    static float[] set21 = new float[2] { 4, Mathf.PI };
    static float[] set22 = new float[2] { 4, 5 * Mathf.PI / 4f };
    static float[] set23 = new float[2] { 4, 3 * Mathf.PI / 2f };
    static float[] set24 = new float[2] { 4, 7 * Mathf.PI / 4f };

    public float[][] targetConfig = new float[][] { set1, set2, set3, set4, set5, set6, set7, set8, set9, set10, set11, set12, set13, set14, set15, set16, set17, set18, set19, set20, set21, set22, set23, set24 };

    // Instruction variables
    public int numberOfHits;
    public bool testFall = false;

    public int[][] cameraPositions; // = new int[][] { cam1, cam2, cam3, cam4, cam5, cam6, cam7, cam8, cam9, cam10, cam11, cam12, cam13, cam14, cam15, cam16, cam17, cam18, cam19, cam20, cam21, cam22, cam23, cam24 };

    void Awake(){
      if(Instance == null){
        DontDestroyOnLoad(gameObject);
        Instance = this;
      } else if(Instance != this){
        Destroy(gameObject);
      }
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

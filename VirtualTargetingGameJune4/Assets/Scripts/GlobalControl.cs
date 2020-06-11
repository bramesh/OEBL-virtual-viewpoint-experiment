using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public static int numberOfTrials = 30;
    public static int numberOfViewpoints = 16;

    public int trialNumber; // n trials per viewpoint
    public int viewpointNumber; // 16 viewpoints per experiment
    public int[] viewpointOrder = new int[16]; // Randomized order of viewpoints
    public float[] trialTimes = new float[numberOfViewpoints * numberOfTrials]; // Times for each trial
    public int[] trialIDs = new int[numberOfViewpoints * numberOfTrials]; // ID for each trial
    public int TargetID;
    public int ID2Count = 0;
    public int ID4Count = 0;
    public int ID6Count = 0;
    public int nextCameraPosition;
    public bool cursorMovement = true;

    void Awake(){
      if(Instance == null){
        DontDestroyOnLoad(gameObject);
        Instance = this;
      } else if(Instance != this){
        Destroy(gameObject);
      }
    }
}

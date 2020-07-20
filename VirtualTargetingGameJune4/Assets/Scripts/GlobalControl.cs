using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public static int numberOfTrials = 30;
    public static int numberOfViewpoints = 12;

    public int trialNumber;                             // n trials per viewpoint
    public int viewpointNumber;                         // number of viewpoints per experiment
    public string subjectID = "";

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
}

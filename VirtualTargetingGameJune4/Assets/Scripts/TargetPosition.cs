using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    System.Random rnd = new System.Random();
    float radius;
    float scale;

    // Start is called before the first frame update
    void Start()
    {

        bool goodAngle = false;
        float angle = Random.Range(0f, 6.28f); // Random angle in radians

        while (!goodAngle) {
          if ((angle % 1.57) > 0.5 && (angle % 1.57) < 1.07) {
            goodAngle = true;
          } else {
            angle = Random.Range(0f, 6.28f);
          }
        }

        if (GlobalControl.Instance.trialNumber % 30 == 0) {
          GlobalControl.Instance.ID2Count = 0;
          GlobalControl.Instance.ID4Count = 0;
          GlobalControl.Instance.ID6Count = 0;
        }

        bool validID = false;

        while (!validID){
          int ID = rnd.Next(0, 3);
          if (ID == 0) {
            if (GlobalControl.Instance.ID2Count < 10) {
              GlobalControl.Instance.ID2Count = GlobalControl.Instance.ID2Count + 1;
              GlobalControl.Instance.TargetID = 2;
              GlobalControl.Instance.trialIDs[GlobalControl.Instance.trialNumber] = 2;
              validID = true;
            }
          } else if (ID == 1) {
            if (GlobalControl.Instance.ID4Count < 10) {
              GlobalControl.Instance.ID4Count = GlobalControl.Instance.ID4Count + 1;
              GlobalControl.Instance.TargetID = 4;
              GlobalControl.Instance.trialIDs[GlobalControl.Instance.trialNumber] = 4;
              validID = true;
            }
          } else if (ID == 2) {
            if (GlobalControl.Instance.ID6Count < 10) {
              GlobalControl.Instance.ID6Count = GlobalControl.Instance.ID6Count + 1;
              GlobalControl.Instance.TargetID = 6;
              GlobalControl.Instance.trialIDs[GlobalControl.Instance.trialNumber] = 6;
              validID = true;
            }
          }
        }

        // Needs update: From 2,4,6 to 2,3,4
        if (GlobalControl.Instance.TargetID == 2) {
          radius = 3f;
          scale = 1f;
        } else if (GlobalControl.Instance.TargetID == 4) {
          radius = 7f;
          scale = 1f;
        } else if (GlobalControl.Instance.TargetID == 6) {
          radius = 7.5f;
          scale = 0.5f;
        }

        float x_location = radius*(float)System.Math.Sin((double)angle);
        float z_location = radius*(float)System.Math.Cos((double)angle);

        transform.position = new Vector3(x_location, 0.5f, z_location);
        transform.localScale = new Vector3(scale, 0.1f, scale);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    System.Random rnd = new System.Random();
    private float radius; // = 5f;
    private float id;
    private float scale;
    private float angle;

    private float x_location;
    private float z_location;

    // Random angles for instructions
    public bool instructionMode = false;

    // Start is called before the first frame update
    void Start()
    {
        // Enable when using random angle

        if (instructionMode)
        {
            bool goodAngle = false;
            float angle = UnityEngine.Random.Range(0f, 6.28f); // Random angle in radians
            radius = 5f;

            while (!goodAngle)
            {
                if ((angle % 1.57) > 0.5 && (angle % 1.57) < 1.07)
                {
                    goodAngle = true;
                }
                else
                {
                    angle = UnityEngine.Random.Range(0f, 6.28f);
                }
            }

            x_location = radius * (float)System.Math.Sin((double)angle);
            z_location = radius * (float)System.Math.Cos((double)angle);

            transform.position = new Vector3(x_location, 0.5f, z_location);
            transform.localScale = new Vector3(1f, 0.1f, 1f);
        }
        else
        {
            id = GlobalControl.Instance.targetConfig[GlobalControl.Instance.trialNumber % GlobalControl.numberOfTrials][0];
            scale = 10f / (Mathf.Pow(2f, id));
            angle = GlobalControl.Instance.targetConfig[GlobalControl.Instance.trialNumber % GlobalControl.numberOfTrials][1];
            
            if ((int)(angle / (Math.PI / 4)) % 2 == 1)
            {
                radius = 2.5f * (float)Math.Sqrt(2); 
            } else
            {
                radius = 5f;
            }

            x_location = radius * (float)System.Math.Sin((double)angle);
            z_location = radius * (float)System.Math.Cos((double)angle);

            transform.position = new Vector3(x_location, 0.5f, z_location);
            transform.localScale = new Vector3(scale, 0.1f, scale);
        }
    }
}

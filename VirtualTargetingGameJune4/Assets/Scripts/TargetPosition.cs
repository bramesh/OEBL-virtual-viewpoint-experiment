using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    System.Random rnd = new System.Random();
    public float radius = 7.5f;
    public float scale = 0.5f;
    float x_location;
    float z_location;

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

        x_location = radius*(float)System.Math.Sin((double)angle);
        z_location = radius*(float)System.Math.Cos((double)angle);

        transform.position = new Vector3(x_location, 0.5f, z_location);
        transform.localScale = new Vector3(scale, 0.1f, scale);
    }
}

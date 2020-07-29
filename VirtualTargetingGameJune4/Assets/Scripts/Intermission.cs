using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intermission : MonoBehaviour
{
    public Text viewpointsRemaining;

    void Start()
    {
        viewpointsRemaining.text = (GlobalControl.numberOfViewpoints - GlobalControl.Instance.viewpointNumber + 1).ToString() + " Rounds(s) Remaining";
    }

    
}

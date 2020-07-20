using System;
using UnityEngine;
using UnityEngine.UI;

public class TimesCredits : MonoBehaviour
{

    public Text timesText;

    // Update is called once per frame
    void Start()
    {
        string output = "";
        for(int i = 0; i < GlobalControl.Instance.trialTimes.Length; i++) {
          output = String.Concat(output, String.Format("{0:0.00}", GlobalControl.Instance.trialTimes[i]));
          output = String.Concat(output, " ");
        }
        Debug.Log(GlobalControl.Instance.trialTimes);
        timesText.text = output;
    }
}

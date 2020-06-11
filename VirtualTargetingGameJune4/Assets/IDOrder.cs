using System;
using UnityEngine;
using UnityEngine.UI;

public class IDOrder : MonoBehaviour
{

    public Text IDText;

    // Update is called once per frame
    void Start()
    {
        string output = "";
        for(int i = 0; i < GlobalControl.Instance.trialIDs.Length; i++) {
          output = String.Concat(output, String.Format("{0:0.00}", GlobalControl.Instance.trialIDs[i]));
          output = String.Concat(output, " ");
        }
        Debug.Log(GlobalControl.Instance.trialTimes);
        IDText.text = output;
    }
}

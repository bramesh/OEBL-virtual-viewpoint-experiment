using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendCatchResults : MonoBehaviour
{
    private bool is_running = false;

    void Start()
    {
        if (is_running)
        {
            return;
        }
        else
        {
            is_running = true;
            StartCoroutine(SendCatchTrialResults());
        }
    }

    IEnumerator SendCatchTrialResults()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", GlobalControl.Instance.subjectID);
        form.AddField("redResult", GlobalControl.Instance.catchTrialResults[0].ToString());
        form.AddField("redTime", GlobalControl.Instance.catchTrialTimes[0].ToString());
        form.AddField("blueResult", GlobalControl.Instance.catchTrialResults[1].ToString());
        form.AddField("blueTime", GlobalControl.Instance.catchTrialTimes[1].ToString());
        form.AddField("greenResult", GlobalControl.Instance.catchTrialResults[2].ToString());
        form.AddField("greenTime", GlobalControl.Instance.catchTrialTimes[2].ToString());

        using (UnityWebRequest www = UnityWebRequest.Post("http://oeblviewpoints.000webhostapp.com//catchtrial.php", form))
        {
            yield return www.SendWebRequest();
        }
    }
}
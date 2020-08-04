using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CatchTrial : MonoBehaviour
{
    private float startTime;

    public Button nextButton;

    public bool Red = false;
    public bool Blue = false;
    public bool Green = false;

    private bool selectionMade = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RedPressed()
    {
        if (!selectionMade)
        {
            if (Red)
            {
                GlobalControl.Instance.catchTrialResults[0] = true;
            }
            else
            {
                GlobalControl.Instance.catchTrialResults[0] = true;
            }
            GlobalControl.Instance.catchTrialTimes[0] = Time.time - startTime;
            nextButton.interactable = true;
            selectionMade = true;
        }
    }

    public void BluePressed()
    {
        if (!selectionMade)
        {
            if (Blue)
            {
                GlobalControl.Instance.catchTrialResults[1] = true;
            }
            else
            {
                GlobalControl.Instance.catchTrialResults[1] = true;
            }
            GlobalControl.Instance.catchTrialTimes[1] = Time.time - startTime;
            nextButton.interactable = true;
            selectionMade = true;
        }
    }

    public void GreenPressed()
    {
        if (!selectionMade)
        {
            if (Green)
            {
                GlobalControl.Instance.catchTrialResults[2] = true;
            }
            else
            {
                GlobalControl.Instance.catchTrialResults[2] = true;
            }
            GlobalControl.Instance.catchTrialTimes[2] = Time.time - startTime;
            nextButton.interactable = true;
            selectionMade = true;
        }
    }

    public void ContinuePressed()
    {
        if (Red)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        } else if (Blue)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        } else if (Green)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
        }
            
    }


}

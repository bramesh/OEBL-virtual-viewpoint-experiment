using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TargetTriggerInstructions : MonoBehaviour
{
    public float stayInTriggerFor = 0.5f;

    // To change target color when entered
    Renderer rend;
    Color darkGreen = new Color(144f / 256f, 238f / 256f, 144f / 256f);
    Color lightGreen = new Color(14f / 256f, 161f / 256f, 14f / 256f);

    private float localStartTime;
    private bool inTrigger;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void OnTriggerEnter()
    {
        // Grab time when trigger is hit
        inTrigger = true;
        localStartTime = Time.time;
    }

    public void OnTriggerExit()
    {
        // If trigger is exited restart timer
        inTrigger = false;
        localStartTime = 0;
    }

    void Update()
    {
        if (inTrigger)
        {
            rend.material.color = darkGreen;
            if ((Time.time - localStartTime) > stayInTriggerFor)
            {
                GlobalControl.Instance.numberOfHits += 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            rend.material.color = lightGreen;
        }
    }
}

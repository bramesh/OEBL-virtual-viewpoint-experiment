using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions1 : MonoBehaviour
{
    public Button nextButton;

    private float startTime;
    public float buttonActivationDelay = 5f;

    public void NextPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void IntermissionNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void Start()
    {
        startTime = Time.time;
        nextButton.interactable = false;
    }

    void Update()
    {
        if (Time.time - startTime > buttonActivationDelay)
        {
            nextButton.interactable = true;
        }
        else
        {
            nextButton.interactable = false;
        }
    }
}

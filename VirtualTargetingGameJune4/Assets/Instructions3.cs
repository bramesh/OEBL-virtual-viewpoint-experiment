using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions3 : MonoBehaviour
{
    public Button nextButton;

    public void NextPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Start()
    {
        nextButton.interactable = false;
    }

    void Update()
    {
        if (GlobalControl.Instance.numberOfHits >= 3)
        {
            nextButton.interactable = true;
        }
        else
        {
            nextButton.interactable = false;
        }
    }
}

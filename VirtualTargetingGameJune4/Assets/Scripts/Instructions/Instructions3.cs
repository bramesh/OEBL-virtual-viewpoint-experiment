using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions3 : MonoBehaviour
{
    public Button nextButton;
    public int hitsToPass = 3;

    public void NextPressed()
    {
        GlobalControl.Instance.numberOfHits = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Start()
    {
        nextButton.interactable = false;
    }

    void Update()
    {
        if (GlobalControl.Instance.numberOfHits >= hitsToPass)
        {
            nextButton.interactable = true;
        }
        else
        {
            nextButton.interactable = false;
        }
    }
}

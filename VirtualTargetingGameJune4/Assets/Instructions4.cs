using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions4 : MonoBehaviour
{
    public Button nextButton;
    public Rigidbody rb;

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
        if (rb.position.y < 0)
        {
            GlobalControl.Instance.testFall = true;
        }
        nextButton.interactable = GlobalControl.Instance.testFall;
    }
}

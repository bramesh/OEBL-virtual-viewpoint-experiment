using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits1 : MonoBehaviour
{
    public void Next() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Back() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}

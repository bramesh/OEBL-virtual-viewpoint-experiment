using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PasswordMenu : MonoBehaviour
{
    public InputField passwordField;

    public Button nextButton;

    public Text informationDisplay;

    public void OnClick()
    { 
        StartCoroutine(Password());
    }

    IEnumerator Password()
    {
        WWWForm form = new WWWForm();
        form.AddField("password", passwordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://oeblviewpoints.000webhostapp.com//password.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                informationDisplay.text = "Server communication failed.";
            }
            else if (www.downloadHandler.text == "0")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                informationDisplay.text = ("Password login failed. Error #" + www.downloadHandler.text);
            }
        }
    }

    public void VerifyInputs()
    {
        nextButton.interactable = (passwordField.text.Length > 0);
    }
}

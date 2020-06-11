using UnityEngine;
using UnityEngine.UI;


public class Order : MonoBehaviour
{

    public Text orderText;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GlobalControl.Instance.viewpointOrder);
        string output = string.Join(" ", GlobalControl.Instance.viewpointOrder);
        orderText.text = output;
    }
}

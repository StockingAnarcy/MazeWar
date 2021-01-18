using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Help : MonoBehaviour
{
    public GameObject introPanel;

    public GameObject Intro2;

    public GameObject Settings;
    public void SubmitChoice(string Choice)
    {
        if (Input.GetKeyDown(KeyCode.Return) || TouchScreenKeyboard.visible == false)
        {
            if (Choice.ToUpper().Equals("B"))
            {
                Intro2.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}

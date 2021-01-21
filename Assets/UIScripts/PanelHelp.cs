using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelHelp : MonoBehaviour
{
    public GameObject Intro2;
    public GameObject PanelSettings;

    TouchScreenKeyboard keyboard;
    public void SubmitChoice(string Choice)
    {
        if (Input.GetKeyDown(KeyCode.Return) || keyboard.status == TouchScreenKeyboard.Status.Done)
        {
            if (Choice.ToUpper().Equals("B"))
            {
                Intro2.SetActive(true);
                gameObject.SetActive(false);
            }
            if (Choice.ToUpper().Equals("/SETTINGS"))
            {
                PanelSettings.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}

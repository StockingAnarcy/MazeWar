using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelHelp : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject Intro2;
    public GameObject PanelSettings;


    public void SubmitChoice(string Choice)
    {
        if (Input.GetKeyDown(KeyCode.Return) || TouchScreenKeyboard.visible == false)
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

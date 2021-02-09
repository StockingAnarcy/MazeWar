using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroScreen2 : MonoBehaviour {

	public GameObject introPanel;

	public GameObject SelectPanelServer;
	public GameObject SelectPanelClient;
	public GameObject SelectPanelHelp;
	public GameObject SelectPanelSettings;

	TouchScreenKeyboard keyboard;

	public AudioClip tone;
	public AudioClip tone2;
	public AudioSource source;
	private void Start()
    {
		StartCoroutine("PlaySound");
	}
    void Update()
	{
		if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	IEnumerator PlaySound()
	{
		for (int i = 0; i < 1; i++)
		{
			source.PlayOneShot(tone);
			yield return new WaitForSeconds(0.2f);
			source.PlayOneShot(tone2);
		}
	}

			public void SubmitChoice(string Choice)
	{
		if (Input.GetKeyDown(KeyCode.Return) || TouchScreenKeyboard.visible == false)
		{
			if(Choice.ToUpper().Equals("C"))
			{
				Debug.Log("Creating New Server");
				SelectPanelServer.SetActive(true);
				gameObject.SetActive(false);
			}
			
			else if(Choice.ToUpper().Equals("J"))
			{
				Debug.Log("Creating New Client");
				SelectPanelClient.SetActive(true);
				gameObject.SetActive(false);
			}
			
			else if (Choice.ToUpper().Equals("O"))
			{
				Debug.Log("SinglePlayer");
				SceneManager.LoadScene(1);
			}
			
			else if (Choice.ToUpper().Equals("/SETTINGS"))
			{
				Debug.Log("Open Settings");
				SelectPanelSettings.SetActive(true);
				gameObject.SetActive(false);
			}

			else if (Choice.ToUpper().Equals("/HELP"))
			{
				Debug.Log("Open Help");
				SelectPanelHelp.SetActive(true);
				gameObject.SetActive(false);
			}

			else if (Choice.ToUpper().Equals("/EXT"))
			{
				Application.Quit();
			}
		}
	}

}

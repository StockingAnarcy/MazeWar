using UnityEngine;

public class ServerSetup : MonoBehaviour {

	public string PostString;

	public GameObject IntroScreen;
	
	TouchScreenKeyboard keyboard;
	void Update () 
	{
		if (Input.GetKey(KeyCode.Delete)||Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void SubmitName(string name)
	{
		if (Input.GetKeyDown(KeyCode.Return) || TouchScreenKeyboard.visible == false)
		{
			Debug.Log("Name Selected: " + name);
			FindObjectOfType<PlayerDetails>().name = name;
			FindObjectOfType<PlayerDetails>().hasName = true;
			FindObjectOfType<NetworkLogic>().playerName = name;
			FindObjectOfType<NetworkLogic>().SetupServer();
			gameObject.SetActive(false);
			IntroScreen.SetActive(false);
        }
	}
	
}

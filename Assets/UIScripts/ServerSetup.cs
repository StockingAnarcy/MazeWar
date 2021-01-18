using UnityEngine;

public class ServerSetup : MonoBehaviour {

	public string PostString;

	public GameObject IntroScreen;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.Delete))
		{
			Application.Quit();
		}

		if (Input.GetKey(KeyCode.Escape))
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

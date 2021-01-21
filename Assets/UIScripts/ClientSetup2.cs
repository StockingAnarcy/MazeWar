using UnityEngine;
using TMPro;

public class ClientSetup2 : MonoBehaviour {

	public string IP;
	public string Port;

	public string PostStr;

	public GameObject IntroScreen;

	TouchScreenKeyboard keyboard;
	public void Load()
	{
		GetComponent<TMP_Text>().text += IP +":" + Port + "\n"+PostStr;
	}

	void Update () 
	{
		if (Input.GetKey(KeyCode.Delete) || Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void SubmitName(string name)
	{
		if (Input.GetKeyDown(KeyCode.Return) || keyboard.status == TouchScreenKeyboard.Status.Done)
		{
			Debug.Log("Name Selected: " + name);
			FindObjectOfType<PlayerDetails>().name = name;
			FindObjectOfType<PlayerDetails>().hasName = true;
			FindObjectOfType<NetworkLogic>().networkAddress = IP;
			FindObjectOfType<NetworkLogic>().networkPort = ushort.Parse(Port);
			FindObjectOfType<NetworkLogic>().SetupClient();
			FindObjectOfType<NetworkLogic>().playerName = name;
			gameObject.SetActive(false);
			IntroScreen.SetActive(false);

			if (name.ToUpper().Equals("/EXT"))
			{
				Application.Quit();
			}
		}
		
	}
}

using UnityEngine;

public class ClientSetup1 : MonoBehaviour 
{
	public GameObject CSetup2;

	TouchScreenKeyboard keyboard;

	void Update () 
	{
		if (Input.GetKey(KeyCode.Delete) || Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void SubmitIP(string ip)
	{
		if (Input.GetKeyDown(KeyCode.Return) || keyboard.status == TouchScreenKeyboard.Status.Done)
		{
			Debug.Log("IP Address Selected: " + ip);
			CSetup2.GetComponent<ClientSetup2>().IP = ip;

			if (ip.ToUpper().Equals("/EXT"))
			{
			
				Application.Quit();
			}

		}
	}

	public void SubmitPort(string port)
	{
		if (Input.GetKeyDown(KeyCode.Return) || TouchScreenKeyboard.visible == false)
		{
			Debug.Log("Port Selected: " + port);
			CSetup2.SetActive(true);
			CSetup2.GetComponent<ClientSetup2>().Port = port;
			CSetup2.GetComponent<ClientSetup2>().Load();
			gameObject.SetActive(false);

			if (port.ToUpper().Equals("EXT"))
			{
				
				Application.Quit();
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;
public class IntroScreen : MonoBehaviour {

	public TMP_Text LoadText;
	public GameObject NextScene;
	public TMP_InputField inpt;
    
	void Start () 
	{
		StartCoroutine("LoadActions");
	}
	
	IEnumerator LoadActions()
	{
		for(int i=0; i< 3; i++)
		{
			LoadText.text = "";
			yield return new WaitForSeconds(0.4f);
			LoadText.text = ".";
			yield return new WaitForSeconds(0.4f);
			LoadText.text = "..";
			yield return new WaitForSeconds(0.4f);
			LoadText.text = "...";
			yield return new WaitForSeconds(0.4f);
		}
		NextScene.SetActive(true);
		EventSystem.current.SetSelectedGameObject(inpt.gameObject);
		gameObject.SetActive(false);

	}


}

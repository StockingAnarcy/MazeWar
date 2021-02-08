using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using TMPro;
public class IntroScreen : MonoBehaviour
{

	public TMP_Text LoadText;
	public GameObject NextScene;
	public TMP_InputField inpt;

	TMP_Text[] texts;
	Image[] images;

	public GameObject MCamera;
	public GameObject Camera;

	public Image map;

	Camera mcam;
	Camera cam;

	EdgeDetectionColor edge;

	public Material mat;
	public Material line;

	void Start()
	{
		texts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
		images = Resources.FindObjectsOfTypeAll<Image>();

		mcam = MCamera.GetComponent<Camera>();
		cam = Camera.GetComponent<Camera>();

		edge = MCamera.GetComponent<EdgeDetectionColor>();

		if (PlayerPrefs.HasKey("Color"))
		{
			if (PlayerPrefs.GetInt("Color") != 0)
			{
				LoadWhite();
			}
			else if (PlayerPrefs.GetInt("Color") != 1)
			{
				LoadBlack();
			}
		}
        else
        {
			LoadWhite();
        }

		StartCoroutine("LoadActions");
	}

	IEnumerator LoadActions()
	{
		for (int i = 0; i < 3; i++)
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

	void LoadWhite()
	{
		foreach (TMP_Text tcol in texts)
		{
			tcol.color = new Color(0, 0, 0);
		}

		foreach (Image icol in images)
		{
			var alpha = icol.color.a;
			icol.color = new Color(255, 255, 255, alpha);
		}

		map.color = Color.white;

		mcam.backgroundColor = new Color(255, 255, 255, 5);
		cam.backgroundColor = new Color(255, 255, 255);

		edge.edgesColor = Color.black;
		edge.edgesOnlyBgColor = Color.white;

		mat.color = Color.white;
		line.color = Color.black;


		PlayerPrefs.SetInt("Color", true ? 1 : 0);

	}

	void LoadBlack()
	{
		foreach (TMP_Text tcol in texts)
		{
			tcol.color = Color.green;
		}

		foreach (Image icol in images)
		{
			var alpha = icol.color.a;
			icol.color = new Color(0, 0, 0, alpha);
		}

		map.color = Color.green;

		mcam.backgroundColor = new Color(0, 0, 0, 5);
		cam.backgroundColor = new Color(0, 0, 0);

		edge.edgesColor = Color.green;
		edge.edgesOnlyBgColor = Color.black;

		mat.color = Color.black;
		line.color = Color.green;

		PlayerPrefs.SetInt("Color", false ? 1 : 0);
	}
}

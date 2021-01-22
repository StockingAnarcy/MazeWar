using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using TMPro;

public class SinglePlayerStart : MonoBehaviour {

	public GameObject IntroScreen;
	public GameObject MazeGen;
	public GameObject SPlayer;

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

	}

	void Update()
    {
        if (Input.GetKey(KeyCode.Delete)|| Input.GetKey(KeyCode.Escape))
        {
			SceneManager.LoadScene(0);
        }			
    }

    public void Submit(string name)
	{
		if (Input.GetKeyDown(KeyCode.Return)||TouchScreenKeyboard.visible==false)
		{
			IntroScreen.SetActive(false);
			MazeGen.GetComponent<MazeGenerator>().MazeSeed = Random.Range(0, 1000);
			MazeGen.SetActive(true);
			SPlayer.SetActive(true);
			SPlayer.GetComponent<SinglePlayerControl>().name = name;
			AIPlayer[] plrs = FindObjectsOfType<AIPlayer>();
			foreach(var p in plrs)
			{
				p.Init();
			}

			if (name.ToUpper().Equals("/EXT"))
			{
				Application.Quit();
			}
		}
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


	}
}

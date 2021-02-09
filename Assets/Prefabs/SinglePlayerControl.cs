using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SinglePlayerControl : MonoBehaviour {

	MazeGenerator gen;
	Maze m;

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

	int pX;
	int pY;

	public int score;
	public new string name;

	public LineRenderer r;

	public GameObject SpriteObj;
	public GameObject CamHolder;

	bool setup;

	public bool PlayerControl;

	public AudioClip Shot;
	public AudioClip Dead;

	void Start () 
	{
		gen = FindObjectOfType<MazeGenerator>();
	}

	public Vector2 pos()
	{
		return new Vector2(pX, pY);
	}

	public void CmdSetName(string nm)
	{
		name = nm;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m == null)
		{
			m = gen.currentMaze();
			RandomPlacement();
		}

		if(!setup && PlayerControl)
		{
			gameObject.tag = "MyPlayer";
			SpriteObj.SetActive(false);
			CamHolder.SetActive(true);
		}

#if UNITY_ANDROID
		if (Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if (t.phase == TouchPhase.Began)
			{
				//save began touch 2d point
				firstPressPos = new Vector2(t.position.x, t.position.y);
			}

			if (t.phase == TouchPhase.Ended)
			{
				//save ended touch 2d point
				secondPressPos = new Vector2(t.position.x, t.position.y);

				//create vector from the two points
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				//normalize the 2d vector
				currentSwipe.Normalize();

				//swipe upwards
				if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				{
					//Debug.Log("up swipe");
					CmdDoMovement(true, false, false, false);
				}
				//swipe down
				if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				{
					//Debug.Log("down swipe");
					CmdDoMovement(false, true, false, false);
				}
				//swipe left
				if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					//Debug.Log("left swipe");
					CmdDoMovement(false, false, true, false);
				}
				//swipe right
				if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					//Debug.Log("right swipe");
					CmdDoMovement(false, false, false, true);
				}

			}
			CheckSeen();
		}
#endif

		bool up = Input.GetKeyDown(KeyCode.UpArrow);
		bool down = Input.GetKeyDown(KeyCode.DownArrow);
		bool left = Input.GetKeyDown(KeyCode.LeftArrow);
		bool right = Input.GetKeyDown(KeyCode.RightArrow);
		if(!PlayerControl)
			return;
		if(up || down || left || right)
		{
			CmdDoMovement(up, down, left, right);
		}
		CheckSeen();
		
			
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Shoot();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene(0);
		}
	}

	public void Shoot()
    {
		StopCoroutine("LineShow");
		StartCoroutine("LineShow");
		CmdShoot();
	}

	void CheckSeen()
	{
		Ray r = new Ray(transform.position+(Vector3.up*1.38f), transform.TransformDirection(Vector3.forward)*50);
		Debug.DrawRay(transform.position+(Vector3.up*1.38f), transform.TransformDirection(Vector3.forward)*50,Color.blue,1);
		RaycastHit hit;
		if(Physics.Raycast(r, out hit, 50))
		{
			if(PlayerControl && (hit.collider.tag == "OtherPlayer" || hit.collider.tag == "MyPlayer"))
			{
				SinglePlayerControl opc = hit.collider.GetComponentInParent<SinglePlayerControl>();
				FindObjectOfType<ScoreSystem>().CurrentView = opc.name;
			}
			else if(PlayerControl)
				FindObjectOfType<ScoreSystem>().CurrentView = "";
		}
		else
			FindObjectOfType<ScoreSystem>().CurrentView = "";
	}

	public void CmdShoot()
	{
		StopCoroutine("LineShow");
		StartCoroutine("LineShow");
		Ray r = new Ray(transform.position+(Vector3.up*1.38f), transform.TransformDirection(Vector3.forward)*50);
		Debug.DrawRay(transform.position+(Vector3.up*1.38f), transform.TransformDirection(Vector3.forward)*50,Color.blue,1);
		RaycastHit hit;
		if (PlayerControl)
		{
			AudioSource Source = GetComponent<AudioSource>();
			Source.PlayOneShot(Shot);
		}
		if (Physics.Raycast(r, out hit, 50))
		{
			if(hit.collider.tag == "OtherPlayer" || hit.collider.tag == "MyPlayer")
			{
				SinglePlayerControl opc = hit.collider.GetComponentInParent<SinglePlayerControl>();
				changeScore(10);
				opc.RpcKill();
				opc.kill();
			}
		}
	}

    public void RpcKill()
    {
		if(GetComponentInChildren<InvertColorsEffect>() != null)
		{
			if (PlayerControl)
			{
				AudioSource Source = GetComponent<AudioSource>();
				Source.PlayOneShot(Dead);
			}
			GetComponentInChildren<InvertColorsEffect>().enabled = true;
        	Invoke("HideEffect", 0.15f);
		}
    }

    void HideEffect()
    {
		GetComponentInChildren<InvertColorsEffect>().enabled = false;
    }

	public void changeScore(int x)
	{
		score += x;
	}

	public bool justDied;

	public void kill()
	{
		RandomPlacement();
		if(!PlayerControl)
			justDied = true;
	}

	IEnumerator LineShow()
	{
		r.enabled = true;
		yield return new WaitForSeconds(0.1f);
		r.enabled = false;
	}

	public void RandomPlacement()
	{
		StopCoroutine("Place");
		StartCoroutine("Place");
	}

	IEnumerator Place()
	{
		while(m == null)
			yield return true;
		
		int x = 0;
		int y =0;
		while(m.GetCell(x,y).isWall)
		{
			x = Random.Range(1, (int)m.Dimensions().x);
			y = Random.Range(1, (int)m.Dimensions().y);
		}
		pX = x;
		pY = y;
		transform.position = new Vector3(x*1.5f, 0, y*1.5f);
		transform.localEulerAngles = new Vector3(0,Random.Range(0,4)*90, 0);
	}

	public void CmdDoMovement(bool up, bool back, bool left, bool right)
	{
		Vector2 fDir = new Vector2(0, 1);
		float ang = transform.localEulerAngles.y;
		if (ang < 100 && ang > 80)
			fDir = new Vector2(1, 0);
		else if (ang < 190 && ang > 170)
			fDir = new Vector2(0, -1);
		else if (ang < 280 && ang > 260)
			fDir = new Vector2(-1, 0);

		if (up)
		{
			int nX = pX + (int)fDir.x;
			int nY = pY + (int)fDir.y;
			if (!m.GetCell(nX, nY).isWall)
			{
				pX = nX;
				pY = nY;
				transform.position = new Vector3(nX * 1.5f, 0, nY * 1.5f);
			}
		}
		else if (back)
		{
			int nX = pX - (int)fDir.x;
			int nY = pY - (int)fDir.y;
			if (!m.GetCell(nX, nY).isWall)
			{
				pX = nX;
				pY = nY;
				transform.position = new Vector3(nX * 1.5f, 0, nY * 1.5f);
			}
		}
		else if (left)
		{
			transform.localEulerAngles += new Vector3(0, -90, 0);
		}
		else if (right)
		{
			transform.localEulerAngles += new Vector3(0, 90, 0);
		}
	}

	/*
	[Command]
	public void CmdUp()
	{
		Vector2 fDir = new Vector2(0, 1);
		float ang = transform.localEulerAngles.y;
		if (ang < 100 && ang > 80)
			fDir = new Vector2(1, 0);
		else if (ang < 190 && ang > 170)
			fDir = new Vector2(0, -1);
		else if (ang < 280 && ang > 260)
			fDir = new Vector2(-1, 0);

		int nX = pX + (int)fDir.x;
		int nY = pY + (int)fDir.y;
		if (!m.GetCell(nX, nY).isWall)
		{
			pX = nX;
			pY = nY;
			transform.position = new Vector3(nX * 1.5f, 0, nY * 1.5f);
		}
	}

	[Command]
	public void CmdDown()
	{
		Vector2 fDir = new Vector2(0, 1);
		float ang = transform.localEulerAngles.y;
		if (ang < 100 && ang > 80)
			fDir = new Vector2(1, 0);
		else if (ang < 190 && ang > 170)
			fDir = new Vector2(0, -1);
		else if (ang < 280 && ang > 260)
			fDir = new Vector2(-1, 0);

		int nX = pX - (int)fDir.x;
		int nY = pY - (int)fDir.y;
		if (!m.GetCell(nX, nY).isWall)
		{
			pX = nX;
			pY = nY;
			transform.position = new Vector3(nX * 1.5f, 0, nY * 1.5f);
		}
	}

	[Command]
	public void CmdLeft()
	{
		transform.localEulerAngles += new Vector3(0, -90, 0);
	}

	[Command]
	public void CmdRight()
	{
		transform.localEulerAngles += new Vector3(0, 90, 0);
	}*/
	
	
	bool notTaken(int x, int y)
	{
		SinglePlayerControl[] controls = GameObject.FindObjectsOfType<SinglePlayerControl>();
		foreach(var c in controls)
		{
			Vector2 v = c.pos();
			int cx = (int)v.x;
			int cy = (int)v.y;
			if(x == cx && y == cy)
				return false;
		}
		return true;
	}

}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameObj : MonoBehaviour {

	public TMP_Text nt;
	public TMP_Text st;
	public Image ig;
	
	public void Init(string nm, int s, bool inView)
	{
		nt.text = nm;
		st.text = ""+s;
		if(inView)
		{
			nt.color = Color.white;
			st.color = Color.white;
			ig.color = Color.black;
		}
	}
}

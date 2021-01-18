using UnityEngine;

public class FireBtn : MonoBehaviour
{
    public void ClickMP()
    {
        GameObject plr = GameObject.FindGameObjectWithTag("MyPlayer");
        PlayerControl ctrl = plr.GetComponent<PlayerControl>();
        if(ctrl.isLocalPlayer)
        {
            ctrl.Shoot();
            Debug.Log("Shot");
        }
        
    }

    public void ClickSP()
    {
        GameObject splr = GameObject.FindGameObjectWithTag("MyPlayer");
        SinglePlayerControl sctrl = splr.GetComponent<SinglePlayerControl>();
        if (sctrl.tag == "MyPlayer" && sctrl != null)
        {
            sctrl.Shoot();
            Debug.Log("Shot");
        }
    }
}

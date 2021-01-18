using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using TMPro;

public class Settings : MonoBehaviour
{
    public bool white;

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
        white = true;
        texts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        images = Resources.FindObjectsOfTypeAll<Image>();

        mcam = MCamera.GetComponent<Camera>();
        cam = Camera.GetComponent<Camera>();

        edge = MCamera.GetComponent<EdgeDetectionColor>();
    }

    public void LoadWhite()
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
    }

    public void LoadBlack()
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

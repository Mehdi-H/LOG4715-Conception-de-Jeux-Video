using UnityEngine;
using System.Collections;

public class drawPosition : MonoBehaviour
{
	//***Variables
	public Transform[] cars;

    Vector3[] screenPos;
    private GUIStyle style = null;
    Color[] col;

    void Start()
    {
		// Récupérer toutes les voitures :
		Transform Cars = GameObject.Find("Cars").GetComponent<Transform>() as Transform;
		cars = new Transform[Cars.childCount];
		int count = 0;
		foreach(Transform car in Cars)
		{
			cars[count++] = car;
		}

		col = new Color[cars.Length];
		screenPos = new Vector3[cars.Length];

		// Récupérer leur couleur :
		for (int i = 0; i < cars.Length; i++)
		{
			MeshRenderer meshrend = cars[i].FindChild("SkyCar").FindChild("vehicle_skyCar_body_paintwork").GetComponent<MeshRenderer>() as MeshRenderer;
			col[i] = new Color(meshrend.materials[1].color.r, meshrend.materials[1].color.g, meshrend.materials[1].color.b);
		}
    }

    //***Actualisation de la position du joueur vis à vis de la minimap
    void Update()
    {
		for (int i = 0; i < cars.Length; i++)
		{
			screenPos[i] = camera.WorldToScreenPoint(cars[i].position);
		}
    }

    //***Dessin de l'icône du joueur sur la minimap
    void OnGUI()
    {
		for (int i = 0; i < cars.Length; i++)
		{
			// Debug.Log(cars[i].transform.name + " : " + col[i]);
			GUIStyle style = new GUIStyle(GUI.skin.box);
			style.normal.background = remplissage(1, 1, col[i]);
			// StyleIcon(col[i]);
			GUI.Box(new Rect(screenPos[i].x, Screen.height - screenPos[i].y, 0.5f, 0.5f), GUIContent.none, style);
		}
    }

    //***Stylisation de l'icône
    private void StyleIcon(Color c)
    {
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.box);
            style.normal.background = remplissage(2, 2, c);
        }
    }

    //***remplissage pixel par pixel de l'icone qui représente le joueur sur la minimap
    private Texture2D remplissage(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D icon = new Texture2D(width, height);
        icon.SetPixels(pix);
        icon.Apply();
        return icon;
    }
}

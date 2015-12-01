using UnityEngine;
using System.Collections;

public class drawPosition : MonoBehaviour
{
    //***Variables
    public Transform joueur;
    public Transform Cars;
    Vector3 screenPos;
    private GUIStyle style = null;
    Color col;

    void Start()
    {

        foreach(Transform car in Cars)
        {
            Debug.Log(car.name);
        }

        Transform[] children = joueur.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "vehicle_skyCar_body_paintwork")
            {
                MeshRenderer mesh = child.GetComponent<MeshRenderer>();
                foreach(Material m in mesh.materials)
                {
                    if(m.name != "skyCar_plastics")
                    {
                        col = m.color;
                    }
                }
            }
        }
    }

    //***Actualisation de la position du joueur vis à vis de la minimap
    void Update()
    {
        screenPos = camera.WorldToScreenPoint(joueur.position);
    }

    //***Dessin de l'icône du joueur sur la minimap
    void OnGUI()
    {
        StyleIcon(col);
        GUI.Box(new Rect(screenPos.x, Screen.height - screenPos.y, (float)0.5, (float)0.5), GUIContent.none, style);
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

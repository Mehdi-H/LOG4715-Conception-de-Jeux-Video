using UnityEngine;
using System.Collections;

public class Virage : MonoBehaviour
{
	private DirectionInterface virage;

	[SerializeField]
	private string virageDirection;

	private string direction = "";



	// Use this for initialization
	void Start()
	{
		virage = GameObject.Find("VirageIndicator").GetComponent<DirectionInterface>() as DirectionInterface;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.transform.parent.parent != null && col.transform.parent.parent.name == "Joueur 1")
		{
			virage.setText(virageDirection);
		}
	}

	void OnTriggerExit(Collider col)
	{

		if (col.transform.parent.parent != null && col.transform.parent.parent.name == "Joueur 1")
		{
			virage.setText("");
		}

	}
}

using UnityEngine;
using System.Collections;

public class DirectionInterface : MonoBehaviour
{
	private string direction;
	
	void Start ()
	{
		direction = "";
	}
	
	public void setText(string text)
	{
		GetComponent<GUIText>().text = text;
	}
}

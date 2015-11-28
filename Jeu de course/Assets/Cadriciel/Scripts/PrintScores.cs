using UnityEngine;
using System.Collections;

public class PrintScores : MonoBehaviour
{	
	void Start ()
	{
		GameObject score = GameObject.Find("ScoreData") as GameObject;
		ScoreData scoreData = score.GetComponent<ScoreData>() as ScoreData;

		GUIText gui = gameObject.GetComponent<GUIText>() as GUIText;

		// === Afficher les joueurs et leur position et le temps du premier ===

		string time = scoreData.getTimeForHumans();
		string[] cars = scoreData.getCarsInOrder();

		for (int i = 0; i < cars.Length; i++)
		{
			gui.text += (i+1).ToString() + ".  " + cars[i] + (i == 0 ? "             " + time : "") + "\n";
		}

		Debug.Log("PremierPourEtreSur : " + scoreData.getFirstToBeSure());
	}
}

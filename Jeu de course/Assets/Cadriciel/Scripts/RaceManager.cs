using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System;

public class RaceManager : MonoBehaviour 
{
	[SerializeField]
	private GameObject _carContainer;

	[SerializeField]
	private GUIText _announcement;

	[SerializeField]
	private int _timeToStart;

	[SerializeField]
	private int _endCountdown;

	private Stopwatch watch;

	// Use this for initialization
	void Awake () 
	{
		CarActivation(false);
		watch = new Stopwatch();
	}
	
	void Start()
	{
		StartCoroutine(StartCountdown());
	}

	IEnumerator StartCountdown()
	{
		int count = _timeToStart;
		do 
		{
			_announcement.text = count.ToString();
			yield return new WaitForSeconds(1.0f);
			count--;
		}
		while (count > 0);
		_announcement.text = "Partez!";
		CarActivation(true);
		watch.Start();
		yield return new WaitForSeconds(1.0f);
		_announcement.text = "";
	}

	public void EndRace(string winner)
	{
		StartCoroutine(EndRaceImpl(winner));
	}

	IEnumerator EndRaceImpl(string winner)
	{
		CarActivation(false);
		watch.Stop();
		_announcement.fontSize = 20;
		int count = _endCountdown;
		do 
		{
			_announcement.text = "Victoire: " + winner + " en premiere place. Retour au titre dans " + count.ToString();
			yield return new WaitForSeconds(1.0f);
			count--;
		}
		while (count > 0);

		// === Fin de la course ===

		// --- Récupérer les positions des joueurs ---

		GameObject gameManager = GameObject.Find("Game Manager") as GameObject;
		CheckpointManager cpManager = gameManager.GetComponent<CheckpointManager>() as CheckpointManager;
		CarController[] orderedCars = cpManager.getCarsInOrder(new List<string>(new string[] {"Joueur 2"}));

		// --- Stocker positions et temps du premier dans ScoreData ---

		GameObject score = GameObject.Find("ScoreData") as GameObject;
		ScoreData scoreData = score.GetComponent<ScoreData>() as ScoreData;

		scoreData.setFirstToBeSure(winner);
		scoreData.setCarsInOrder(orderedCars);
		scoreData.setTimeFirst(TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds));
		
		// --- Lancer la scène de fin ---

		Application.LoadLevel("endScreen");
	}

	public void Announce(string announcement, float duration = 2.0f)
	{
		StartCoroutine(AnnounceImpl(announcement,duration));
	}

	IEnumerator AnnounceImpl(string announcement, float duration)
	{
		_announcement.text = announcement;
		yield return new WaitForSeconds(duration);
		_announcement.text = "";
	}

	public void CarActivation(bool activate)
	{
		foreach (CarAIControl car in _carContainer.GetComponentsInChildren<CarAIControl>(true))
		{
			car.enabled = activate;
		}
		
		foreach (CarUserControlMP car in _carContainer.GetComponentsInChildren<CarUserControlMP>(true))
		{
			car.enabled = activate;
		}

	}

	public GameObject getCarContainer() {
		return _carContainer;
	}
}

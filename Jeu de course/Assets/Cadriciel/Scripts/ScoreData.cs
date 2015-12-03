using UnityEngine;
using System.Collections;
using System;

public class ScoreData : MonoBehaviour
{
	// ==========================================
	// == Attributes
	// ==========================================

	private string firstToBeSure;
	private string[] carsInOrder;
	private TimeSpan ts;

	// ==========================================
	// == Start
	// ==========================================

	void Start ()
	{

	}

	// ==========================================
	// == Setters
	// ==========================================

	public void setFirstToBeSure(string first)
	{
		this.firstToBeSure = first;
    }

	public void setCarsInOrder(CarController[] carsInOrder)
	{
		string tmp = null;
		this.carsInOrder = new string[carsInOrder.Length];

		for (int i = 0; i < carsInOrder.Length; i++)
		{
			if (i == 0 && !carsInOrder[i].name.Equals(firstToBeSure))
			{
				this.carsInOrder[i] = firstToBeSure;
				tmp = carsInOrder[i].name;
			}
			else if (tmp != null && carsInOrder[i].name.Equals(firstToBeSure))
			{
				this.carsInOrder[i] = tmp;
			}
			else
			{
				this.carsInOrder[i] = carsInOrder[i].name;
			}
		}
	}

	public void setTimeFirst(TimeSpan ts)
	{
		this.ts = ts;
	}

	// ==========================================
	// == Getters
	// ==========================================

	public string[] getCarsInOrder()
	{
		return this.carsInOrder;
	}

	public string getFirstToBeSure()
	{
		return this.firstToBeSure;
    }

	public string getTimeForHumans()
	{
		string temps = ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds;
		return temps;
	}
}

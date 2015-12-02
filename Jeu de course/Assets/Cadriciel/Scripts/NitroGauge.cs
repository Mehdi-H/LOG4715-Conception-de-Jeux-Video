using UnityEngine;
using System.Collections;

public class NitroGauge : MonoBehaviour
{
	// ==========================================
	// == Attributs
	// ==========================================

	[SerializeField]
	private float progress = 100;

	[SerializeField]
	private float nitroForce = 50;

	[SerializeField]
	private GUITexture _nitro;

	private CarFX fx;
	private bool flag; // évite d'appeler fx.enableFX à chaque frame quand il n'y a pas de changement

	// ==========================================
	// == Start
	// ==========================================

	void Start ()
	{
		this.enabled = false;
		StartCoroutine (enableNitro());

		fx = GetComponent<CarFX>() as CarFX;
	}

	IEnumerator enableNitro()
	{
		// Attendre le début de la course pour activer la nitro :

		GameObject gm = GameObject.Find("Game Manager") as GameObject;
		RaceManager rm = gm.GetComponent<RaceManager>() as RaceManager;
		yield return new WaitForSeconds(rm.getTimeToStart());

		this.enabled = true;
	}

	// ==========================================
	// == Update
	// ==========================================

	void Update () {

		if (progress >= 100)
			progress = 100;

		if (progress <= 0)
			progress = 0;

		if (Input.GetButton("Nitro") && progress > 10)
		{
			transform.rigidbody.AddForce(transform.forward * nitroForce);
			progress -= 3;

			if (!flag)
			{
				fx.enableFX(CarFX.FX.NITRO, true);
				flag = true;
			}
		}
		else if (flag)
		{
			fx.enableFX(CarFX.FX.NITRO, false);
			flag = false;
		}

		if (Input.GetButton("Nitro") && progress <= 10) {
			progress -= 2;
		}


		progress += 0.5f;

		_nitro.pixelInset = new Rect(_nitro.pixelInset.x, _nitro.pixelInset.y, progress, _nitro.pixelInset.height);
	}
}


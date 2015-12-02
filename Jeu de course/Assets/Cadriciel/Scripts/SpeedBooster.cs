using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class SpeedBooster : MonoBehaviour
{
	[SerializeField]
	private float boostForce = 100.0f;

	private Transform _joueur;
	private Stopwatch _watch;
	[SerializeField] private long _fireDurationMs = 2000;

	private CarFX fx;

	// Use this for initialization
	void Start ()
	{
		
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.attachedRigidbody != null && col.attachedRigidbody.tag == "Player")
		{
			// Récupérer le manager d'effets de la voiture :

			fx = col.gameObject.GetComponentInParent<CarFX>() as CarFX;

			// Ajouter une impulsion au joueur :

			_joueur = col.attachedRigidbody.transform;
			_joueur.rigidbody.AddForce(_joueur.forward * boostForce, ForceMode.Impulse);

			// Ajouter une courte traînée de feu derrière le joueur :

			fx.enableFX(CarFX.FX.BOOST, true, 2000);
		}
	}
}
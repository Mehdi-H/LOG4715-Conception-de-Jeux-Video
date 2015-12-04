using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class SkeletonAttackManager : MonoBehaviour
{
	private enum Groupe { GAUCHE, DROITE };

	// ==========================================
	// == Attributs
	// ==========================================

	[SerializeField]
	private long attenteDebutMS = 3000,
		decalageGroupeMS = 5000;

	private bool go = false, attacking = false;
	private Groupe dernierAttaquant = Groupe.DROITE;

	private Animation[] gauche, droite;

	private Stopwatch watch;

	// ==========================================
	// == Start
	// ==========================================

	void Start ()
	{
		watch = new Stopwatch();
		watch.Start();

		gauche = transform.FindChild("Groupe Gauche").GetComponentsInChildren<Animation>();
		droite = transform.FindChild("Groupe Droite").GetComponentsInChildren<Animation>();
	}

	// ==========================================
	// == Update
	// ==========================================

	void Update ()
	{
		// === Temps d'attente au début de la course avant de lancer les animations ===

		if (!go && watch.ElapsedMilliseconds > attenteDebutMS)
		{
			go = true;
			watch.Stop();
			watch.Reset();

			StartCoroutine(attack(gauche));

			UnityEngine.Debug.Log("0. On peut attaquer");
		}

		// === Lancer des attaques en boucle ===

		if (go)
		{
			// Pause entre 2 attaques finie :
			if (!attacking && watch.ElapsedMilliseconds > decalageGroupeMS)
			{
				UnityEngine.Debug.Log("3. Attaque incomming : " + (dernierAttaquant == Groupe.GAUCHE ? Groupe.DROITE : Groupe.GAUCHE));

				Animation[] prochainAttaquant = (dernierAttaquant == Groupe.GAUCHE ? droite : gauche);
				dernierAttaquant = (dernierAttaquant == Groupe.GAUCHE ? Groupe.DROITE : Groupe.GAUCHE);

				StartCoroutine(attack(prochainAttaquant));
				watch.Stop();
				watch.Reset();
			}
		}
	}

	IEnumerator attack(Animation[] gr)
	{
		UnityEngine.Debug.Log("1. À l'attaque !");

		attacking = true;

		for (int i = 0; i < gr.Length; i++)
		{
			// Sort du sol :
			gr[i].Play("OffGround");

			// Attaque :
			gr[i].PlayQueued("attack", QueueMode.CompleteOthers);

			// Rentre dans le sol :
			gr[i].PlayQueued("InGround", QueueMode.CompleteOthers);
		}

		float count = 5.5f;
		while (count > 0)
		{
			count -= 0.5f;
			UnityEngine.Debug.Log("   count : " + count);
            yield return new WaitForSeconds(0.5f);
		}

		UnityEngine.Debug.Log("2. Attaque terminée ");
		attacking = false;
		watch.Start();
	}
}

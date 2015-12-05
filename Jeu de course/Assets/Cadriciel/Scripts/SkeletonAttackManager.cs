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
	private int attenteDebutSec = 3,
		decalageGroupeSec = 5;

	private bool go = false, attacking = false;
	private Groupe dernierAttaquant = Groupe.GAUCHE;

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

		if (!go)
		{
			if (watch.ElapsedMilliseconds > (attenteDebutSec * 1000))
			{
				go = true;
				watch.Stop();
				watch.Reset();

				StartCoroutine(attack(gauche));
			}
		}

		// === Lancer des attaques en boucle ===

		else // go == true
		{
			// Pause entre 2 attaques finie :
			if (!attacking && watch.ElapsedMilliseconds > (decalageGroupeSec * 1000))
			{
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
		attacking = true;

		for (int i = 0; i < gr.Length; i++)
		{
			// Sort du sol :
			gr[i].Play("offground");

			// Attaque :
			gr[i].PlayQueued("attack", QueueMode.CompleteOthers);

			// Rentre dans le sol :
			gr[i].PlayQueued("inground", QueueMode.CompleteOthers);
		}

		int count = 6;
		while (count > 0)
		{
			count -= 1;
            yield return new WaitForSeconds(1);
		}
		
		attacking = false;
		watch.Start();
	}
}

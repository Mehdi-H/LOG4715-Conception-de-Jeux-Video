using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class CarFX : MonoBehaviour
{
	// ==========================================
	// == Enum des FX possibles
	// ==========================================

	public enum FX {
		NITRO,
		BOOST
	};

	// ==========================================
	// == Structure d'un FX
	// ==========================================

	private struct FX_struct
	{
		// ------------------------------------------
		// -- Attributs
		// ------------------------------------------

		public GameObject obj;
		public bool enabled, flag;
		public FX type;
		public Stopwatch watch;
		public long duration;

		// ------------------------------------------
		// -- Constructeur
		// ------------------------------------------

		public FX_struct(GameObject model, FX type)
		{
			duration = 0;
			watch = new Stopwatch();
			this.type = type;
			flag = false;

			// Chargement de l'effet :
			// GameObject model = Resources.Load(modelName) as GameObject;

			// Instantiation de l'effet :
			obj = Instantiate(model) as GameObject;

			// Désactivation du rendu de l'effet :
			enabled = false;
			setRenderer();
		}

		// ------------------------------------------
		// -- Méthodes
		// ------------------------------------------

		public void followCar(Transform car)
		{
			if (enabled)
			{
				if (type == FX.NITRO || (type == FX.BOOST && watch.ElapsedMilliseconds < duration))
					obj.transform.position = car.position;
				else {
					enabled = false;
					watch.Stop();
					watch.Reset();
                }
			}

			// Mettre à jour l'affichage de l'effet s'il y a eu changement :
			if (enabled != flag)
				setRenderer();
		}

		public void setRenderer()
		{
			// UnityEngine.Debug.Log("Let me enable (" + enabled + ") this renderer for you, kind sir.");

			Renderer[] children = obj.GetComponentsInChildren<Renderer>() as Renderer[];

			for (int i = 0; i < children.Length; i++)
			{
				children[i].enabled = enabled;
			}

			flag = enabled;
		}
	}

	// ==========================================
	// == Attributs
	// ==========================================

	[SerializeField]
	private GameObject nitroFireModel, boostFireModel;

	private FX_struct nitroFX, boostFX;

	// ==========================================
	// == Start
	// ==========================================

	void Start ()
	{
		nitroFX = new FX_struct(nitroFireModel, FX.NITRO);
		boostFX = new FX_struct(boostFireModel, FX.BOOST);
	}

	// ==========================================
	// == Update
	// ==========================================

	void Update ()
	{
		nitroFX.followCar(transform);
		boostFX.followCar(transform);
	}

	// ==========================================
	// == Méthodes
	// ==========================================

	public void enableFX(FX fxName, bool enable, long durationMS)
	{
		// UnityEngine.Debug.Log("Let me enable (" + enable + ") this (" + fxName + ") for you, kind sir.");

		switch (fxName)
		{
			case FX.NITRO:
				nitroFX.enabled = enable;
				break;

			case FX.BOOST:
				boostFX.enabled = enable;
				boostFX.duration = durationMS;
				boostFX.watch.Start();
				break;

			default:
				UnityEngine.Debug.Log("FX inconnu");
				return;
		}
	}

	public void enableFX(FX fxName, bool enable)
	{
		enableFX(fxName, enable, 0);
	}
}

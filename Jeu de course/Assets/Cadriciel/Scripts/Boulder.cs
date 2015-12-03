using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour
{
	// ==========================================
	// == Attributs
	// ==========================================

	private Vector3 _initialPosition;

	[SerializeField]
	private int _initialDownImpulse = 10;
	

	// ==========================================
	// == Start
	// ==========================================

	void Start ()
	{
		_initialPosition = transform.position;
		launch();
	}

	void launch()
	{
		transform.position = _initialPosition;
		rigidbody.velocity = Vector3.zero;
		rigidbody.AddForce(Vector3.down * _initialDownImpulse, ForceMode.Impulse);
	}

	// ==========================================
	// == Update
	// ==========================================

	void Update ()
	{
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.transform.parent != null && col.transform.parent.name == "Tourelle")
		{
			launch();
		}
	}
}

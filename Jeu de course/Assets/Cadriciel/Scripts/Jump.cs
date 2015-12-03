using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
	// ==========================================
	// == Attributs
	// ==========================================

	[SerializeField]
	private float _jumpForce = 20.0f;

	[SerializeField]
	private float _airControlRotation = 3.0f;

	private bool grounded, flag;

	// ==========================================
	// == FixedUpdate
	// ==========================================

	void FixedUpdate ()
	{
		// === Sauter ===

		if (Input.GetButtonDown("Jump") && grounded)
		{
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
			rigidbody.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
		}

		// === Air Control ===

		if (!grounded)
		{  
			// Tourner horizontalement (comme un virage à plat) :
			if (Input.GetAxis("VirageAerien") > 0) {
				transform.Rotate(0.0f, _airControlRotation, 0.0f);
			}
			if (Input.GetAxis("VirageAerien") < 0) {
				transform.Rotate(0.0f, -_airControlRotation, 0.0f);
			}

			// Flip vertical (salto avant/arrière) :
			if (Input.GetAxis("Vertical") > 0) {
				transform.Rotate(_airControlRotation, 0.0f, 0.0f);
			}
			if (Input.GetAxis("Vertical") < 0) {
				transform.Rotate(-_airControlRotation, 0.0f, 0.0f);
			}

			// Tonneau gauche/droite :
			if (Input.GetAxis("Horizontal") > 0)
			{
				transform.Rotate(0.0f, 0.0f, -_airControlRotation);
			}
			if (Input.GetAxis("Horizontal") < 0)
			{
				transform.Rotate(0.0f, 0.0f, _airControlRotation);
			}
		}
    }

	// ==========================================
	// == Vérifier si le joueur est au sol
	// ==========================================

	void OnCollisionEnter(Collision col)
	{
		if (col.transform.root.name == "Terrain" || col.transform.root.name == "Grounded Material")
		{
			flag = true;
			grounded = true;
		}
	}

	void OnCollisionExit(Collision col)
	{
		if (col.transform.root.name == "Terrain" || col.transform.root.name == "Grounded Material")
		{
			flag = false;
			StartCoroutine(delayedGrounding(0.3f, false));
		}
	}

	IEnumerator delayedGrounding(float delay, bool isGrounded)
	{
		Debug.Log("En instance de dé-solage");
		yield return new WaitForSeconds(delay);
		if (!flag)
		{
			grounded = isGrounded;
			Debug.Log("Dé-solé : " + grounded);
		}
		else
		{
			Debug.Log("Désolé je te dé-sole pas : " + grounded);
		}
	}
}

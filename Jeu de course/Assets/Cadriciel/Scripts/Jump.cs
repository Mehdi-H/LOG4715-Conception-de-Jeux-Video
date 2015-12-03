﻿using UnityEngine;
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
		if (flag)
			return; // collision avec un mur

		if (col.gameObject.name == "Track" || col.transform.root.name == "Grounded Material")
		{
			flag = true;
			grounded = true;
			Debug.Log("Solé : " + grounded);
		}
	}

	void OnCollisionExit(Collision col)
	{
		if (!flag)
			return;

		if (col.gameObject.name == "Track" || col.transform.root.name == "Grounded Material")
		{
			flag = false;
			StartCoroutine(delayedGrounding(0.3f, false));
		}
	}

	IEnumerator delayedGrounding(float delay, bool isGrounded)
	{
		Debug.Log("Ah ? ...");

		float count = 0;
		while (!flag && count < delay)
		{
			count += 0.1f;
			yield return new WaitForSeconds(0.1f);
		}
		
		if (!flag)
		{
			grounded = isGrounded;
			Debug.Log("... en l'air ! (" + grounded + ")");
		}
		else
		{
			Debug.Log("...à terre ! (" + grounded + ")");
		}
	}
}

using UnityEngine;
using System.Collections;

public class AnimationWorkaround : MonoBehaviour
{
	private Vector3 initPos;

	private Transform bip01;
	private Animation animation;

	void Start ()
	{
		bip01 = transform.FindChild("Bip01").GetComponent<Transform>() as Transform;
		initPos = bip01.position;
		animation = GetComponent<Animation>() as Animation;
	}
	
	void Update ()
	{
		if (! animation.isPlaying)
		{
			// Rester sous terre :
			bip01.position = initPos;
		}
	}
}

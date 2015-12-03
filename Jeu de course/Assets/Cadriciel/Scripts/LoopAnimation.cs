using UnityEngine;
using System.Collections;

public class LoopAnimation : MonoBehaviour
{
	[SerializeField]
	private string animationName = "dance";

	private Animation _animation;
	private bool isRewinding = false;

	void Start ()
	{
		_animation = GetComponent<Animation>();
	}
	
	void Update ()
	{
		if (!_animation.IsPlaying(animationName))
		{
			Debug.Log("recommence");
			_animation.Play(animationName);
		}
	}
}

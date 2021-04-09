﻿using UnityEngine;
using System.Collections;

public class ScaleObject : MonoBehaviour 
{
	#region Inspector Members
	[Header("Settings")]
	[Tooltip("Scale animation curve")]
	[SerializeField] private AnimationCurve curve;

	[Tooltip("Scale animation duration")]
	[SerializeField] private float duration;

	[Tooltip("Animation loop state")]
	[SerializeField] private bool loop;

	[Tooltip("Play animation on start")]
	[SerializeField] private bool onStart;
	#endregion

	#region Private Members
	private float timeCounter;		// Animation time counter
	private bool isPlaying;			// Animation current is playing state
	#endregion

	#region Main Methods
	private void Start()
	{
		// Initialize values
		timeCounter = 0f;
		if (!isPlaying) isPlaying = onStart;
	}

	private void Update()
	{
		if (isPlaying)
		{
			// Update local scale based on animation curve
			transform.localScale = Vector3.one*curve.Evaluate(timeCounter/duration);

			// Update time counter
			timeCounter += Time.deltaTime;

			if (timeCounter > duration)
			{
				if (loop) timeCounter = 0f;
				else
				{
					transform.localScale = Vector3.one*curve.Evaluate(1f);
					isPlaying = false;
				}
			}
		}
	}
	#endregion

	#region Scale Methods
	public void Play()
	{
		// Reset time counter to start animation
		timeCounter = 0f;
		isPlaying = true;
	}
	#endregion

	#region Properties
	public bool IsPlaying
	{
		get { return isPlaying; }
	}
	#endregion
}

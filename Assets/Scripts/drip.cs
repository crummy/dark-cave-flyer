using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drip : MonoBehaviour {

	public float timeBetweenDrips = 3000f;
	public float variance = 1000f;

	Vector2 originalPosition;
	System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
	float timeUntilNextDrip;
	bool isDripping;
	SpriteRenderer sprite;
	Rigidbody2D body;
	AudioSource splash;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		body = GetComponent<Rigidbody2D> ();
		splash = GetComponent<AudioSource> ();
		originalPosition = transform.position;
		startWaitingForNextDrip ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDripping && stopwatch.ElapsedMilliseconds > timeUntilNextDrip) {
			isDripping = true;
			sprite.enabled = true;
			transform.position = originalPosition;
		}
	}

	void OnCollisionEnter2D() {
		splash.Play ();
		startWaitingForNextDrip ();
	}

	void startWaitingForNextDrip ()
	{
		isDripping = false;
		sprite.enabled = false;
		body.velocity = Vector2.zero;
		stopwatch.Reset ();
		stopwatch.Start ();
		timeUntilNextDrip = timeBetweenDrips + (variance / 2 + Random.value * variance);
	}
}

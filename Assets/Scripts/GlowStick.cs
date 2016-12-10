using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowStick : MonoBehaviour {

	enum State {
		growing,
		glowing
	}

	public float flicker = 0.1f;
	public int flickerIntervalMs = 60;
	public int timeToGlow = 1000;

	System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
	Light glow;
	float initialIntensity;
	State state = State.growing;

	// Use this for initialization
	void Start () {
		stopwatch.Start ();
		glow = GetComponent<Light> ();
		initialIntensity = glow.intensity;
		glow.intensity = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.growing) {
			glow.intensity = initialIntensity - initialIntensity * Mathf.Pow(0.99f, (float)stopwatch.ElapsedMilliseconds);
			if (stopwatch.ElapsedMilliseconds > timeToGlow) {
				state = State.glowing;
			}
		} else if (stopwatch.ElapsedMilliseconds > flickerIntervalMs) {
			float randomFlicker = Random.value * flicker;
			glow.intensity = initialIntensity + (flicker/2 + randomFlicker);
			stopwatch.Reset ();
			stopwatch.Start ();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhaust : MonoBehaviour {
	public Vector3 offset;
	public Vector2 variance;
	public GameObject particle;
	public float particleLifetime = 0.5f;
	public float ejectionIntervalMs = 100f;

	List<GameObject> exhaustParticles;
	Rigidbody2D body;
	System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

	// Use this for initialization
	void Start () {
		this.body = GetComponent<Rigidbody2D> ();
		exhaustParticles = new List<GameObject>();
		stopwatch.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W) && stopwatch.ElapsedMilliseconds > ejectionIntervalMs) {
			fire ();
			stopwatch.Reset ();
			stopwatch.Start ();
		}	
	}

	void fire ()
	{
		GameObject exhaust = Instantiate (particle);
		exhaustParticles.Add (exhaust);
		exhaust.transform.position = transform.position + Quaternion.Euler(0, 0, body.rotation) * offset;
		exhaust.GetComponent<Rigidbody2D> ().velocity = Quaternion.Euler (0, 0, body.rotation) * Vector2.down;
		Vector2 randomness = new Vector2 (Random.value * variance.x, Random.value * variance.y);
		exhaust.GetComponent<Rigidbody2D> ().velocity += (-variance/2) + randomness;
		Destroy (exhaust, particleLifetime);
	}
		
}

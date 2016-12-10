using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

	public float rotationTorque = 2;
	public float boostForce = 100;
	public float maxRotation = 30;

	[Range(0,1)] public float rotationSlowing = 0.9f;


	Rigidbody2D body;
	AudioSource thrustNoise;
	AudioSource crashNoise;

	void Start () {
		body = GetComponent<Rigidbody2D> ();
		crashNoise = GetComponents<AudioSource> ()[1];
		thrustNoise = GetComponents<AudioSource> ()[0];
		thrustNoise.loop = true;
		thrustNoise.Play ();
	}
	
	void FixedUpdate () {
		getInput ();
		restrictRotation ();
		restrictVelocities ();
	}

	void getInput ()
	{
		if (Input.GetKey (KeyCode.A)) {
			body.AddTorque (rotationTorque * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.D)) {
			body.AddTorque (-rotationTorque * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.W)) {
			body.AddForce (body.transform.up * boostForce * Time.deltaTime);
			thrustNoise.volume = 1;
		} else {
			thrustNoise.volume *= 0.9f;
		}
	}

	void restrictRotation ()
	{
		float angle = Mathf.DeltaAngle (body.rotation, 0);

		if (angle < -maxRotation) {
			float angleExceededBy = Mathf.DeltaAngle (angle, -maxRotation);
			body.AddTorque (-angleExceededBy * Time.deltaTime);
		} else if (angle > maxRotation) {
			float angleExceededBy = Mathf.DeltaAngle (angle, maxRotation);
			body.AddTorque (-angleExceededBy * Time.deltaTime);
		}
	}

	void restrictVelocities ()
	{
		body.angularVelocity *= 0.9f;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		crashNoise.volume = collision.relativeVelocity.magnitude * 0.2f;
		crashNoise.Play ();
	}
}

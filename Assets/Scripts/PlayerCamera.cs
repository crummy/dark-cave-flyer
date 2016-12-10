using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public Rigidbody2D player;
	public float followSpeed = 0.1f;
	public float zoom = 3f;
	public float directionOffset;
	public float velocityOffset;
	public float tiltFactor = 0.05f;

	Vector3 offset;
	Vector3 lookOffset;

	// Use this for initialization
	void Start () {
		transform.position = player.position;
		offset = Vector3.zero;
		lookDown ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		offset = player.velocity * velocityOffset;
		transform.position = Vector3.Lerp (transform.position, player.transform.position + offset + lookOffset, followSpeed);
		transform.position = new Vector3(transform.position.x, transform.position.y, -10);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis (-player.rotation * tiltFactor, Vector3.back), followSpeed);
		Camera.main.orthographicSize = zoom + player.velocity.magnitude * 0.1f;
	}

	void lookDown() {
		lookOffset = new Vector3 (0, -directionOffset, 0);
	}

	void lookUp() {
		lookOffset = new Vector3 (0, directionOffset, 0);
	}
}

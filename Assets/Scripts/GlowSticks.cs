using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowSticks : MonoBehaviour {

	public GameObject glowStick;
	[Range(0, 10)] public int glowStickCount = 5;
	public Vector3 glowStickSpawnOffset;
	public Rigidbody2D body;

	// Use this for initialization
	void Start () {	
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && glowStickCount > 0) {
			GameObject newStick = Instantiate (glowStick, transform.position + glowStickSpawnOffset, Quaternion.identity);
			newStick.GetComponent<Rigidbody2D> ().AddTorque(body.rotation * 0.01f);
			glowStickCount--;
		}
	}
}

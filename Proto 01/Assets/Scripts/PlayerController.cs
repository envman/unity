using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float speed = 10.0f;

	public Material seen;
	public float seenTime;
	private Material existing;

	void Start () {
		existing = GetComponent<Renderer>().material;
	}

	void Update () {
		float translation = Input.GetAxis("Vertical") * speed;
		float strafe = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		strafe *= Time.deltaTime;

		transform.Translate(strafe, 0, translation);
	}

	void LateUpdate() {
		if (seenTime > 0 && seenTime + .5f > Time.time) {
			GetComponent<Renderer>().material = seen;
		} else {
			GetComponent<Renderer>().material = existing;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float speed = 10.0f;
	// private Rigidbody rb;
	public Material seen;

	private Material existing;
	// private Renderer renderer;

	void Start () {
		// remderer = GetComponent<Renderer>();
		existing = GetComponent<Renderer>().material;
		// rb = GetComponent<Rigidbody>();
	}

	void OnMouseOver() {
		GetComponent<Renderer>().material = seen;
	}

	void OnMouseExit() {
		GetComponent<Renderer>().material = existing;
	}

	void Update () {
		float translation = Input.GetAxis("Vertical") * speed;
		float strafe = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		strafe *= Time.deltaTime;

		transform.Translate(strafe, 0, translation);
	}

	void FixedUpdate() {



		// float moveHorizontal = Input.GetAxis("Horizontal");
		// float moveVertical = Input.GetAxis("Vertical");
		//
		// Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		//
		// rb.AddForce(movement * speed);
	}
}

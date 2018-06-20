using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private int count;
	private Rigidbody rb;

	public float speed;
	public Text CountText;
	public Text WinText;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>(); // Get by type?? :O
		count = 0;
		UpdateCount();
		WinText.text = "";
	}

	// Update is called once per frame
	void Update () {

	}

	void UpdateCount() {
		CountText.text = "Count: " + count.ToString();

		if (count >= 8) {
			WinText.text = "You Win!";
		}
	}

	// Fixed time updates, so framerate doesn't make us go faster?? :/
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other) {
		// Destroy(other.gameObject);

		if (other.gameObject.CompareTag("Pickup")) {
			other.gameObject.SetActive(false);

			count += 1;
			UpdateCount();
		}
	}
}

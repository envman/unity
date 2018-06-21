using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclusionZone : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		var pc = other.gameObject.GetComponent<PlayerController>();
		if (pc != null) {
			pc.inNaughtyZone = true;
		}
	}

	void OnTriggerExit(Collider other) {
		var pc = other.gameObject.GetComponent<PlayerController>();
		if (pc != null) {
			pc.inNaughtyZone = false;
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	public string staticPassword;
	public KeyPadContoller keyPad;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		this.keyPad.Show(staticPassword);
	}

	public void Open() {
		Destroy(gameObject);
	}
}

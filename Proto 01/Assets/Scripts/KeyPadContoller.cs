using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPadContoller : MonoBehaviour {

	public Text display;
	public DoorController door;

	string code = "";
	string pass = "1234";

	void Start() {
		door.keyPad = this;
		Hide();
	}

	public void Show(string password) {
		pass = password;
		gameObject.SetActive(true);
	}

	public void Hide() {
		gameObject.SetActive(false);
	}

	public void Pressed(string val) {
		code = code + val;
		display.text = code;

		Check();
	}

	public void Reset() {
		code = "";
	}

	void Check() {
		if (code == pass) {
			display.text = "Correct!";

			door.Open();
			Hide();
		}
	}
}

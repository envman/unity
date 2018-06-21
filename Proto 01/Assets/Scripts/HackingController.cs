using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingController : MonoBehaviour {

	public InputField input;

	private KeyPadContoller currentKeyPad;

	private bool hacking = false;
	private int guess = 0;

	void Start () {
		input.onEndEdit.AddListener((str) => LockInput());
		// input.onEndEdit.AddListener(delegate { LockInput(input); });

		gameObject.SetActive(false);
	}

	public void BeginHack(KeyPadContoller keyPad) {
		currentKeyPad = keyPad;

		gameObject.SetActive(true);
	}

	// TODO: Should probably take fixed time to hack, skillzzz
	void Guess() {
		var result = guess.ToString().PadLeft(4, '0');

		if (currentKeyPad.Check(result)) {
			gameObject.SetActive(false);
			hacking = false;
			guess = 0;
		} else {
			guess++;
		}
	}

	void Update() {
		if (hacking) {
			Guess();
			Guess();
			Guess();
		}
	}

	void LockInput() {
		if (input.text == "run script hack") {
			Debug.Log("run script...");
			hacking = true;
		}

		input.text = "";
	}
}

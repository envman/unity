using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingController : MonoBehaviour {

	public InputField input;

	void Start () {
		input.onEndEdit.AddListener(delegate { LockInput(input); });
	}

	void LockInput(InputField theInput) {

		if (input.text == "run script hack") {

		}

		input.text = "";
	}
}

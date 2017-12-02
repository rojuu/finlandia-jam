using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sauna : MonoBehaviour {
	public Image fadeImage;
	public float warmthGainAmount;

	Text saunaText;
	Player player;
	bool isInSaunaTrigger;
	bool usedSauna = false;

	void Awake() {
		player = FindObjectOfType<Player>();
	}

	void OnTriggerEnter(Collider col) {
		if(col.tag == "Player") {
			saunaText.gameObject.SetActive(true);
			isInSaunaTrigger = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if(col.tag == "Player") {
			saunaText.gameObject.SetActive(false);
			isInSaunaTrigger = false;
		}
	}

	void Update() {
		if(isInSaunaTrigger && !usedSauna && Input.GetKeyDown(KeyCode.E)) {
			// StartCoroutine(UseSauna());
		}
	}

	IEnumerator UseSauna() {
		usedSauna = true;
		
		// Fade
		for(;;) {
			
		}
	}
}

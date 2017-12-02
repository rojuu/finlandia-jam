using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sauna : MonoBehaviour {
	public float warmthGainAmount;
	public Text saunaText;
	public Player player;
	public bool inSauna;

	void Awake() {
		player = FindObjectOfType<Player>();
	}
	void OnTriggerEnter(Collider col) {
		if(col.tag == "Player") {
			saunaText.gameObject.SetActive(true);
			inSauna = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if(col.tag == "Player") {
			saunaText.gameObject.SetActive(false);
			inSauna = false;
		}
	}

	void Update() {
		if(inSauna && Input.GetKeyDown(KeyCode.E)) {
			player.currentWarmth += warmthGainAmount;
		}
	}
}

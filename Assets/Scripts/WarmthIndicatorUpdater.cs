using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarmthIndicatorUpdater : MonoBehaviour {
	public Player player;
	public Text text;

	void Awake() {
		player = FindObjectOfType<Player>();
		if(player == null) Destroy(gameObject);
		text = GetComponent<Text>();
		if(text == null) Destroy(gameObject);
	}

	void Update() {
		text.text = "Warmth: " + Mathf.RoundToInt(player.currentWarmth);
	}
}

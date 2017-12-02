using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

	public float maxWarmth;
	public float warmthChangePerSecond;
	public Text warmthIndicator;
	public float currentWarmth;

	bool isDrinking = false;

	void Start () {
		currentWarmth = maxWarmth;
	}
	
	void Update () {
		warmthIndicator.text = "Warmth: " + Mathf.RoundToInt(currentWarmth);

		if (Input.GetMouseButton(0)) {
			isDrinking = true;
			currentWarmth += warmthChangePerSecond * Time.deltaTime;
		}

		if (!Input.GetMouseButton(0)) {
			isDrinking = false;
		}

		if (currentWarmth > 0 && !isDrinking) {
			currentWarmth -= warmthChangePerSecond * Time.deltaTime;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingHand : MonoBehaviour {
	Player p;

	float rotationAmount;
	public float rotationMaxAmount;
	public float rotationSpeed;

	void Awake() {
		p = FindObjectOfType<Player>();
		if(p == null) Destroy(gameObject);
	}

	void Update() {
		float amount = 0f;
		if(p.isDrinking) {
			amount =  rotationSpeed * Time.deltaTime;
		} else {
			amount = -rotationSpeed * Time.deltaTime;
		}

		rotationAmount += amount;
		rotationAmount = Mathf.Clamp(rotationAmount, 0f, rotationMaxAmount);
		if(rotationAmount < rotationMaxAmount && rotationAmount > 0) {
			transform.RotateAround(transform.position + (transform.forward * 0.3f) + (-transform.up * 0.2f), transform.up, -amount);
		}
	}
}

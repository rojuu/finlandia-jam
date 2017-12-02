using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxWarmth;
    public float warmthChangePerFrame;
    public Text warmthIndicator;
    public float currentWarmth;

    bool isDrinking = false;


	void Start () {
        currentWarmth = maxWarmth;
	}
	
	void Update () {
        warmthIndicator.text = "Warmth: " + Mathf.RoundToInt(currentWarmth);
        print(currentWarmth);
        if (currentWarmth > 0 && !isDrinking) {
            currentWarmth -= warmthChangePerFrame;
        }

        if (Input.GetMouseButton(0)) {
            isDrinking = true;
            currentWarmth += warmthChangePerFrame;
        }

        if (Input.GetMouseButtonUp(0)) {
            isDrinking = false;
        }

	}
}

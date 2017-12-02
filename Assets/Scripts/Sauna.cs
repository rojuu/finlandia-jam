using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sauna : MonoBehaviour {
	public Image fadeImage;
	public float fadeTime;
	public float warmthGainAmount;
	public Text saunaText;
	public float blackTime;

	Player player;
	bool isInSaunaTrigger;
	bool usedSauna = false;

	void Awake() {
		player = FindObjectOfType<Player>();
	}

	void OnTriggerEnter(Collider col) {
		if(col.tag == "Player" && !usedSauna) {
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
			StartCoroutine(UseSauna());
		}
	}

	IEnumerator Fade(Color fadeColor, float lerpTime, float startAlpha, float endAlpha) {
		float currentLerpTime = 0f;
		for(;;) {
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
	
			float t = currentLerpTime / lerpTime;
			fadeColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
			fadeImage.color = fadeColor;

			yield return null;
			if(t > 0.99f) break;
		}
	}

	IEnumerator UseSauna() {
		usedSauna = true;

		float lerpTime = fadeTime;
		Color fadeColor = Color.black;
		fadeColor.a = 0f;

		yield return StartCoroutine(Fade(fadeColor, lerpTime, 0f, 1f));

		yield return new WaitForSeconds(blackTime);

		saunaText.gameObject.SetActive(false);
		player.currentWarmth += warmthGainAmount;

		yield return StartCoroutine(Fade(fadeColor, lerpTime, 1f, 0f));
	}
}

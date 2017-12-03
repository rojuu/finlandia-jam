using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sauna : MonoBehaviour {
	public Image fadeImage;
	public float fadeTime;
	public float warmthGainAmount;
    public float soberGainAmount;
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
			player.audioSource.PlayOneShot(player.nearSaunaClips[Random.Range(0, player.nearSaunaClips.Length)]);
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
		player.isInSauna = true;
		player.currentWarmth += warmthGainAmount;

		float lerpTime = fadeTime;
		Color fadeColor = Color.black;
		fadeColor.a = 0f;
		int index = Random.Range(0, player.avaaSaunanClips.Length);
		player.audioSource.PlayOneShot(player.avaaSaunanClips[index]);
		yield return StartCoroutine(Fade(fadeColor, lerpTime, 0f, 1f));
		yield return new WaitForSeconds(player.avaaSaunanClips[index].length + 0.1f);


		player.audioSource.PlayOneShot(player.inSaunaClips[Random.Range(0, player.inSaunaClips.Length)]);
		yield return new WaitForSeconds(blackTime);
		player.audioSource.PlayOneShot(player.afterSaunaClips[Random.Range(0, player.afterSaunaClips.Length)]);

		saunaText.gameObject.SetActive(false);
		player.currentWarmth += warmthGainAmount;
        player.drunkness -= soberGainAmount;
		player.isInSauna = false;

		yield return StartCoroutine(Fade(fadeColor, lerpTime, 1f, 0f));
	}
}

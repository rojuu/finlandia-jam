using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float fadeTime;
    public float waitTime;
    public Image fadeImage;

    public Text tutorial;

    Player player;

    bool isDead;

	void Start () {
        player = FindObjectOfType<Player>();
        StartCoroutine(RemoveTutorial(7));
	}
	
	void Update () {
		if (player.currentWarmth <= 0 && !isDead) {
            StartCoroutine(Dead());
        }

        if (Input.GetKeyDown("r")) {
            ResetLevel();
        }
	}


    public void ResetLevel() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator Dead() {
        isDead = true;

        float lerpTime = fadeTime;
        Color fadeColor = Color.black;
        fadeColor.a = 0f;

        yield return StartCoroutine(Fade(fadeColor, lerpTime, 0f, 1f));

        yield return new WaitForSeconds(waitTime);

        ResetLevel();

    }

    IEnumerator Fade(Color fadeColor, float lerpTime, float startAlpha, float endAlpha) {
        float currentLerpTime = 0f;
        for (; ; ) {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }

            float t = currentLerpTime / lerpTime;
            fadeColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = fadeColor;

            yield return null;
            if (t > 0.99f) break;
        }
    }

    IEnumerator RemoveTutorial(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        tutorial.gameObject.SetActive(false);
    }
}

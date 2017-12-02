using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {
	public float maxWarmth;
	public float warmthDepletePerSecond;
	public float warmthGainPerSecond;
    public AudioClip callSound;

    AudioSource audioSource;

    Roni roni;

	private float _currentWarmth;
	public float currentWarmth {
		get {
			return _currentWarmth;
		}
		set {
			_currentWarmth = Mathf.Clamp(value, 0, maxWarmth);
		}
	}

	bool isDrinking = false;

	void Start () {
		currentWarmth = maxWarmth;
        audioSource = GetComponent<AudioSource>();
        roni = FindObjectOfType<Roni>();
	}

	void Update () {
		if (Input.GetMouseButton(0)) {
			isDrinking = true;
			currentWarmth += warmthGainPerSecond * Time.deltaTime;
		}

		if (!Input.GetMouseButton(0)) {
			isDrinking = false;
		}

        if (Input.GetMouseButtonDown(1)) {
            StartCoroutine(CallRoni());
        }

		if (currentWarmth > 0 && !isDrinking) {
			currentWarmth -= warmthDepletePerSecond * Time.deltaTime;
		}
	}


    IEnumerator CallRoni() {
        audioSource.PlayOneShot(callSound);
        yield return new WaitForSeconds(callSound.length);
        roni.CalledRoni();
    }

}

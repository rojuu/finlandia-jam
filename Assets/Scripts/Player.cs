using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
			_currentWarmth = Mathf.Clamp (value, 0, maxWarmth);
		}
	}

	public bool isDrinking = false;

	[HideInInspector]
	public float drunkness = 0f;
	public float drunknessGainedPerSecond = 1f;
	public float drunknessLostPerSecond = 0.1f;

[Header("Drunkness effects")]
	public float movementAngleRotation = 270f;
	public float movementRandom = 5f;
	

	void Awake () {
		GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().Init(this);
	}

	void Start () {
		currentWarmth = maxWarmth;
		audioSource = GetComponent<AudioSource> ();
		roni = FindObjectOfType<Roni> ();
	}

	void Update () {
		if (Input.GetMouseButton (0)) {
			isDrinking = true;
			currentWarmth += warmthGainPerSecond * Time.deltaTime;
			drunkness += (drunknessGainedPerSecond + drunknessLostPerSecond) * Time.deltaTime;
			drunkness = Mathf.Clamp (drunkness, 0f, 1f);
		}

		if (!Input.GetMouseButton (0)) {
			isDrinking = false;
		}

		if (Input.GetMouseButtonDown (1)) {
			StartCoroutine (CallRoni ());
		}

		if (currentWarmth > 0 && !isDrinking) {
			currentWarmth -= warmthDepletePerSecond * Time.deltaTime;
			currentWarmth -= drunknessLostPerSecond * Time.deltaTime;
		}
	}

	IEnumerator CallRoni () {
		audioSource.PlayOneShot (callSound);
		yield return new WaitForSeconds (callSound.length);
		roni.CalledRoni ();
	}

}
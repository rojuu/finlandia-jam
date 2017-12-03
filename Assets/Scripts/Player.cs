using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	new Transform transform;
	public float maxWarmth;
	public float warmthDepletePerSecond;
	public float warmthGainPerSecond;
	public float huutoWarmthRaja;
	public Image thermometer;
	public Image whiteFill;
	public Text endText;
	bool canHuutoWarmth = true;
	public AudioClip[] callSoundClips;
	public AudioClip[] viinanJuontiClips;
	public AudioClip[] afterViinaClips;
	public AudioClip[] nearSaunaClips;
	public AudioClip[] avaaSaunanClips;
	public AudioClip[] inSaunaClips;
	public AudioClip[] afterSaunaClips;
	public AudioClip[] foundRoniClips;
	public AudioClip[] warmthHuutoClips;
	public AudioClip[] pelinAlkuClips;
	public AudioClip[] foundKotiClips;
	public AudioClip[] kuolemaClips;
	public AudioClip[] randomHuuteluClips;

	public AudioSource audioSource;

	Roni roni;

	public bool foundRoni {
		get {
			return roni.foundRoni;
		}
		set {
			roni.foundRoni = value;
		}
	}

	public bool kotonaOllaan {
		get {
			return roni.kotonaOllaan;
		}
		set {
			roni.kotonaOllaan = value;
		}
	}

	private float _currentWarmth;
	public float currentWarmth {
		get {
			return _currentWarmth;
		}
		set {
			_currentWarmth = Mathf.Clamp (value, 0, maxWarmth);
			thermometer.fillAmount = _currentWarmth / maxWarmth;
		}
	}

	public bool isDrinking = false;

	float _drunkness = 0f;

	[HideInInspector]
	public float drunkness {
		get {
			return _drunkness;
		}
		set {
			_drunkness = value;
			UpdateShaders ();
		}
	}
	public float drunknessGainedPerSecond = 1f;
	public float drunknessLostPerSecond = 0.1f;

	public float randomHuuteluDelayMin, randomHuuteluDelayMax;

	[Header ("Drunkness effects")]
	public float movementAngleRotation = 270f;
	public float movementRandom = 5f;

	void Awake () {
		GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().Init (this);
		rotOffset = new Vector3 (
			Random.Range (0, Mathf.PI),
			Random.Range (0, Mathf.PI),
			Random.Range (0, Mathf.PI)
		);
		transform = GetComponent<Transform> ();
		Camera cam = GetComponentInChildren<Camera> ();
		rotator = cam.transform.parent;
		pp = cam.GetComponent<PostProcessing> ();
		bb = cam.GetComponent<BoxBlur> ();
		UpdateShaders ();
	}

	void Start () {
		currentWarmth = maxWarmth;
		roni = FindObjectOfType<Roni> ();
		audioSource.PlayOneShot (pelinAlkuClips[Random.Range (0, pelinAlkuClips.Length)]);
		StartCoroutine (RandomHuutelu ());
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

		if (Input.GetMouseButtonDown (0) && !audioSource.isPlaying) {
			int index = Random.Range (0, viinanJuontiClips.Length);
			audioSource.PlayOneShot (viinanJuontiClips[index]);
		}

		if (Input.GetMouseButtonUp (0) && !audioSource.isPlaying) {
			int index = Random.Range (0, afterViinaClips.Length);
			audioSource.PlayOneShot (afterViinaClips[index]);
		}

		if (Input.GetMouseButtonDown (1)) {
			StartCoroutine (CallRoni ());
		}

		if (currentWarmth > 0 && !isDrinking) {
			currentWarmth -= warmthDepletePerSecond * Time.deltaTime;
			drunkness -= drunknessLostPerSecond * Time.deltaTime;
		}

		UpdateCameraRotation ();

		if (currentWarmth < huutoWarmthRaja && canHuutoWarmth) {
			canHuutoWarmth = false;
			audioSource.PlayOneShot (warmthHuutoClips[Random.Range (0, warmthHuutoClips.Length)]);
		}

		if (currentWarmth > huutoWarmthRaja + 10) {
			canHuutoWarmth = true;
		}
	}

	bool callingRoni = false;
	IEnumerator CallRoni () {
		if (!callingRoni) {
			callingRoni = true;
			int index = Random.Range (0, callSoundClips.Length);
			audioSource.Stop ();
			audioSource.PlayOneShot (callSoundClips[index]);
			yield return new WaitForSeconds (callSoundClips[index].length);
			callingRoni = false;
			roni.CalledRoni ();
		}
	}

	Transform rotator;
	public Vector3 drunkCamRot;
	Vector3 rotOffset;
	void UpdateCameraRotation () {
		rotator.localRotation =
			Quaternion.Euler (new Vector3 (
				Mathf.Clamp (Mathf.Sin (Time.timeSinceLevelLoad + rotOffset.x) * drunkness * drunkCamRot.x, -drunkCamRot.x, drunkCamRot.x),
				Mathf.Clamp (Mathf.Sin (Time.timeSinceLevelLoad + rotOffset.y) * drunkness * drunkCamRot.y, -drunkCamRot.y, drunkCamRot.y),
				Mathf.Clamp (Mathf.Sin (Time.timeSinceLevelLoad + rotOffset.z) * drunkness * drunkCamRot.z, -drunkCamRot.z, drunkCamRot.z)));
	}

	PostProcessing pp;
	BoxBlur bb;
	public float maxDisplacement = 0.1f;
	public int maxBlurIterations = 4;
	public int maxDownRes = 4;

	void UpdateShaders () {
		pp.EffectMaterial.SetFloat (
			"_Magnitude",
			Mathf.Lerp (0, maxDisplacement, Mathf.Pow (drunkness, 2))
		);
		bb.Iterations = Mathf.RoundToInt (
			Mathf.Lerp (0, maxBlurIterations, drunkness)
		);
		bb.DownRes = Mathf.RoundToInt (
			Mathf.Lerp (0, maxDownRes, drunkness)
		);
	}

	IEnumerator RandomHuutelu () {
		for (;;) {
			yield return new WaitForSeconds (Random.Range (randomHuuteluDelayMin, randomHuuteluDelayMax));
			if (!audioSource.isPlaying) {
				audioSource.PlayOneShot (randomHuuteluClips[Random.Range (0, randomHuuteluClips.Length)]);
			}
		}
	}

	public IEnumerator EndGame () {
		float currentTime = 0;
		float lerpTime = 0.2f;
		endText.gameObject.SetActive (true);
		for (;;) {
			currentTime += Time.deltaTime;
			if (currentTime > lerpTime) {
				currentTime = lerpTime;
			}

			float t = currentTime / lerpTime;
			Color endC = endText.color;
			Color c = whiteFill.color;
			c.a = Mathf.Lerp (0f, 1f, t);
			endC.a = Mathf.Lerp (0f, 1f, t);
			whiteFill.color = c;
			endText.color = endC;

			yield return null;
			if (t > 0.99f) break;
		}

		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (0);
	}
}
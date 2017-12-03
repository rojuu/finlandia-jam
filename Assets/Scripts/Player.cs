using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
		new Transform transform;
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
		}

		void Start () {
			currentWarmth = maxWarmth;
			audioSource = GetComponent<AudioSource> ();
			roni = FindObjectOfType<Roni> ();
			rotator = GetComponentInChildren<Camera> ().transform.parent;
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
			UpdateCameraRotation ();
		}

		IEnumerator CallRoni () {
			audioSource.PlayOneShot (callSound);
			yield return new WaitForSeconds (callSound.length);
			roni.CalledRoni ();
		}

		Transform rotator;
		public Vector3 drunkCamRot;
		Vector3 rotOffset;
		void UpdateCameraRotation () {
			rotator.localRotation =
				Quaternion.Euler(new Vector3 (
					Mathf.Clamp(Mathf.Sin (Time.timeSinceLevelLoad + rotOffset.x) * drunkness * drunkCamRot.x, -drunkCamRot.x, drunkCamRot.x),
					Mathf.Clamp(Mathf.Sin (Time.timeSinceLevelLoad + rotOffset.y) * drunkness * drunkCamRot.y, -drunkCamRot.y, drunkCamRot.y),
					Mathf.Clamp(Mathf.Sin (Time.timeSinceLevelLoad + rotOffset.z) * drunkness * drunkCamRot.z, -drunkCamRot.z, drunkCamRot.z)));
			}
		}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roni : MonoBehaviour {

    public float scaredAmount;
    public AudioClip barkSound;

    AudioSource audioSource;

	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {

        print("Hauhau oot mun triggerin sisäl");
    }

    public void CalledRoni() {
        audioSource.PlayOneShot(barkSound);
    }

}

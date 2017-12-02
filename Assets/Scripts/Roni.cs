using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roni : MonoBehaviour {

    public AudioClip barkSound;
    public float waitTime = 1;

    AudioSource audioSource;
    int randomPoint;

    public float speed = 10;
    float lerpTime;
    float currentLerpTime;
    float moveDistance;
    Vector3 startPos;
    Vector3 endPos;

    bool nytLerpataan = false;

    GameObject[] dogPoints;

	void Start () {
        audioSource = GetComponent<AudioSource>();
        dogPoints = GameObject.FindGameObjectsWithTag("DogPoint");
        StartCoroutine(SwitchPlaces(waitTime));
	}

	void Update () {
		if (nytLerpataan) {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            float perc = currentLerpTime / lerpTime;
            transform.position = Vector3.Lerp(startPos, endPos, perc);
        }
	}

    private void OnTriggerEnter(Collider other) {

        print("Hauhau oot mun triggerin sisäl");
    }

    public void CalledRoni() {
        audioSource.PlayOneShot(barkSound);
    }

    IEnumerator SwitchPlaces(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        print("Nyt alkaa lerppi");
        randomPoint = Random.Range(0, dogPoints.Length);
        startPos = transform.position;
        endPos = dogPoints[randomPoint].transform.position;
        moveDistance = Vector3.Distance(startPos, endPos);
        lerpTime = moveDistance / speed;
        nytLerpataan = true;
    }

}

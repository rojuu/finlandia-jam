using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roni : MonoBehaviour {

    public AudioClip barkSound;
    public float waitTime;

    AudioSource audioSource;
    int randomPoint;

    public float speed;
    float lerpTime;
    float currentLerpTime;
    float moveDistance;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 homePoint;

    bool nytLerpataan = false;
    public bool foundRoni = false;
    public bool kotonaOllaan = false;

    GameObject[] dogPoints;

	void Start () {
        audioSource = GetComponent<AudioSource>();
        dogPoints = GameObject.FindGameObjectsWithTag("DogPoint");
        homePoint = GameObject.FindGameObjectWithTag("HomePoint").transform.position;
        StartCoroutine(SwitchPlaces(waitTime));
	}

	void Update () {
		if (nytLerpataan) {
            transform.LookAt(endPos);
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            float perc = currentLerpTime / lerpTime;
            transform.position = Vector3.Lerp(startPos, endPos, perc);
        }

        if (kotonaOllaan && transform.position == endPos) {
            transform.LookAt(FindObjectOfType<Player>().transform.position);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            print("Hauhau oot mun triggerin sisäl");
            nytLerpataan = false;

            if (!foundRoni) {
                audioSource.PlayOneShot(barkSound);
                Player p = other.gameObject.GetComponent<Player>();
                p.audioSource.PlayOneShot(p.foundRoniClips[Random.Range(0, p.foundRoniClips.Length)]);
                foundRoni = true;
            }

            currentLerpTime = 0;
            startPos = transform.position;
            endPos = homePoint;
            moveDistance = Vector3.Distance(startPos, endPos);
            lerpTime = moveDistance / speed;
            nytLerpataan = true;
        }

        if (other.gameObject.tag == "HomePoint") {
            kotonaOllaan = true;
        }

    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            nytLerpataan = false;
        }
    }

    public void CalledRoni() {
        audioSource.PlayOneShot(barkSound);
    }

    public void ArrivedAtDestination() {
        nytLerpataan = false;
        currentLerpTime = 0;
        StartCoroutine(SwitchPlaces(waitTime));
    }

    IEnumerator SwitchPlaces(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if (!foundRoni) {
            print("Nyt alkaa lerppi");
            randomPoint = Random.Range(0, dogPoints.Length);
            startPos = transform.position;
            endPos = dogPoints[randomPoint].transform.position;
            moveDistance = Vector3.Distance(startPos, endPos);
            lerpTime = moveDistance / speed;
            nytLerpataan = true;
        }
        
        /*
        if (foundRoni) {
            startPos = transform.position;
            endPos = homePoint;
            moveDistance = Vector3.Distance(startPos, endPos);
            lerpTime = moveDistance / (speed * 0.5f);
            nytLerpataan = true;
        }
        */
    }
}

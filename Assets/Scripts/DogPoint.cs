using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPoint : MonoBehaviour {

    Roni roni;

    private void Start() {
        roni = FindObjectOfType<Roni>();
    }

    private void OnTriggerEnter(Collider other) {
        print("TRIGGERED");
        if (other.gameObject.tag == "Roni") {
            print("Roni on perillä");
            roni.ArrivedAtDestination();
        }
    }
}

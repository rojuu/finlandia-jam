using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour {
	void OnTriggerEnter(Collider col) {
		Player p = col.gameObject.GetComponent<Player>();
		if(p) {
			if(p.kotonaOllaan) {
				StartCoroutine(p.EndGame());
			}
		}
	}
}

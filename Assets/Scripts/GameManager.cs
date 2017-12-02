using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    Player player;

	void Start () {
        player = FindObjectOfType<Player>();
	}
	
	void Update () {
		if (player.currentWarmth <= 0) {
            ResetLevel();
        }

        if (Input.GetKeyDown("r")) {
            ResetLevel();
        }
	}


    public void ResetLevel() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}

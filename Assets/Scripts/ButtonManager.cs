using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    public Button playButton;
    public Button exitButton;

	void Start () {
        Button pbtn = playButton.GetComponent<Button>();
        Button ebtn = exitButton.GetComponent<Button>();
        pbtn.onClick.AddListener(PlayGame);
        ebtn.onClick.AddListener(ExitGame);
	}

    void PlayGame() {
        SceneManager.LoadScene("ToniTestet");
    }

    void ExitGame() {
        Application.Quit();
    }
}

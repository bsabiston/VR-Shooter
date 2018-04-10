using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI: MonoBehaviour {

	public Image fadePlane;
	public GameObject gameOverUI;
	public Text scoreUI;

	public Text gameOverScoreUI;

	Player player;

	void Start () {
		player = FindObjectOfType<Player> ();
		player.OnDeath += OnGameOver;
	}


	void Update() {
		/*
		if (gameOverUI.activeSelf) {
			if (Input.GetButtonDown ("Fire2" )) {
				StartNewGame();
			}
		}
	*/
	}


	void OnGameOver () {
		Cursor.visible = true;
		StartCoroutine (Fade (Color.clear, new Color(0,0,0,.95f), 1));
		gameOverScoreUI.text = scoreUI.text;
		scoreUI.gameObject.SetActive (false);

		gameOverUI.SetActive (true);

	}

	IEnumerator Fade(Color from, Color to, float time) {
		float speed = 1 / time;
		float percent = 0;
		while (percent < 1) {
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp (from, to, percent);
			yield return null;
		}

	}

	// UI Input

	public void StartNewGame() {
		SceneManager.LoadScene ("Game");
	}

	public void ReturnToMainMenu() {
		SceneManager.LoadScene ("Menu");
	}

}

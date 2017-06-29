using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private static GameManager instance;
	public bool gameOver = false;

	private GameObject player;
	private Text playerHealthText;
	private GameObject gameOverText;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	void Start(){
		//Find the player
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealthText = GameObject.Find ("PlayerHealthText").GetComponent<Text> ();
		gameOverText = GameObject.Find ("GameOverText");
		gameOverText.SetActive (false);
	}

	// Stub method to trigger Game Over state;
	public void KillPlayer(){
		gameOver = true;
	}

	void Update(){
		SetPlayerHealthText ();
		CheckGameOver ();

		// If gameOver, activate text and load MainMenu after 2 seconds
		if(gameOver){
			gameOverText.SetActive (true);
			Invoke ("LoadMainMenu", 2f);
		}
	}

	void CheckGameOver(){
		if (player != null) {
			// Get player health some way (Stubbed)
			//gameOver = player.GetHealth () <= 0;
		}
	}

	void SetPlayerHealthText(){
		playerHealthText.text = "Health: " + Random.Range (0, 100);
	}

	private void LoadMainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
}

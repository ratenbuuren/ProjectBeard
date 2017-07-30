using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public bool gameOver = false;

	[Range(1f, 3f)]
	public int humanPlayers;
	[Range(0f, 3f)]
	public int computerPlayers;

	private GameObject player;
	private GameObject computerPlayer;

	public GameObject playerPrefab;
	public GameObject computerPlayerPrefab;

	private Text playerHealthText;
	private Text computerPlayerHealthText;

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
		//Create the players
		player = Instantiate(playerPrefab, new Vector3(-5,0,0), Quaternion.identity);
		computerPlayer = Instantiate(computerPlayerPrefab, new Vector3(5,0,0), Quaternion.identity);

		//Find their text labels
		playerHealthText = GameObject.Find ("PlayerHealthText").GetComponent<Text> ();
		computerPlayerHealthText = GameObject.Find ("ComputerPlayerHealthText").GetComponent<Text> ();

		//Find the game over text
		gameOverText = GameObject.Find ("GameOverText");
		gameOverText.SetActive (false);
	}

	// Stub method to trigger Game Over state;
	public void KillPlayer(){
		player.GetComponent<BaseTank> ().TakeDamage (1000);
	}

	void Update(){
		SetHealthTexts ();
		CheckGameOver ();

		// If gameOver, activate text and load MainMenu after 2 seconds
		if(gameOver){
			gameOverText.SetActive (true);
			Invoke ("LoadMainMenu", 2f);
		}
	}

	void CheckGameOver(){
		if (player != null) {
			// Get player health some way
			if (player.GetComponent<BaseTank> ().GetHealth () <= 0) {
				gameOver = true;
				gameOverText.GetComponent<Text> ().text = "Loser";
			}
			else if(computerPlayer != null && computerPlayer.GetComponent<BaseTank>().GetHealth () <= 0){
				gameOver = true;
				gameOverText.GetComponent<Text> ().text = "Winner";
			}
		}
	}

	void SetHealthTexts(){
		if (player != null) {
			playerHealthText.text = "Health: " + player.GetComponent<BaseTank> ().GetHealth ();
		}
		if (computerPlayer != null) {
			computerPlayerHealthText.text = "Health: " + computerPlayer.GetComponent<BaseTank> ().GetHealth ();
		}
	}

	private void LoadMainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
}

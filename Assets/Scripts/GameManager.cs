using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public bool gameOver = false;

    [Range(1f, 3f)] public int humanPlayers;
    [Range(0f, 3f)] public int computerPlayers;

    private GameObject player;
    private GameObject computerPlayer;
    private List<GameObject> players = new List<GameObject>();

    public GameObject playerPrefab;
    public GameObject computerPlayerPrefab;
    public GameObject[] PowerUpPrefabs;

    private Text playerHealthText;
    private Text computerPlayerHealthText;

    private GameObject gameOverText;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        //Create the players
        CreateHumanPlayer(new Vector3(-5, 0, 0), "HorizontalKeyboard", "VerticalKeyboard", "FireKeyboard",
            "RotateTurretController1", false);
        CreateHumanPlayer(new Vector3(5, 0, 0), "HorizontalController1", "VerticalController1", "FireController1",
            "RotateTurretController1", true);
        CreateHumanPlayer(new Vector3(0, 3, 0), "HorizontalController2", "VerticalController2", "FireController2",
            "RotateTurretController2", true);

        //computerPlayer = Instantiate(computerPlayerPrefab, new Vector3(5,0,0), Quaternion.identity);

        //Find their text labels
        playerHealthText = GameObject.Find("PlayerHealthText").GetComponent<Text>();
        computerPlayerHealthText = GameObject.Find("ComputerPlayerHealthText").GetComponent<Text>();

        //Find the game over text
        gameOverText = GameObject.Find("GameOverText");
        gameOverText.SetActive(false);

        InvokeRepeating("SpawnPowerUps", 2.0f, 2.0f);
    }

    private void CreateHumanPlayer(Vector3 postition, string horizontalAxis, string verticalAxis, string fireInput,
        string rotateTurretAxis, bool controller) {
        player = Instantiate(playerPrefab, postition, Quaternion.identity);
        player.GetComponent<HumanTankMovement>().SetAxis(horizontalAxis, verticalAxis);
        player.GetComponent<HumanTankShooting>().SetFireInput(fireInput, rotateTurretAxis, controller);
        players.Add((player));
    }

    // Stub method to trigger Game Over state;
    public void KillPlayer() {
        player.GetComponent<TankStats>().TakeDamage(1000);
    }

    public void RemovePlayer(GameObject player) {
        players.Remove(player);
    }

    void Update() {
        SetHealthTexts();
        CheckGameOver();

        // If gameOver, activate text and load MainMenu after 2 seconds
        if (gameOver) {
            gameOverText.SetActive(true);
            Invoke("LoadMainMenu", 2f);
        }
    }

    void CheckGameOver() {
        gameOver = (players.Count == 1);
    }

    void SetHealthTexts() {
        if (player != null) {
            playerHealthText.text = "Health: " + player.GetComponent<TankStats>().GetStat(StatType.Health);
        }
        if (computerPlayer != null) {
            computerPlayerHealthText.text =
                "Health: " + computerPlayer.GetComponent<TankStats>().GetStat(StatType.Health);
        }
    }

    private void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    private void SpawnPowerUps() {
        int maxX = 12;
        int maxY = 7;
        float posX = Random.Range(0, maxX) - (maxX / 2);
        float posY = Random.Range(0, maxY) - (maxY / 2);
        Instantiate(PowerUpPrefabs[Random.Range(0, PowerUpPrefabs.Length)], new Vector3(posX, posY, 0),
            Quaternion.identity);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public bool gameOver = false;

    [Range(1f, 3f)] public int humanPlayers;
    [Range(0f, 3f)] public int computerPlayers;

    private GameObject player;
    private GameObject computerPlayer;
    private List<GameObject> players = new List<GameObject>();
    private Dictionary<TankStats, Text> _playerStatsUIs = new Dictionary<TankStats, Text>();

    public GameObject playerPrefab;
    public GameObject computerPlayerPrefab;
    public GameObject playerStatsUIPrefab;
    public GameObject[] powerUpPrefabs;

    private GameObject _canvas;

    private GameObject gameOverText;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        _canvas = GameObject.Find("Canvas");
        //Create the players
        CreateHumanPlayer("Player1 Tank", new Vector3(-5, 0, 0), "HorizontalKeyboard", "VerticalKeyboard", "FireKeyboard",
            "RotateTurretController1", false);
        CreateHumanPlayer("Player2 Tank", new Vector3(5, 0, 0), "HorizontalController1", "VerticalController1", "FireController1",
            "RotateTurretController1", true);
        CreateHumanPlayer("Player2 Tank", new Vector3(0, 3, 0), "HorizontalController2", "VerticalController2", "FireController2",
            "RotateTurretController2", true);

        //computerPlayer = Instantiate(computerPlayerPrefab, new Vector3(5,0,0), Quaternion.identity);

        //Find the game over text
        gameOverText = GameObject.Find("GameOverText");
        gameOverText.SetActive(false);

        InvokeRepeating("SpawnPowerUps", 2.0f, 2.0f);
    }

    private void CreateHumanPlayer(string name, Vector3 postition, string horizontalAxis, string verticalAxis, string fireInput,
        string rotateTurretAxis, bool controller) {
        player = Instantiate(playerPrefab, postition, Quaternion.identity);
        player.name = name;
        player.GetComponent<HumanTankMovement>().SetAxis(horizontalAxis, verticalAxis);
        player.GetComponent<HumanTankShooting>().SetFireInput(fireInput, rotateTurretAxis, controller);
        players.Add((player));
        
        
        GameObject playerStatsUI = Instantiate(playerStatsUIPrefab, Vector3.zero, Quaternion.identity);
        playerStatsUI.transform.SetParent(_canvas.transform);
        
        playerStatsUI.GetComponent<RectTransform>().anchorMin = GetAnchorForPlayerUI(players.Count);
        playerStatsUI.GetComponent<RectTransform>().anchorMax = GetAnchorForPlayerUI(players.Count);
        playerStatsUI.GetComponent<RectTransform>().pivot = GetAnchorForPlayerUI(players.Count);
        
        playerStatsUI.GetComponent<RectTransform>().localScale = Vector3.one;
        playerStatsUI.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        
        playerStatsUI.GetComponent<Text>().color = GetColorForPlayerUI(players.Count);
        player.GetComponent<SpriteRenderer>().color = GetColorForPlayerUI(players.Count);
        _playerStatsUIs.Add(player.GetComponent<TankStats>(), playerStatsUI.GetComponent<Text>());
    }

    private Vector2 GetAnchorForPlayerUI(int playerNumber) {
        switch (playerNumber) {
            case 1:
                return new Vector2(0, 1);
            case 2:
                return new Vector2(1, 1);
            case 3:
                return new Vector2(0, 0);
            case 4: 
                return new Vector2(0, 1);
            default:
                return new Vector2(0, 0);
        }
    }

    private Color GetColorForPlayerUI(int playerNumber) {
        switch (playerNumber) {
            case 1:
                return Color.red;
            case 2:
                return Color.blue;
            case 3:
                return Color.yellow;
            case 4:
                return Color.green;
            default:
                return Color.black;
        }
    }

    public void RemovePlayer(GameObject player) {
        players.Remove(player);
    }

    void Update() {
        SetStatsUIs();
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

    void SetStatsUIs() {
        //"HP: " + playerStatsUI.Key.GetStat(StatType.Health) + "\n" +
        //"Armor: " + playerStatsUI.Key.GetStat(StatType.Armor) + "\n" +
        foreach (KeyValuePair<TankStats, Text> playerStatsUI in _playerStatsUIs) {
            if (playerStatsUI.Key != null) {
                playerStatsUI.Value.text =
                    playerStatsUI.Key.gameObject.name + "\n" +
                    
                    "FR: " + playerStatsUI.Key.GetStat(StatType.FireRate) + "\n" +
                    "DMG: " + playerStatsUI.Key.GetStat(StatType.ProjectileDamage) + "\n" +
                    "Size: " + playerStatsUI.Key.GetStat(StatType.ProjectileSize) + "\n" +
                    "PVel: " + playerStatsUI.Key.GetStat(StatType.ProjectileVelocity) + "\n" +
                    "Range: " + playerStatsUI.Key.GetStat(StatType.ProjectileRange) + "\n" +
                    "MS: " + playerStatsUI.Key.GetStat(StatType.MovementSpeed) + "\n" +
                    "MRS: " + playerStatsUI.Key.GetStat(StatType.MovementRotationSpeed) + "\n" +
                    "TRS: " + playerStatsUI.Key.GetStat(StatType.TurretRotationSpeed) + "\n";
            }
        }
    }

    private void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    private void SpawnPowerUps() {
        if (powerUpPrefabs.Length > 0) {
            int maxX = 12;
            int maxY = 7;
            float posX = Random.Range(0, maxX) - (maxX / 2);
            float posY = Random.Range(0, maxY) - (maxY / 2);
            Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], new Vector3(posX, posY, 0),
                Quaternion.identity);
        }
    }
}
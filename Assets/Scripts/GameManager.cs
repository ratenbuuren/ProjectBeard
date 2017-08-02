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

    public Color ColorPlayer1 = Color.red;
    public Color ColorPlayer2 = Color.blue;
    public Color ColorPlayer3 = Color.yellow;
    public Color ColorPlayer4 = Color.green;
    private List<Color> _playerColors = new List<Color>();
    private List<Vector2> _playerUiAnchors = new List<Vector2>();

    private GameObject player;
    private GameObject computerPlayer;
    private List<GameObject> players = new List<GameObject>();
    private Dictionary<GameObject, Text> _playerStatsUis = new Dictionary<GameObject, Text>();

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
        AddColorsAndAnchorsToList();
        //Create the players
        CreateHumanPlayer("Player1 Tank", new Vector3(-5, 0, 0), "HorizontalKeyboard", "VerticalKeyboard", "FireKeyboard",
            "RotateTurretController1", false);
        CreateHumanPlayer("Player2 Tank", new Vector3(5, 0, 0), "HorizontalController1", "VerticalController1", "FireController1",
            "RotateTurretController1", true);
        CreateHumanPlayer("Player3 Tank", new Vector3(0, 3, 0), "HorizontalController2", "VerticalController2", "FireController2",
            "RotateTurretController2", true);

        //computerPlayer = Instantiate(computerPlayerPrefab, new Vector3(5,0,0), Quaternion.identity);

        //Find the game over text
        gameOverText = GameObject.Find("GameOverText");
        gameOverText.SetActive(false);

        InvokeRepeating("SpawnPowerUps", 2.0f, 2.0f);
    }

    private void AddColorsAndAnchorsToList() {
        _playerColors.Add(ColorPlayer1);
        _playerColors.Add(ColorPlayer2);
        _playerColors.Add(ColorPlayer3);
        _playerColors.Add(ColorPlayer4);
        _playerUiAnchors.Add(new Vector2(0, 1));
        _playerUiAnchors.Add(new Vector2(1, 1));
        _playerUiAnchors.Add(new Vector2(0, 0));
        _playerUiAnchors.Add(new Vector2(0, 1));
    }

    private void CreateHumanPlayer(string name, Vector3 postition, string horizontalAxis, string verticalAxis, string fireInput,
        string rotateTurretAxis, bool controller) {
        player = Instantiate(playerPrefab, postition, Quaternion.identity);
        player.name = name;
        player.GetComponent<HumanTankMovement>().SetAxis(horizontalAxis, verticalAxis);
        player.GetComponent<HumanTankShooting>().SetFireInput(fireInput, rotateTurretAxis, controller);
        players.Add((player));

        CreatePlayerStatsUi(players.Count, player);
    }

    private void CreatePlayerStatsUi(int playerNumber, GameObject player) {
        GameObject playerStatsUi = Instantiate(playerStatsUIPrefab, Vector3.zero, Quaternion.identity);
        playerStatsUi.transform.SetParent(_canvas.transform);
        
        playerStatsUi.GetComponent<RectTransform>().anchorMin = _playerUiAnchors[playerNumber - 1];
        playerStatsUi.GetComponent<RectTransform>().anchorMax = _playerUiAnchors[playerNumber - 1];
        playerStatsUi.GetComponent<RectTransform>().pivot = _playerUiAnchors[playerNumber - 1];
        
        playerStatsUi.GetComponent<RectTransform>().localScale = Vector3.one;
        playerStatsUi.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        playerStatsUi.GetComponent<Text>().color = _playerColors[playerNumber - 1];
        player.GetComponent<SpriteRenderer>().color = _playerColors[playerNumber - 1];
        _playerStatsUis.Add(player, playerStatsUi.GetComponent<Text>());
    }

    public void RemovePlayer(GameObject player) {
        players.Remove(player);
        Destroy(_playerStatsUis[player].gameObject);
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
        foreach (KeyValuePair<GameObject, Text> playerStatsUI in _playerStatsUis) {
            if (playerStatsUI.Key != null) {
                TankHealth healthScript = playerStatsUI.Key.GetComponent<TankHealth>();
                TankStats statsScript = playerStatsUI.Key.GetComponent<TankStats>();
                playerStatsUI.Value.text =
                    playerStatsUI.Key.gameObject.name + "\n" +
                    "HP: " + healthScript.CurrentHealth + "/" + statsScript.GetStat(StatType.MaxHealth) + "\n" +
                    "Armor: " + healthScript.CurrentArmor + "\n" +
                    "FR: " + statsScript.GetStat(StatType.FireRate) + "\n" +
                    "DMG: " + statsScript.GetStat(StatType.ProjectileDamage) + "\n" +
                    "Size: " + statsScript.GetStat(StatType.ProjectileSize) + "\n" +
                    "PVel: " + statsScript.GetStat(StatType.ProjectileVelocity) + "\n" +
                    "Range: " + statsScript.GetStat(StatType.ProjectileRange) + "\n" +
                    "MS: " + statsScript.GetStat(StatType.MovementSpeed) + "\n" +
                    "MRS: " + statsScript.GetStat(StatType.MovementRotationSpeed) + "\n" +
                    "TRS: " + statsScript.GetStat(StatType.TurretRotationSpeed) + "\n";
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
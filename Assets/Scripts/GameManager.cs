using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [Range(0f, 2f)] public int KeyBoardPlayers = 2;
    [Range(0f, 2f)] public int ControllerPlayers;

    public Color ColorPlayer1 = Color.red;
    public Color ColorPlayer2 = Color.blue;
    public Color ColorPlayer3 = Color.yellow;
    public Color ColorPlayer4 = Color.green;

    public Vector3 SpawnPoint1 = new Vector3(-5, 0, 0);
    public Vector3 SpawnPoint2 = new Vector3(5, 0, 0);
    public Vector3 SpawnPoint3 = new Vector3(0, 3, 0);
    public Vector3 SpawnPoint4 = new Vector3(0, -3, 0);
    
    public GameObject PlayerPrefab;
    public GameObject PlayerStatsUiPrefab;
    public GameObject[] PowerUpPrefabs;

    private readonly List<Color> _playerColors = new List<Color>();
    private readonly List<Vector2> _playerUiAnchors = new List<Vector2>();
    private readonly List<Vector3> _playerSpawnPoints = new List<Vector3>();

    private readonly List<GameObject> _players = new List<GameObject>();
    private readonly Dictionary<GameObject, Text> _playerStatsUis = new Dictionary<GameObject, Text>();

    private bool _gameOver;
    private GameObject _canvas;
    private GameObject _gameOverText;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        _canvas = GameObject.Find("Canvas");
        AddColorsAndAnchorsToList();
        CreatePlayers();

        //Find the game over text
        _gameOverText = GameObject.Find("GameOverText");
        _gameOverText.SetActive(false);
    }

    private int GetKeyboardPlayers() {
        try {
            return MainMenuManager.Instance.KeyboardPlayers;
        } catch (NullReferenceException) {
            return KeyBoardPlayers;
        }
    }

    private int GetControllerPlayers() {
        try {
            return MainMenuManager.Instance.ControllerPlayers;
        } catch (NullReferenceException) {
            return ControllerPlayers;
        }
    }

    private void CreatePlayers() {
        int currentPlayerIndex = 1;
        for (int keyboardPlayers = 1; keyboardPlayers <= GetKeyboardPlayers(); keyboardPlayers++) {
            CreateHumanPlayer(currentPlayerIndex, "HorizontalKeyboard" + keyboardPlayers,
                "VerticalKeyboard" + keyboardPlayers,
                "FireKeyboard" + keyboardPlayers, "RotateTurretKeyboard" + keyboardPlayers);
            currentPlayerIndex++;
        }
        for (int controllerPlayers = 1; controllerPlayers <= GetControllerPlayers(); controllerPlayers++) {
            CreateHumanPlayer(currentPlayerIndex, "HorizontalController" + controllerPlayers,
                "VerticalController" + controllerPlayers,
                "FireController" + controllerPlayers, "RotateTurretController" + controllerPlayers);
            currentPlayerIndex++;
        }
    }

    private void AddColorsAndAnchorsToList() {
        _playerColors.Add(ColorPlayer1);
        _playerColors.Add(ColorPlayer2);
        _playerColors.Add(ColorPlayer3);
        _playerColors.Add(ColorPlayer4);
        _playerUiAnchors.Add(new Vector2(0, 1));
        _playerUiAnchors.Add(new Vector2(1, 1));
        _playerUiAnchors.Add(new Vector2(0, 0));
        _playerUiAnchors.Add(new Vector2(1, 0));
        _playerSpawnPoints.Add(SpawnPoint1);
        _playerSpawnPoints.Add(SpawnPoint2);
        _playerSpawnPoints.Add(SpawnPoint3);
        _playerSpawnPoints.Add(SpawnPoint4);
    }

    private void CreateHumanPlayer(int playerNumber, string horizontalAxis, string verticalAxis,
        string fireInput, string rotateTurretAxis) {
        GameObject player = Instantiate(PlayerPrefab, _playerSpawnPoints[playerNumber - 1], Quaternion.identity);
        player.name = "Player " + playerNumber + " Tank";
        player.GetComponent<HumanTankMovement>().SetAxis(horizontalAxis, verticalAxis);
        player.GetComponent<HumanTankShooting>().SetFireInput(fireInput, rotateTurretAxis);
        _players.Add((player));

        CreatePlayerStatsUi(_players.Count, player);
    }

    private void CreatePlayerStatsUi(int playerNumber, GameObject player) {
        GameObject playerStatsUi = Instantiate(PlayerStatsUiPrefab, Vector3.zero, Quaternion.identity);
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
        _players.Remove(player);
        Destroy(_playerStatsUis[player].gameObject);
    }

    void Update() {
        SetStatsUIs();
        CheckGameOver();

        // If gameOver, activate text and load MainMenu after 2 seconds
        if (_gameOver) {
            _gameOverText.GetComponent<Text>().text = "" + _players[0].name + " WINS!";
            _gameOverText.SetActive(true);
            Invoke("LoadMainMenu", 2f);
        }
    }

    void CheckGameOver() {
        _gameOver = (_players.Count == 1);
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
}
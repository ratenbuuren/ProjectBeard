using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    private static MainMenuManager _instance;

    private int _keyboardPlayers;
    private int _controllerPlayers;

    private GameObject _playButton;
    private GameObject _setupPanel;

    private GameObject _keyboardSlider;
    private GameObject _keyboardValue;
    private GameObject _controllerSlider;
    private GameObject _controllerValue;

    private GameObject _setupBackButton;
    private GameObject _setupReadyButton;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        _playButton = GameObject.Find("PlayButton");
        _playButton.GetComponent<Button>().onClick.AddListener(ShowSetupPanel);

        _setupPanel = GameObject.Find("SetupPanel");

        _keyboardSlider = GameObject.Find("KeyboardPlayersSlider");
        _keyboardSlider.GetComponent<Slider>().onValueChanged.AddListener(UpdateKeyboardPlayers);
        _keyboardValue = GameObject.Find("KeyboardPlayersValue");

        _controllerSlider = GameObject.Find("ControllerPlayersSlider");
        _controllerSlider.GetComponent<Slider>().onValueChanged.AddListener(UpdateControllerPlayers);
        _controllerValue = GameObject.Find("ControllerPlayersValue");

        _setupBackButton = GameObject.Find("SetupBackButton");
        _setupBackButton.GetComponent<Button>().onClick.AddListener(HideSetupPanel);

        _setupReadyButton = GameObject.Find("SetupReadyButton");
        _setupReadyButton.GetComponent<Button>().onClick.AddListener(LoadBattleArena);

        HideSetupPanel();
    }

    private void ShowSetupPanel() {
        _playButton.SetActive(false);
        _setupPanel.SetActive(true);
        _setupReadyButton.SetActive(false);
    }

    private void HideSetupPanel() {
        _setupPanel.SetActive(false);
        _playButton.SetActive(true);
    }

    private void UpdateKeyboardPlayers(float players) {
        _keyboardPlayers = (int) players;
        _keyboardValue.GetComponent<Text>().text = "" + _keyboardPlayers;
        CheckPlayerValues();
    }

    private void UpdateControllerPlayers(float players) {
        _controllerPlayers = (int) players;
        _controllerValue.GetComponent<Text>().text = "" + _controllerPlayers;
        CheckPlayerValues();
    }

    private void CheckPlayerValues() {
        int players = _keyboardPlayers + _controllerPlayers;
        _setupReadyButton.SetActive(players >= 2 && players <= 4);
    }

    public void LoadBattleArena() {
        SceneManager.LoadScene("BattleArena");
    }
}
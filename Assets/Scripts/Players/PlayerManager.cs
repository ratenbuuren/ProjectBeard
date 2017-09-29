using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public GameObject PlayerPrefab;
    
    private PlayerManager _playerManager;
    private Dictionary<PlayerType, int> _numPlayers;
    private readonly List<GameObject> _players = new List<GameObject>();
   
    public PlayerManager() {
        foreach (PlayerType type in Enum.GetValues(typeof(PlayerType))) {
            _numPlayers[type] = 0;
        }
    }
    
    public int NumPlayers(PlayerType type) {
        return _numPlayers[type];
    }

    public void SpawnPlayer(PlayerType type, Vector2 position) {
        switch (type) {
            case PlayerType.Human:
                SpawnHumanPlayer(position);
                break;
            case PlayerType.Ai:
                SpawnAiPlayer(position);
                break;
        }
    }

    private void SpawnHumanPlayer(Vector2 position) {
            GameObject player = Instantiate(PlayerPrefab, position, Quaternion.identity);
            player.name = "Player " + _numPlayers.Count + " Tank";
            player.GetComponent<HumanTankMovement>().SetAxis(horizontalAxis, verticalAxis);
            player.GetComponent<HumanTankShooting>().SetFireInput(fireInput, rotateTurretAxis);
            _players.Add((player));

            CreatePlayerStatsUi(_players.Count, player);
    }
    
    private void SpawnAiPlayer(Vector2 position) {
        throw new NotImplementedException();
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
}
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(GameManager))]
public class LevelCreator : MonoBehaviour {
    public int height = 10;
    public int width = 10;
    
    public GameObject ground = null;
    public GameObject edge = null;
    public GameObject edgeDecoration = null;

    private Dictionary<TerrainType, GameObject> _prefabs;
    private GameManager _gameManager;
    
    private void Start() {
        _gameManager = GetComponent<GameManager>();
        
        preparePrefabs();
        Level level = LevelGenerator.generate(height, width);
        setSpawnPoints(level);
        LevelRenderer.render(level, _prefabs);
    }

    private void setSpawnPoints(Level level) {
        
    }
    
    private void preparePrefabs() {
        _prefabs = new Dictionary<TerrainType, GameObject>();
        _prefabs.Add(TerrainType.Edge, edge);
        _prefabs.Add(TerrainType.Ground, ground);
        _prefabs.Add(TerrainType.EdgeDecoration, edgeDecoration);
    }
}
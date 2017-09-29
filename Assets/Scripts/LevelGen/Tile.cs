using System.Runtime.InteropServices;
using UnityEngine;

public struct Tile {
    public struct TerrainStruct {
        public TerrainType Type;
        public float Rotation;
    }

    public struct ObstacleStruct {
        public ObstacleType Type;
        public float Rotation;
    }

    public TerrainStruct terrain;
    public ObstacleStruct obstacle;
    
    public Tile(TerrainType terrainType, float terrainRotation = 0f, float obstacleRotation = 0f) : this(terrainType, ObstacleType.None, terrainRotation, obstacleRotation) {}

    public Tile(TerrainType terrainType, ObstacleType obstacleType, float terrainRotation = 0f, float obstacleRotation = 0f) {
        this.terrain = new TerrainStruct();
        this.terrain.Type = terrainType;
        this.terrain.Rotation = terrainRotation;
        
        this.obstacle = new ObstacleStruct();
        this.obstacle.Type = obstacleType;
        this.obstacle.Rotation = obstacleRotation;
    }
}
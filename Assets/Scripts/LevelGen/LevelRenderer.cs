using System.Collections.Generic;
using UnityEngine;

public class LevelRenderer : MonoBehaviour {
    
    private const string rootName = "Level";
    private static GameObject root;

    public static void render(Level level, Dictionary<TerrainType, GameObject> prefabs) {
        root = new GameObject(rootName);
        
        for (int x = 0; x < level.Width; x++) {
            for (int y = 0; y < level.Height; y++) {
                Tile tile = level.get(x, y);

                // create terrain
                new Prefabs.PrefabBuilder(prefabs[tile.terrain.Type])
                    .position(new Vector2(x, y))
                    .rotate(tile.terrain.Rotation)
                    .parent(root)
                    .build();
            }
        }        
    }
}
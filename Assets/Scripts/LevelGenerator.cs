using UnityEngine;
using System;

public class LevelGenerator : MonoBehaviour {
    private const string rootName = "Tiles";

    public int height = 10;
    public int width = 10;
    public GameObject tile = null;
    public GameObject edge = null;
    public GameObject edgeDecoration = null;
    [Range(0f, 1f)] public float edgeScale = 0.5f;

    public GameObject obstacle = null;

    private GameObject root;
    private float blockCenterOffset; // the offset of the block compared to the exact center of a 1x1 block

    void Start() {
        if (tile == null) {
            throw new Exception("Cannot create level out of empty tile objects");
        } else if (edge == null) {
            throw new Exception("Cannot create level out of empty edge objects");
        } else if (edgeDecoration == null) {
            throw new Exception("Cannot create level out of empty edge decoration objects");
        }

        root = new GameObject(rootName);
        blockCenterOffset = (0.5f - edgeScale / 2f);

        generatePlayingField();
        generateEdges();
        generateCorners();
        generateObstacles();
    }

    private void generatePlayingField() {
        float xPos = -(width / 2f) + 0.5f;
        for (int y = 0; y < height; y++) {
            float yPos = (float) y - (height / 2f) + 0.5f;
            generateRow(tile, new Vector2(xPos, yPos), Vector2.one, new FixedRotation(), width);
        }
    }

    private void generateEdges() {
        float xPos = -(width / 2f) + 0.5f;
        float yPos = (height / 2f) + 0.5f - blockCenterOffset;
        Rotation fixedRotation = new FixedRotation();
        Rotation randRotation = new RandomRotation(1);
        Vector2 scale = new Vector2(1, edgeScale);

        generateRow(edge, new Vector2(xPos, yPos), scale, fixedRotation, width);
        generateRow(edge, new Vector2(xPos, -yPos), scale, fixedRotation, width);
        generateRow(edgeDecoration, new Vector2(xPos, yPos), Vector2.one, randRotation, width);
        generateRow(edgeDecoration, new Vector2(xPos, -yPos), Vector2.one, randRotation, width);

        xPos = (width / 2f) + 0.5f - blockCenterOffset;
        yPos = -(height / 2f) + 0.5f;
        scale = new Vector2(edgeScale, 1);

        generateColumn(edge, new Vector2(xPos, yPos), scale, fixedRotation, height);
        generateColumn(edge, new Vector2(-xPos, yPos), scale, fixedRotation, height);
        generateColumn(edgeDecoration, new Vector2(xPos, yPos), Vector2.one, randRotation, height);
        generateColumn(edgeDecoration, new Vector2(-xPos, yPos), Vector2.one, randRotation, height);
    }

    private void generateCorners() {
        // top left
        float xPos = (width / 2f) + 0.5f - blockCenterOffset;
        float yPos = (height / 2f) + 0.5f - blockCenterOffset;

        Vector2[] positions = new Vector2[4] {
            new Vector2(xPos, yPos),
            new Vector2(-xPos, yPos),
            new Vector2(xPos, -yPos),
            new Vector2(-xPos, -yPos),
        };

        foreach (Vector2 position in positions) {
            new Prefabs.PrefabBuilder(edge)
                .position(position)
                .scale(edgeScale, edgeScale)
                .parent(root)
                .build();
        }
    }

    private void generateObstacles() {
        if (width >= 5 && height >= 5) {
            float xPos = (width / 2f) - 2.5f;
            float yPos = (height / 2f) - 2.5f;

            Vector2[] positions = new Vector2[4] {
                new Vector2(xPos, yPos),
                new Vector2(-xPos, yPos),
                new Vector2(xPos, -yPos),
                new Vector2(-xPos, -yPos),
            };

            foreach (Vector2 position in positions) {
                new Prefabs.PrefabBuilder(obstacle)
                    .position(position)
                    .parent(root)
                    .build();
            }
        } else {
            Debug.Log("Level is too small to generate obstacles");
        }
    }

    private GameObject[] generateRow(GameObject obj, Vector2 pos, Vector2 scale, Rotation rotation, int n) {
        return generateLine(obj, pos, scale, rotation, n, Direction.Horizontal);
    }

    private GameObject[] generateColumn(GameObject obj, Vector2 pos, Vector2 scale, Rotation rotation, int n) {
        return generateLine(obj, pos, scale, rotation, n, Direction.Vertical);
    }

    private GameObject[] generateLine(GameObject obj, Vector2 pos, Vector2 scale, Rotation rotation, int n,
        Direction dir) {
        GameObject[] objects = new GameObject[n];
        for (int i = 0; i < n; i++) {
            objects[i] = new Prefabs.PrefabBuilder(obj)
                .position(pos)
                .scale(scale)
                .rotate(rotation.value())
                .parent(root)
                .build();

            switch (dir) {
                case Direction.Horizontal:
                    pos.x++;
                    break;
                case Direction.Vertical:
                    pos.y++;
                    break;
                default:
                    throw new Exception("Failed to create line: invalid direction");
            }
        }
        return objects;
    }
}
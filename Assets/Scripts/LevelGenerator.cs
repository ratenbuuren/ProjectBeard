using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGenerator : MonoBehaviour {

	private const string rootName = "Tiles";

	public int height = 10;
	public int width = 10;
	public GameObject tile = null;
	public GameObject edge = null;

	private GameObject root = null;

	void Start () {
		if (tile == null) {
			throw new Exception ("Cannot create level out of empty tile objects");
		} else if (edge == null) {
			throw new Exception ("Cannot create level out of empty edge objects");
		}


		root = new GameObject (rootName);

		generatePlayingField ();
		generateEdges ();
	}

	private void generatePlayingField() {
		float xPos = -(width / 2f) + 0.5f;;
		for (int y = 0; y < height; y++) {
			float yPos = (float) y - (height / 2f) + 0.5f;
			generateRow (tile, new Vector2(xPos, yPos), width, new FixedRotation());
		}
	}

	private void generateEdges() {
		float xPos = -(width / 2f) - 0.5f;
		float yPos = (height / 2f) + 0.5f;
		Rotation rotation = new RandomRotation(90);
		
		generateRow (edge, new Vector2(xPos, yPos), width+2, rotation);
		generateRow (edge, new Vector2(xPos, -yPos), width+2, rotation);

		xPos = (width / 2f) + 0.5f;
		yPos = -(height / 2f) + 0.5f;

		generateColumn (edge, new Vector2(xPos, yPos), height, rotation);
		generateColumn (edge, new Vector2(-xPos, yPos), height, rotation);
	}

	private GameObject[] generateRow(GameObject obj, Vector2 pos, int n, Rotation rotation) {
		return generateLine (obj, pos, n, rotation, Direction.Horizontal);
	}

	private GameObject[] generateColumn(GameObject obj, Vector2 pos, int n, Rotation rotation) {
		return generateLine (obj, pos, n, rotation, Direction.Vertical);
	}

	private GameObject[] generateLine(GameObject obj, Vector2 pos, int n, Rotation rotation, Direction dir)
	{
		GameObject[] objects = new GameObject[n];
		for (int i = 0; i < n; i++) {
			objects[i] = new Prefabs.PrefabBuilder (obj)
				.position (pos)
				.parent (root)
				.rotate (rotation.value())
				.build ();
			
			switch (dir) {
				case Direction.Horizontal:
					pos.x++;
					break;
				case Direction.Vertical:
					pos.y++;
					break;
				default:
					throw new Exception ("Failed to create line: invalid direction");
			}
		}
		return objects;
	}
}

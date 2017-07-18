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
			generateRow (tile, xPos, yPos, width, new FixedRotation());
		}
	}

	private void generateEdges() {
		float xPos = -(width / 2f) - 0.5f;
		float yPos = (height / 2f) + 0.5f;
		Rotation rotation = new RandomRotation(90);
		
		generateRow (edge, xPos, yPos, width+2, rotation);
		generateRow (edge, xPos, -yPos, width+2, rotation);

		xPos = (width / 2f) + 0.5f;
		yPos = -(height / 2f) + 0.5f;

		generateColumn (edge, xPos, yPos, height, rotation);
		generateColumn (edge, -xPos, yPos, height, rotation);
	}

	private void generateRow(GameObject obj, float x, float y, int n, Rotation rotation) {
		generateLine (obj, x, y, n, rotation, Direction.Horizontal);
	}

	private void generateColumn(GameObject obj, float x, float y, int n, Rotation rotation) {
		generateLine (obj, x, y, n, rotation, Direction.Vertical);
	}

	private void generateLine(GameObject obj, float x, float y, int n, Rotation rotation, Direction dir) {
		for (int i = 0; i < n; i++) {
			new Prefabs.PrefabBuilder (obj)
				.position (x, y)
				.parent (root)
				.rotate (rotation.value())
				.build ();

			switch (dir) {
				case Direction.Horizontal:
					x++;
					break;
				case Direction.Vertical:
					y++;
					break;
				default:
					throw new Exception ("Failed to create line: invalid direction");
			}
		}
	}
}

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
			generateRow (tile, xPos, yPos, width, false);
		}
	}

	private void generateEdges() {
		float xPos = -(width / 2f) - 0.5f;
		float yPos = (height / 2f) + 0.5f;

		generateRow (edge, xPos, yPos, width+2, true);
		generateRow (edge, xPos, -yPos, width+2, true);

		xPos = (width / 2f) + 0.5f;
		yPos = -(height / 2f) + 0.5f;

		generateColumn (edge, xPos, yPos, height, true);
		generateColumn (edge, -xPos, yPos, height, true);
	}

	private void generateRow(GameObject obj, float x, float y, int n, bool randRotation) {
		generateLine (obj, x, y, n, randRotation, Direction.Horizontal);
	}

	private void generateColumn(GameObject obj, float x, float y, int n, bool randRotation) {
		generateLine (obj, x, y, n, randRotation, Direction.Vertical);
	}

	private void generateLine(GameObject obj, float x, float y, int n, bool randRotation, Direction dir) {
		for (int i = 0; i < n; i++) {
			float rotation = randRotation ? UnityEngine.Random.Range (0, 4) * 90 : 0f;
			new PrefabBuilder (obj)
				.position (x, y)
				.parent (root)
				.rotate (rotation)
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
		
	private enum Direction { 
		Vertical,
		Horizontal
	}

	private class PrefabBuilder {

		private GameObject obj;
		private GameObject template;

		public PrefabBuilder(GameObject template) {
			this.template = template;
			this.obj = Instantiate(template);
		}

		public PrefabBuilder position(float x, float y) {
			obj.transform.position = new Vector2 (x, y);
			obj.name = String.Format ("{0} [{1},{2}]", template.name, x, y);
			return this;
		}

		public PrefabBuilder parent(GameObject parent) { 
			obj.transform.parent = parent.transform;
			return this;
		}

		public PrefabBuilder rotate(float angle) {
			obj.transform.Rotate(new Vector3(0, 0, angle));
			return this;
		}

		public GameObject build() {
			return obj;
		}
	}
}

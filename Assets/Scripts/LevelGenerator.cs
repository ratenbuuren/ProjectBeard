using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGenerator : MonoBehaviour {

	private const string rootName = "Tiles";

	public int height = 10;
	public int width = 10;
	public GameObject tile = null;

	void Start () {
		if (tile == null) {
			throw new Exception ("Cannot create level out of empty tiles");
		}

		GameObject root = new GameObject (rootName);

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {

				float xPos = (float) x - (width / 2f) + 0.5f;
				float yPos = (float) y - (height / 2f) + 0.5f;

				tile = new PrefabBuilder (tile)
					.position (xPos, yPos)
					.parent (root)
					.build ();
			}
		}
	}
		
	private class PrefabBuilder {

		private GameObject obj;

		public PrefabBuilder(GameObject obj) {
			this.obj = Instantiate(obj);
		}

		public PrefabBuilder position(float x, float y) {
			obj.transform.position = new Vector2 (x, y);
			return this;
		}

		public PrefabBuilder parent(GameObject parent) { 
			obj.transform.parent = parent.transform;
			return this;
		}

		public GameObject build() {
			return obj;
		}
	}
}

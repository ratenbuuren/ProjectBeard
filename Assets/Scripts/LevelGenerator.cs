﻿using System.Collections;
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
			throw new Exception ("Cannot create level out of empty tiles");
		}

		root = new GameObject (rootName);

		generatePlayingField ();
		generateEdges ();
	}

	private void generatePlayingField() {
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {

				float xPos = (float) x - (width / 2f) + 0.5f;
				float yPos = (float) y - (height / 2f) + 0.5f;

				new PrefabBuilder (tile)
					.position (xPos, yPos)
					.parent (root)
					.build ();
			}
		}
	}

	private void generateEdges() {
		// create top and bottom row
		for (int x = 0; x < width+2; x++) { 
			float xPos = (float)x - (width / 2f) - 0.5f;
			float yPos = (height / 2f) + 0.5f;

			new PrefabBuilder (edge)
				.position (xPos, yPos)
				.parent (root)
				.build ();

			yPos = -yPos;

			new PrefabBuilder (edge)
				.position (xPos, yPos)
				.parent (root)
				.build ();
		}

		// create left and right column
		for (int y = 0; y < height; y++) {
			float xPos = (width / 2) + 0.5f;
			float yPos = (float) y - (height / 2f) + 0.5f;

			new PrefabBuilder (edge)
				.position (xPos, yPos)
				.parent (root)
				.build ();

			xPos = -xPos;

			new PrefabBuilder (edge)
				.position (xPos, yPos)
				.parent (root)
				.build ();
		}
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

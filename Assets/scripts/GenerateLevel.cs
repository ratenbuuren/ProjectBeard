using UnityEngine;
using System.Collections;

public class GenerateLevel : MonoBehaviour {
	
	public Sprite new_sprite;
	public GameObject[] islandPrefab;
	// 0 = sea
	// 1 = island
	private static int[][] level = 
	{
		new [] {1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1},
		new [] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
		new [] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
		new [] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
		new [] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
		new [] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
		new [] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
		new [] {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
	};
	private int X_MAX, Y_MAX;
	
	// helper method for InitiateLevelMatrix
	bool CheckTiles(int x, int y, bool up, bool down, bool left, bool right) {
		bool result = true;
		if (up)
			result &= (x != 0 && level [x - 1] [y] != 0);
		else 
			result &= (x == 0 || level [x - 1] [y] == 0);
		
		if (down)
			result &= (x != X_MAX-1 && level [x + 1] [y] != 0);
		else 
			result &= (x == X_MAX-1 || level [x + 1] [y] == 0);
		
		if (right)
			result &= (y != Y_MAX-1 && level [x] [y + 1] != 0);
		else 
			result &= (y == Y_MAX-1 || level [x] [y + 1] == 0);
		
		if (left)
			result &= (y != 0 && level [x] [y - 1] != 0);
		else 
			result &= (y == 0 || level [x] [y - 1] == 0);
		
		return result;
	}

	// Changes the values of the level matrix so that the correct island_X is connected to it
	void InitiateLevelMatrix() {
		for (int x=0; x<X_MAX; x++) {
			for (int y=0; y<Y_MAX; y++) {
				if (level[x][y]==0) continue;
				if (CheckTiles(x,y,false,false,false,false)) level[x][y] = 1;
				else if (CheckTiles(x,y,true,true,true,true)) level[x][y] = 2;
				else if (CheckTiles(x,y,false,false,true,true)) level[x][y] = 3;
				else if (CheckTiles(x,y,true,true,false,false)) level[x][y] = 4;
				else if (CheckTiles(x,y,false,false,false,true)) level[x][y] = 5;
				else if (CheckTiles(x,y,false,true,false,false)) level[x][y] = 6;
				else if (CheckTiles(x,y,false,false,true,false)) level[x][y] = 7;
				else if (CheckTiles(x,y,true,false,false,false)) level[x][y] = 8;
				else if (CheckTiles(x,y,false,true,false,true)) level[x][y] = 9;
				else if (CheckTiles(x,y,false,true,true,false)) level[x][y] = 10;
				else if (CheckTiles(x,y,true,false,true,false)) level[x][y] = 11;
				else if (CheckTiles(x,y,true,false,false,true)) level[x][y] = 12;
				else if (CheckTiles(x,y,true,false,true,true)) level[x][y] = 13;
				else if (CheckTiles(x,y,true,true,false,true)) level[x][y] = 14;
				else if (CheckTiles(x,y,false,true,true,true)) level[x][y] = 15;
				else if (CheckTiles(x,y,true,true,true,false)) level[x][y] = 16;
				else level[x][y] = 12;
			}
		}		
	}
	
	// Instatiates an island at the x,y location
	void CreateIsland(int x, int y, int tile_i) {
		float xpos = (x * 256f) / 100f;
		float ypos = ((-y + X_MAX/2) * 256f) / 100f;
		Vector3 position = new Vector3 (xpos, ypos, 0);
		
		//GameObject island = (GameObject)
		Instantiate(Resources.Load ("Island"), position, Quaternion.identity);
		
		//Sprite new_sprite = tile_1; //Resources.Load(("tile_1"), typeof(Sprite)) as Sprite; //Resources.Load<Sprite>("tile_1");
		
		
		//island.GetComponent<SpriteRenderer> ().sprite = new_sprite;
	}
	
	// Use this for initialization
	void Start () {
		X_MAX = level.Length;
		Y_MAX = level[0].Length;
		InitiateLevelMatrix ();
		for (int x=0; x<X_MAX; x++) {
			for (int y=0; y<Y_MAX; y++) {
				if (level[x][y]!=0)
					CreateIsland (y, x, level[x][y]);
			}
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

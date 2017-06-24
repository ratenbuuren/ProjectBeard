using UnityEngine;
using System.Collections;

public class ParseImage : MonoBehaviour {

	// color comparison (because comparing floats directly does not work nicely
	private static bool colorCompare(float r1, float g1, float b1, float r2, float g2, float b2) {
		float delta = 0.001f;
		return (Mathf.Abs (r1 - r2) < delta &&
						Mathf.Abs (g1 - g2) < delta &&
						Mathf.Abs (b1 - b2) < delta);
	}

	// translate RGB values to int values
	public static int getPixelValue(float r, float g, float b){ 
		if (colorCompare (r, g, b, 1f, 1f, 1f)) // white
			// empty position
			return 0;
		else if (colorCompare (r, g, b, 0f, 0f, 0f)) // black
			// wall
			return 1;
		else if (colorCompare (r, g, b, 0.1333333f, 0.6941177f, 0.2980392f)) { // dark green
			// Position of the player : make sure to only have one of these
			return 2;
		}
		else if (colorCompare(r,g,b,0.9294118f,0.1098039f,0.1411765f)) // red
			// Enemy
			return 3;
		else if (colorCompare(r,g,b,0.6392157f,0.2862745f,0.6431373f)) // purple
			// Change_captain
			return 4;
		else {
			print ("Unknown Color: (" + r + "f," + g + "f," + b + "f)");
			return 0;
		}
	}

	// Use this for initialization
	public static int[,] Parse (string filename) {
		Texture2D levelBitmap = Resources.Load( filename ) as Texture2D;

		int[,] level = new int[levelBitmap.width,levelBitmap.height];

		for (int x=0; x<levelBitmap.width; x++) {
			for (int y=0; y<levelBitmap.height; y++) {

				level[x,y] = getPixelValue(levelBitmap.GetPixel(x,y).r, 
				                            levelBitmap.GetPixel(x,y).g, 
				                            levelBitmap.GetPixel(x,y).b);
			}
		}
	
		//Debug.LogWarning( levelBitmap.GetPixel( 1 , 1 ).r );
		return level;
	}

}

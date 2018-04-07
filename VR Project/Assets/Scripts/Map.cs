using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Threading;



public class Map : MonoBehaviour {

	public GameObject hexPreFab;
	private const int DELAY = 5000;

	private static int width = 20;
	private static int height = 20;

	private static float XOffset = 0.882f;
    private static float ZOffset = 0.764f;

	private static GameObject[,] tiles = new GameObject[width,height];

	private static string[,] map;

	private static bool loopEdges = false;

    // Use this for initialization
    void Start () {
		initializeRandomBoard ();
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				float xPos = x * XOffset;
				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
				GameObject hex_go = (GameObject)Instantiate (hexPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);
             
				//name
				hex_go.name = "Hex_" + x + "_" + y;
				//its position

				hex_go.GetComponent<Hex> ().x = x;
				hex_go.GetComponent<Hex> ().y = y;

				hex_go.transform.SetParent (this.transform);
				hex_go.isStatic = true;

				tiles [x, y] = hex_go;
				MeshRenderer ColorHex = hex_go.GetComponentInChildren<MeshRenderer> ();
				if (2 < x && x < 17 && 2 < y && y < 17) { 
					if (map [x, y] == "Stable") {
						ColorHex.material.color = Color.green;
					}
					if (map [x, y] == "Polluted") {
						ColorHex.material.color = Color.yellow;
					}

					if (x == y || x - y == 1|| x-y == -1){
						if (map [x, y] == "Stable") {
							ColorHex.material.color = Color.cyan;
						}
						if (map [x, y] == "Polluted") {
							ColorHex.material.color = Color.blue;
						}
					}
				}
				else {
					if (map [x, y] == "Stable") {
						ColorHex.material.color = Color.cyan;
					}
					if (map [x, y] == "Polluted") {
						ColorHex.material.color = Color.blue;
					}
				}
			}
		}   
	}
	
	// Update is called once per frame
	void Update () {
		// Run the game until the Escape key is pressed.
		updateBoard();
		paintTiles ();
		// Wait for a bit between updates.
		Thread.Sleep(DELAY);
	}

	private static void paintTiles(){
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				MeshRenderer ColorHex = tiles[x,y].GetComponentInChildren<MeshRenderer> ();
				if (2 < x && x < 17 && 2 < y && y < 17) { 
					if (map [x, y] == "Stable") {
						ColorHex.material.color = Color.green;
					}
					if (map [x, y] == "Polluted") {
						ColorHex.material.color = Color.yellow;
					}

					if (x == y || x - y == 1|| x-y == -1){
						if (map [x, y] == "Stable") {
							ColorHex.material.color = Color.cyan;
						}
						if (map [x, y] == "Polluted") {
						ColorHex.material.color = Color.blue;
						}
					}
				}
				else {
					if (map [x, y] == "Stable") {
						ColorHex.material.color = Color.cyan;
					}
					if (map [x, y] == "Polluted") {
						ColorHex.material.color = Color.blue;
					}
				}
			}
		}
	}

	private static void initializeRandomBoard()
	{
		var random = new System.Random();
		map = new string[width, height];

		for (var y = 0; y < height; y++) {
			for (var x = 0; x < width; x++) {
				// Equal probability of being true or false.
				if (random.Next (2) == 0) {
					map [x, y] = "Stable";
					Debug.Log (x + "," + y + ": STABLE");
				} 
				else {
					map[x, y] = "Polluted";	
					Debug.Log (x + "," + y + ": POLLUTED");
				}
			}
		}
	}

	// Moves the board to the next state based on Conway's rules.
	private void updateBoard()
	{
		// A temp variable to hold the next state while it's being calculated.
		string [,] newMap = new string[width,height];

		for (var y = 0; y < height; y++) {
			for (var x = 0; x < width; x++) {
				var n = countLiveNeighbors(x, y);
				var k = map[x, y];

				// A live cell dies unless it has exactly 2 or 3 live neighbors.
				// A dead cell remains dead unless it has exactly 3 live neighbors.
				if ((k == "Stable" && (n == 2 || n == 3)) || (k == "Polluted" && n == 3)) {
					newMap [x, y] = "Stable";
				} else {
					newMap [x, y] = "Polluted";

				}
			}
		}
		// Set the board to its new state.
		map = newMap;
	}

	// Returns the number of live neighbors around the cell at position (x,y).
	private static int countLiveNeighbors(int x, int y)
	{
		// The number of live neighbors.
		int val = 0;

		// This nested loop enumerates the 9 cells in the specified cells neighborhood.
		for (var j = -1; j <= 1; j++) {
			// If loopEdges is set to false and y+j is off the board, continue.
			if (!loopEdges && y + j < 0 || y + j >= height) {
				continue;
			}

			// Loop around the edges if y+j is off the board.
			int k = (y + j + height) % height;

			for (var i = -1; i <= 1; i++) {
				// If loopEdges is set to false and x+i is off the board, continue.
				if (!loopEdges && x + i < 0 || x + i >= width) {
					continue;
				}

				// Loop around the edges if x+i is off the board.
				int h = (x + i + width) % width;

				// Count the neighbor cell at (h,k) if it is alive.
				val += map [h, k] == "Stable" ? 1 : 0;

			}
		}

		// Subtract 1 if (x,y) is alive since we counted it as a neighbor.
		return val - (map[x, y]== "Stable" ? 1 : 0);
	}
}

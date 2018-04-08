using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class Map : MonoBehaviour {

	public int Wood = 0;
	public int Food = 0;
	public int Water = 0;
	public int Minerals = 0;
	public int Energy = 100;

	public GameObject hexPreFab;
	public GameObject mountainPreFab;
	public GameObject cityPreFab;
	public GameObject forestPreFab;
	public GameObject sheepPreFab;

	private int SheepCount = 20; 
	private int CityCount = 1;
	private int MountainCount = 15;
	private int ForestCount = 25;

    public Slider EnergySlider;

	private const int DELAY = 5000;

	private static int width = 20;
	private static int height = 20;

	private static float XOffset = 0.882f;
    private static float ZOffset = 0.764f;


	// contains int entries reflecting what is the tile:
		// 0 - water, 1 - land, 2 - sheep, 3 - forest, 4 - mountains, 5 - city
	private static int[,] mapType = new int[width,height];

	// contains all the actual tiles, used for painting colors
	private static GameObject[,] tiles = new GameObject[width,height];


	// contains the current state of the tile at the [x,y] coordinate 
	// states are either polluted or stable
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
					mapType [x, y] = 1;
					if (map [x, y] == "Stable") {
						ColorHex.material.color = Color.green;
					}
					if (map [x, y] == "Polluted") {
						ColorHex.material.color = Color.yellow;
					}
				}

				else {
					mapType [x, y] = 0;
					if (map [x, y] == "Stable") {
						ColorHex.material.color = Color.cyan;
					}
					if (map [x, y] == "Polluted") {
						ColorHex.material.color = Color.blue;
					}
				}
		
			}
		}
		MakeCity ();
		MakeMoutain ();
		MakeForest ();
		MakeSheep ();

        EnergySlider = GameObject.Find("EnergySlider").GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
		// Run the game until the Escape key is pressed.
		if (Energy < 0){
			updateBoard();
			Energy = 100;
		}
		paintTiles ();
		gatherResource ();
        updateEnergy ();
		// Wait for a bit between updates.
		//Thread.Sleep(DELAY);
	}


	public void gatherResource(){
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (map [x, y] == "Stable") {
					if (mapType [x, y] == 2) {
						Debug.Log (x + "," + y + ": FOOD");
						Food += 10;
						Debug.Log ("Current Food: "+Food);
						Energy -= 5;
					}
					else if (mapType [x, y] == 3) {
						Debug.Log (x + "," + y + ": WOOD");
						Wood += 10;
						Debug.Log ("Current Wood: "+Wood);
						Energy -= 5;
					} 
					else if (mapType [x, y] == 4) {
						Debug.Log (x + "," + y + ": Minerals");
						Minerals += 10;
						Debug.Log ("Current Minerals: " + Minerals);
						Energy -= 5;
					}
                    updateEnergy();
                }
			}
		}
	}

    public void updateEnergy()
    {
        Debug.Log("ENERGY: " + EnergySlider.value);
        this.EnergySlider.value = this.Energy;
    }

	public void MakeMoutain(){
		int count = 0;
		while (count < MountainCount){
			int x = UnityEngine.Random.Range(0, width);
			int y = UnityEngine.Random.Range(0, height);
			if (mapType [x, y] == 1) {
				float xPos = x * XOffset;
				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
				GameObject mountain_go = (GameObject)Instantiate (mountainPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);
				//transform.localScale -= new Vector3 (0.1F, 0, 0);
				//name
				mountain_go.name = "Mountain_" + x + "_" + y;
				mountain_go.transform.localScale -= new Vector3 (0.9F, 0.9F, 0.8F);
				mapType [x, y] = 4;
				count += 1;
			}
		}
	}

	public void MakeCity(){
		int count = 0;
		while (count < CityCount){
			int x = UnityEngine.Random.Range(0, width);
			int y = UnityEngine.Random.Range(0, height);
			if (mapType [x, y] == 1) {
				float xPos = x * XOffset;
				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
				GameObject city_go = (GameObject)Instantiate (cityPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);
				//transform.localScale -= new Vector3 (0.1F, 0, 0);
				//name
				city_go.name = "City_" + x + "_" + y;
				city_go.transform.localScale -= new Vector3 (0.3F, 0.3F, 0.3F);
				mapType [x, y] = 5;
				count += 1;
			}
		}
	}

	public void MakeSheep(){
		int count = 0;
		while (count < SheepCount){
			int x = UnityEngine.Random.Range(0, width);
			int y = UnityEngine.Random.Range(0, height);
			if (mapType [x, y] == 1) {
				float xPos = x * XOffset;
				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
				GameObject sheep_go = (GameObject)Instantiate (sheepPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);
				//transform.localScale -= new Vector3 (0.1F, 0, 0);
				//name
				sheep_go.name = "Sheep_" + x + "_" + y;
				sheep_go.transform.localScale -= new Vector3 (0.5F, 0.5F, 0.5F);
				mapType [x, y] = 2;
				count += 1;
			}
		}
	}
		
	public void MakeForest(){
		int count = 0;
		while (count < ForestCount){
			int x = UnityEngine.Random.Range(0, width);
			int y = UnityEngine.Random.Range(0, height);
			if (mapType [x, y] == 1) {
				float xPos = x * XOffset;
				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
				GameObject forest_go = (GameObject)Instantiate (forestPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);
				//transform.localScale -= new Vector3 (0.1F, 0, 0);
				//name
				forest_go.name = "Forest_" + x + "_" + y;
				forest_go.transform.localScale -= new Vector3 (0.6F, 0.6F, 0.6F);
				mapType [x, y] = 3;
				count += 1;
			}
		}
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
					//Debug.Log (x + "," + y + ": STABLE");
				} 
				else {
					map[x, y] = "Polluted";	
					//Debug.Log (x + "," + y + ": POLLUTED");
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
					//Debug.Log ("THIS IS NOW STABLe: "+x+","+y);
					newMap [x, y] = "Stable";
				} else {
					//Debug.Log ("THIS IS NOW POLLUTED: "+x+","+y);
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

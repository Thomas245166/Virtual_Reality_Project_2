﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour {
	
	public int Turn = 0;

	// Resources at the start of the game
	public int Wood = 0;
	public int Food = 0;
	public int Water = 0;
	public int Minerals = 0;
	public int Energy = 100;
    public int People = 0;
    public int Cities = 0;

	public GameObject hexPreFab;
	public GameObject mountainPreFab;
	public GameObject cityPreFab;
	public GameObject forestPreFab;
	public GameObject sheepPreFab;
	public GameObject loggingPreFab;
	public GameObject popUpPreFab;

    //size in tiles of how big the board is
    public static int width = 15;
	public static int height = 15;
    
	// total number of each land tile type on the board
	private static int TileNum = (height-6) * (width-6);

    private int SheepCount = TileNum / 4;
    private int CityCount = 1;
    private int MountainCount = TileNum / 5;
    private int ForestCount = TileNum / 3;

    public Slider EnergySlider;
    public Text EnergyAmount, FoodCount, MineralsCount, WoodCount, PeopleCount, TurnCount;

	//delay for testing
	private const int DELAY = 5000;

	private static float XOffset = 0.882f;
    private static float ZOffset = 0.764f;

	public int GoodPopCount;
	public int BadPopCount;

	// contains int entries reflecting what is the tile:
		// 0 - water, 1 - land, 2 - sheep, 3 - forest, 4 - mountains, 5 - city
	public static TileType[,] mapType = new TileType[width,height];

	// contains all the actual tiles, used for painting colors
	private static GameObject[,] tiles = new GameObject[width,height];

	// contains the current state of the tile at the [x,y] coordinate 
	// states are either polluted or stable
	public static Status[,] map;

	private static bool loopEdges = false;

    #region Initialization
    // Use this for initialization
    void Start () {
        Debug.Log("Tile Number" + TileNum);
        Debug.Log("Sheep Count: " + SheepCount + "Wood Count: " + WoodCount + "Mineral Count: " + MountainCount + "City Count: " + CityCount);
        initializeRandomBoard();
        for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				float xPos = x * XOffset;
				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
				GameObject hex_go = (GameObject)Instantiate(hexPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);
                var createdHex = hex_go.GetComponent<Hex>();

                GameObject pop_go = Instantiate(popUpPreFab, new Vector3(xPos, 0, y * ZOffset), Quaternion.identity);
                pop_go.name = "Pop_" + x + "_" + y;
                var createdPopUp = pop_go.GetComponent<PopUp>();
                var group = createdPopUp.GetComponent<CanvasGroup>();
                group.alpha = 0f;
                group.interactable = false;
                group.blocksRaycasts = false;

                //name
                hex_go.name = "Hex_" + x + "_" + y;

                //its position
                //hex_go.GetComponent<Hex>().x = x;
                //hex_go.GetComponent<Hex>().y = y;
                createdHex.x = x;
                createdHex.y = y;

                //meta info
                hex_go.transform.SetParent(this.transform);
                hex_go.isStatic = true;

                //add click stuff - work in progress
                hex_go.AddComponent<BoxCollider>();
                hex_go.GetComponent<BoxCollider>().isTrigger = true;
                hex_go.AddComponent<EventTrigger>();

                // add it to the array of tiles we have
                tiles[x, y] = hex_go;
                
				// get its render
				MeshRenderer ColorHex = hex_go.GetComponentInChildren<MeshRenderer> ();

                // if in the center set to 1 for land in maptype
                // color
                // 3 border water
                //if (2 < x && x < (width - 3) && 2 < y && y < (height - 3))
                
                // 2 border water
                if (1 < x && x < (width - 2) && 1 < y && y < (height - 2))
                {
                    mapType[x, y] = TileType.Land;
                    if (map[x, y] == Status.Stable)
                    {
                        ColorHex.material.color = Color.green;
                    }
                    if (map[x, y] == Status.Polluted)
                    {
                        ColorHex.material.color = Color.yellow;
                    }
                }
                // if on the edges set to 0 for water and color
                else
                {
                    mapType[x, y] = TileType.Water;
                    if (map[x, y] == Status.Stable)
                    {
                        ColorHex.material.color = Color.blue;
                    }
                    if (map[x, y] == Status.Polluted)
                    {
                        ColorHex.material.color = Color.gray;
                    }
                    //createdHex.popUp = Instantiate
                }
                createdHex.type = mapType[x, y];
                createdHex.status = map[x, y];
			}
		}

		// create the pieces on the board
		MakeCity ();
		MakeMountain ();
		MakeForest ();
		MakeSheep ();
        InitializeUIElements();
    }

    private void InitializeUIElements()
    {
        EnergySlider = GameObject.Find("EnergySlider").GetComponent<Slider>();
        EnergyAmount = GameObject.Find("EnergyAmount").GetComponent<Text>();
        FoodCount = GameObject.Find("FoodCount").GetComponent<Text>();
        MineralsCount = GameObject.Find("MineralsCount").GetComponent<Text>();
        PeopleCount = GameObject.Find("PeopleCount").GetComponent<Text>();
        TurnCount = GameObject.Find("TurnCount").GetComponent<Text>();
        WoodCount = GameObject.Find("WoodCount").GetComponent<Text>();
    }
    #endregion

    // Update is called once per frame
    void Update () {
		if (Energy <= 0){
			updateBoard ();
			Energy = 100;
            Turn += 1;
		}
		paintTiles ();
        updateResources ();
	}

    #region Gathering
    public void gatherResourceFood(){
		if (Energy >= 10) {
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					if (map [x, y] == Status.Stable) {
						if (mapType [x, y] == TileType.Sheep) {
							//Debug.Log (x + "," + y + ": FOOD");
							Food += 2;
							//Debug.Log ("Current Food: "+Food);
						} 
					}
				}
			}
			Debug.Log ("Current Food: " + Food);
			Energy -= 10;
			updateResources ();
		}
		else if (Energy <= 0) {
			Energy = 100;
            Turn += 1;
		}
	}


	public void gatherResourceWood(){
		if (Energy >= 10) {
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					if (map [x, y] == Status.Stable) {
						if (mapType [x, y] == TileType.Forest) {
							//Debug.Log (x + "," + y + ": Minerals");
							Wood += 2;
							//Debug.Log ("Current Minerals: " + Minerals);
						}
					}
				}
			}

			Debug.Log ("Current Wood: " + Wood);
			Energy -= 10;
			updateResources ();
		}
		else if (Energy <= 0) {
			Energy = 100;
            Turn += 1;
		}
	}


	public void gatherResourceMineral(){
		if (Energy >= 10) {
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					if (map [x, y] == Status.Stable) {
						if (mapType [x, y] == TileType.Mountain) {
							//Debug.Log (x + "," + y + ": Minerals");
							Minerals += 2;
							//Debug.Log ("Current Minerals: " + Minerals);
						}
					}
				}
			}
	
			Debug.Log ("Current Minerals: " + Minerals);
			Energy -= 10;
			updateResources ();
		}else if (Energy <= 0) {
			Energy = 100;
            Turn += 1;
		}
	}

	public void gatherResourceWater(){
		if (Energy >= 10) {
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					if (map [x, y] == Status.Stable) {
						if (mapType [x, y] == 0) {
							//Debug.Log (x + "," + y + ": Minerals");
							Water += 2;
							//Debug.Log ("Current Minerals: " + Minerals);
						}
					}
				}
			}
			Debug.Log ("Current Minerals: " + Minerals);
			Energy -= 10;
			updateResources ();
		}else if (Energy <= 0) {
			Energy = 100;
            Turn += 1;
            // TODO: instead of just moving to the next turn,
            // tell the player they don't have enough energy to 
            // perform this action in this turn***
            // (Same for other gathering methods)
		}
	}
    #endregion

    public void updateResources()
    {
       // Debug.Log("ENERGY: " + EnergySlider.value);
        this.EnergySlider.value = this.Energy;
        this.EnergyAmount.text = this.Energy.ToString();
        this.FoodCount.text = this.Food.ToString();
        this.WoodCount.text = this.Wood.ToString();
        this.MineralsCount.text = this.Minerals.ToString();
        this.PeopleCount.text = this.People.ToString();
        this.TurnCount.text = this.Turn.ToString();
		RenderSettings.skybox.SetFloat("_Blend", 0.01f*Mathf.Round(this.Energy));
    }

    #region Add Models/Build on Tiles
    public void MakeMountain(){
		int count = 0;
		while (count < MountainCount){
			int x = UnityEngine.Random.Range(0, width);
			int y = UnityEngine.Random.Range(0, height);
			if (mapType [x, y] == TileType.Land) {
				float xPos = x * XOffset;
				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
                var euler = transform.eulerAngles;
                euler.y = UnityEngine.Random.Range(45f, 260f);
                GameObject mountain_go = (GameObject)Instantiate (mountainPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);
				//transform.localScale -= new Vector3 (0.1F, 0, 0);
				//name
				mountain_go.name = "Mountain_" + x + "_" + y;
				mountain_go.transform.localScale -= new Vector3 (0.9F, 0.9F, 0.8F);
                mountain_go.transform.eulerAngles = euler;
                mapType [x, y] = TileType.Mountain;
				count += 1;

                GameObject hex_go = tiles[x, y];
                var mountainTile = hex_go.GetComponent<Hex>();
                mountainTile.type = TileType.Mountain;
            }
		}
	}

	public void MakeCity(){
		int count = 0;
		while (count < CityCount){
			int x = UnityEngine.Random.Range(0, width);
			int y = UnityEngine.Random.Range(0, height);
			if (mapType [x, y] == TileType.Land) {
				float xPos = x * XOffset;
				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
				GameObject city_go = (GameObject)Instantiate (cityPreFab, new Vector3 (xPos,(float).175 , y * ZOffset), Quaternion.identity);
				//transform.localScale -= new Vector3 (0.1F, 0, 0);
				//name
				city_go.name = "City_" + x + "_" + y;
				city_go.transform.localScale -= new Vector3 (0.3F, 0.3F, 0.3F);
				mapType [x, y] = TileType.City;
				People += 10;
				count += 1;
                Cities += 1;
                
                GameObject hex_go = tiles[x, y];
                var cityTile = hex_go.GetComponent<Hex>();
                cityTile.type = TileType.City;
			}
		}
	}

	public void MakeSheep(){
		int count = 0;
		while (count < SheepCount){
			int x = UnityEngine.Random.Range(0, width);
			int y = UnityEngine.Random.Range(0, height);
			if (mapType [x, y] == TileType.Land) {
				float xPos = x * XOffset;
				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
                var euler = transform.eulerAngles;
                euler.y = UnityEngine.Random.Range(0f, 45f);
            
                GameObject sheep_go = (GameObject)Instantiate (sheepPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);
				//transform.localScale -= new Vector3 (0.1F, 0, 0);
				//name
				sheep_go.name = "Sheep_" + x + "_" + y;
				sheep_go.transform.localScale -= new Vector3 (0.5F, 0.5F, 0.5F);
                sheep_go.transform.eulerAngles = euler;
                mapType [x, y] = TileType.Sheep;
				count += 1;

                GameObject hex_go = tiles[x, y];
                var sheepTile = hex_go.GetComponent<Hex>();
                sheepTile.type = TileType.Sheep;
            }
		}
	}
		
	public void MakeForest(){
		int count = 0;
		while (count < ForestCount){
			int x = UnityEngine.Random.Range(0, width);
			int y = UnityEngine.Random.Range(0, height);
			if (mapType [x, y] == TileType.Land) {
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
				mapType [x, y] = TileType.Forest;
				count += 1;

                GameObject hex_go = tiles[x, y];
                var forestTile = hex_go.GetComponent<Hex>();
                forestTile.type = TileType.Forest;
            }
		}
	}

    
    public void MakeTree(){
		bool run = true;
		int count = 0;
		while (run && (count < 25)) {
			int x = UnityEngine.Random.Range (0, width);
			int y = UnityEngine.Random.Range (0, height);

			if (mapType [x, y] == TileType.Land) {
				float xPos = x * XOffset;

				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}

				GameObject forest_go = (GameObject)Instantiate (forestPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);

				//name
				forest_go.name = "Forest_" + x + "_" + y;
				forest_go.transform.localScale -= new Vector3 (0.6F, 0.6F, 0.6F);

				mapType [x, y] = TileType.Forest;

				map [x, y] = Status.Stable;
				Energy -= 20;
				run = false;
                
				GameObject hex_go = tiles[x, y];
                
				var forestTile = hex_go.GetComponent<Hex>();
                forestTile.type = TileType.Forest;
                forestTile.status = Status.Stable;
			}
			count++;
		}
	}

	public void UpgradeWood(int x, int y){
		float xPos = x * XOffset;
		//Is the Row odd
		if (y % 2 == 1) {
			xPos += XOffset / 2f;
		}
		GameObject forest = GameObject.Find ("Forest_" + x + "_" + y);
		Destroy (forest);
		GameObject logging_go = (GameObject)Instantiate (loggingPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);

		logging_go.name = "Logging_" + x + "_" + y;
		//forest_go.transform.localScale -= new Vector3 (0.6F, 0.6F, 0.6F);

		mapType [x, y] = TileType.Logging;
		map [x, y] = Status.Stable;
		Energy -= 20;
		GameObject hex_go = tiles[x, y];

		var forestTile = hex_go.GetComponent<Hex>();
		forestTile.type = TileType.Logging;
		forestTile.status = Status.Stable;
	}





	public void MakeMine(){
		bool run = true;
		int count = 0;
		while (run && (count < 25)) {
			int x = UnityEngine.Random.Range (0, width);
			int y = UnityEngine.Random.Range (0, height);
			if (mapType [x, y] == TileType.Land) {
				float xPos = x * XOffset;
				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
                GameObject mine_go = (GameObject)Instantiate (mountainPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);
				//transform.localScale -= new Vector3 (0.1F, 0, 0);
				//name
				mine_go.name = "Mine_" + x + "_" + y;
				mine_go.transform.localScale -= new Vector3 (0.6F, 0.6F, 0.6F);
				mapType [x, y] = TileType.Mountain;
				Energy -= 20;
				map [x, y] = Status.Stable;
				run = false;

                GameObject hex_go = tiles[x, y];
                var mountainTile = hex_go.GetComponent<Hex>();
                mountainTile.type = TileType.Mountain;
                mountainTile.status = Status.Stable;
			}
			count++;
		}
	}

	public void MakeFarm(){
		bool run = true;
		int count = 0;
		while (run&&(count < 25)) {
			int x = UnityEngine.Random.Range (0, width);
			int y = UnityEngine.Random.Range (0, height);
			if (mapType [x, y] == TileType.Land) {
				float xPos = x * XOffset;
				//Is the Row odd
				if (y % 2 == 1) {
					xPos += XOffset / 2f;
				}
				GameObject farm_go = (GameObject)Instantiate (sheepPreFab, new Vector3 (xPos, 0, y * ZOffset), Quaternion.identity);
				//transform.localScale -= new Vector3 (0.1F, 0, 0);
				//name
				farm_go.name = "Farm_" + x + "_" + y;
				farm_go.transform.localScale -= new Vector3 (0.6F, 0.6F, 0.6F);
				mapType [x, y] = TileType.Sheep;
				map [x, y] = Status.Stable;
				Energy -= 20;
				run = false;

                GameObject hex_go = tiles[x, y];
                var farmTile = hex_go.GetComponent<Hex>();
                farmTile.type = TileType.Sheep;
                farmTile.status = Status.Stable;
			}
			count++;
		}
	}

	public void BuildCity(){
		bool run = true;
		int count = 0;
		if (Minerals > 500  && Wood > 500 ){
			while (run && (count < 25)){
				int x = UnityEngine.Random.Range(0, width);
				int y = UnityEngine.Random.Range(0, height);
				if (mapType [x, y] == TileType.Land) {
					float xPos = x * XOffset;
					//Is the Row odd
					if (y % 2 == 1) {
						xPos += XOffset / 2f;
					}
					GameObject city_go = (GameObject)Instantiate (cityPreFab, new Vector3 (xPos,(float).175 , y * ZOffset), Quaternion.identity);
					//transform.localScale -= new Vector3 (0.1F, 0, 0);
					//name
					city_go.name = "City_" + x + "_" + y;
					city_go.transform.localScale -= new Vector3 (0.3F, 0.3F, 0.3F);
					mapType [x, y] = TileType.City;
					map [x, y] = Status.Stable;
					People += 10;
					Minerals -= 500;
					Wood -= 500;
					Energy -= 50;
					run = false;

                    Cities += 1;

                    GameObject hex_go = tiles[x, y];
                    var cityTile = hex_go.GetComponent<Hex>();
                    cityTile.type = TileType.City;
                    cityTile.status = Status.Stable;
				}
				count++;
			}
		}
	}
    #endregion

    #region Cleaning Specific Tile
    public void CleanWater(int x, int y)
    {
        if (mapType[x, y] == TileType.Water && map[x, y] == Status.Polluted)
        {
            Energy -= 10;
            map[x, y] = Status.Stable;

            GameObject hex_go = tiles[x, y];
            var tile = hex_go.GetComponent<Hex>();
            tile.status = Status.Stable;
        }
    }

    public void CleanLand(int x, int y)
    {
        if (mapType[x, y] == TileType.Land && map[x, y] == Status.Polluted)
        {
            Energy -= 10;
            map[x, y] = Status.Stable;

            GameObject hex_go = tiles[x, y];
            var tile = hex_go.GetComponent<Hex>();
            tile.status = Status.Stable;
        }
    }

    public void CleanCity(int x, int y)
    {
        if (mapType[x, y] == TileType.City && map[x, y] == Status.Polluted)
        {
            Energy -= 10;
            map[x, y] = Status.Stable;

            GameObject hex_go = tiles[x, y];
            var tile = hex_go.GetComponent<Hex>();
            tile.status = Status.Stable;
        }
    }

    public void CleanWood(int x, int y)
    {
        if (mapType[x, y] == TileType.Forest && map[x, y] == Status.Polluted)
        {
            Energy -= 10;
            map[x, y] = Status.Stable;

            GameObject hex_go = tiles[x, y];
            var tile = hex_go.GetComponent<Hex>();
            tile.status = Status.Stable;
        }
    }

    public void CleanFood(int x, int y)
    {
        if (mapType[x, y] == TileType.Sheep && map[x, y] == Status.Polluted)
        {
            Energy -= 10;
            map[x, y] = Status.Stable;

            GameObject hex_go = tiles[x, y];
            var tile = hex_go.GetComponent<Hex>();
            tile.status = Status.Stable;
        }
    }

    public void CleanMineral(int x, int y)
    {
        if (mapType[x, y] == TileType.Mountain && map[x, y] == Status.Polluted)
        {
            Energy -= 10;
            map[x, y] = Status.Stable;

            GameObject hex_go = tiles[x, y];
            var tile = hex_go.GetComponent<Hex>();
            tile.status = Status.Stable;
        }
    }
    #endregion

    #region Cleaning - Random
    public void CleanWater()
    {
        bool run = true;
        while (run)
        {
            int x = UnityEngine.Random.Range(0, width);
            int y = UnityEngine.Random.Range(0, height);
            if (mapType[x, y] == TileType.Water && map[x, y] == Status.Polluted)
            {
                Energy -= 10;
                map[x, y] = Status.Stable;
                run = false;

                GameObject hex_go = tiles[x, y];
                var tile = hex_go.GetComponent<Hex>();
                tile.status = Status.Stable;
            }
        }
    }

    public void CleanWood()
    {
        bool run = true;
        while (run)
        {
            int x = UnityEngine.Random.Range(0, width);
            int y = UnityEngine.Random.Range(0, height);
            if (mapType[x, y] == TileType.Forest && map[x, y] == Status.Polluted)
            {
                Energy -= 10;
                map[x, y] = Status.Stable;
                run = false;

                GameObject hex_go = tiles[x, y];
                var tile = hex_go.GetComponent<Hex>();
                tile.status = Status.Stable;
            }
        }
    }
    public void CleanFood()
    {
        bool run = true;
        while (run)
        {
            int x = UnityEngine.Random.Range(0, width);
            int y = UnityEngine.Random.Range(0, height);
            if (mapType[x, y] == TileType.Sheep && map[x, y] == Status.Polluted)
            {
                Energy -= 10;
                map[x, y] = Status.Stable;
                run = false;

                GameObject hex_go = tiles[x, y];
                var tile = hex_go.GetComponent<Hex>();
                tile.status = Status.Stable;
            }
        }
    }
    public void CleanMineral()
    {
        bool run = true;
        while (run)
        {
            int x = UnityEngine.Random.Range(0, width);
            int y = UnityEngine.Random.Range(0, height);
            if (mapType[x, y] == TileType.Mountain && map[x, y] == Status.Polluted)
            {
                Energy -= 10;
                map[x, y] = Status.Stable;
                run = false;

                GameObject hex_go = tiles[x, y];
                var tile = hex_go.GetComponent<Hex>();
                tile.status = Status.Stable;
            }
        }
    }
    #endregion

    #region Painting
    private static void paintTiles(){
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				MeshRenderer ColorHex = tiles[x,y].GetComponentInChildren<MeshRenderer> ();
                // bigger water border
                //if (2 < x && x < (width -3) && 2 < y && y < (height -3)) { 
                // smaller water border
                if (1 < x && x < (width - 2) && 1 < y && y < (height - 2))
                {
                    if (map [x, y] == Status.Stable) {
						ColorHex.material.color = Color.green;
					}
					if (map [x, y] == Status.Polluted) {
						ColorHex.material.color = Color.yellow;
					}
				}
				else {
					if (map [x, y] == Status.Stable) {
						ColorHex.material.color = Color.blue;
					}
					if (map [x, y] == Status.Polluted) {
						ColorHex.material.color = Color.grey;
					}
				}
			}
		}
	}
    #endregion

    #region Random Board
    private static void initializeRandomBoard()
	{
		var random = new System.Random();
		map = new Status[width, height];

		for (var y = 0; y < height; y++) {
			for (var x = 0; x < width; x++) {
				// Equal probability of being true or false.
				if (random.Next(2) == 0) {
					map [x, y] = Status.Stable;
					//Debug.Log (x + "," + y + ": STABLE");
				} 
				else {
					map[x, y] = Status.Polluted;	
					//Debug.Log (x + "," + y + ": POLLUTED");
				}
			}
		}
	}
    #endregion

    #region Update Board
    // Moves the board to the next state based on Conway's rules.
    private void updateBoard()
	{
		// A temp variable to hold the next state while it's being calculated.
		Status [,] newMap = new Status[width,height];

		for (var y = 0; y < height; y++) {
			for (var x = 0; x < width; x++) {
				var n = countLiveNeighbors(x, y);
				var k = map[x, y];

				// A live cell dies unless it has exactly 2 or 3 live neighbors.
				// A dead cell remains dead unless it has exactly 3 live neighbors.
				if ((k == Status.Stable && (n >= 2)) || (k == Status.Polluted && n >= 5)) {
					//Debug.Log ("THIS IS NOW STABLe: "+x+","+y);
					newMap [x, y] = Status.Stable;
				} else {
					//Debug.Log ("THIS IS NOW POLLUTED: "+x+","+y);
					newMap [x, y] = Status.Polluted;
				}
			}
		}
		// Set the board to its new state.
		map = newMap;
		checkFood ();
		Debug.Log (People);
	}

    public void UpdateButtonPush()
    {
        Energy = 100;
        updateResources();
        updateBoard();
        paintTiles();
        Turn += 1;
    }
    #endregion

    public void checkFood(){
		Food -= (People * 2);
		Debug.Log (People);
		if (Food > 0) {
			BadPopCount = 0;
			GoodPopCount++;
		}else if (Food < 0) {
			GoodPopCount = 0;
			BadPopCount++;
		}
		PopCheck ();

	}

	public void PopCheck(){
		if (GoodPopCount > 2) {
			People += 5;
		} else if (BadPopCount > 2) {
			People -= 3;
			Food = 0;
		}
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
				val += map [h, k] == Status.Stable ? 1 : 0;

			}
		}

		// Subtract 1 if (x,y) is alive since we counted it as a neighbor.
		return val - (map[x, y]== Status.Stable ? 1 : 0);
	}
}

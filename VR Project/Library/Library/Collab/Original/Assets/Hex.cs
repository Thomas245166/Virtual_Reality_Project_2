using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hex : MonoBehaviour, IPointerClickHandler {

    Map map;

    public int x;
    public int y;
	public bool p;
    public TileType type = 0;
    public Status status = 0;

    public PopUp popUp;
    public GameObject popUpPrefab;

    private bool showPopUp = false;

    void Start()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        switch(type)
        {
            default:
                Debug.Log("Type value not found for clicked Hex at (" + x + "," + y + ")");
                break;

			case TileType.Water:
				Debug.Log ("Water Hex at (" + x + "," + y + "), " + status.ToString ());
				//popUp = GameObject.Find("Pop_" + x + "_" + y).GetComponent<PopUp>();
				var pops = Map.pops [x, y].GetComponent<PopUp> ();
				PopUp.px = x;
				PopUp.py = y;
				pops.makePopUp();
                //map.CleanWater(x, y);
                break;

            case TileType.Land:
                Debug.Log("Land Hex at (" + x + "," + y + "), " + status.ToString());
                map.CleanLand(x, y);
                break;

			case TileType.Sheep:
				Debug.Log ("Sheep Hex at (" + x + "," + y + "), " + status.ToString ());
				if (Map.map [x, y] == Status.Stable) {
				map.UpgradeSheep(x,y);		
				}
				map.CleanFood(x, y);
                break;

			case TileType.Forest:
				Debug.Log ("Forest Hex at (" + x + "," + y + "), " + status.ToString ());
				if (Map.map[x,y] == Status.Stable) { 
					map.UpgradeWood (x, y);
				}
				map.CleanWood (x, y);
                break;

            case TileType.Mountain:
                Debug.Log("Mountain Hex at (" + x + "," + y + "), " + status.ToString());
				if (Map.map[x,y] == Status.Stable) { 
					map.UpgradeMountain (x, y);
				}
                map.CleanMineral(x, y);
                break;

            case TileType.City:
                Debug.Log("City Hex at (" + x + "," + y + "), " + status.ToString());
                map.CleanCity(x, y);
                break;
			case TileType.Logging:
				Debug.Log("Logging Hex at (" + x + "," + y + "), " + status.ToString());
				break;
        }
    }

    public override string ToString()
    {
        return "x:" + this.x + ", y:" + this.y + ", type:" + this.type.ToString() + ", status:" + this.status;
    }
		
    //public Hex[] GetNeighbours()
    //{
    //    GameObject leftneighbor=GameObject.Find("Hex_" + (x - 1) + "_" + y);

    //    GameObject rightneighbor = GameObject.Find("Hex_" + (x + 1) + "_" + y);
    //    return null;
    //}
}

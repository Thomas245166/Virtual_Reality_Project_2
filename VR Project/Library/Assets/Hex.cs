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
	private static int clickx = 0;
	private static int clicky = 0;
	public GameObject pop;
	public PopUp popUP;
	public static TileType clicktile;
	public MeshRenderer ColorHex;

    void Start()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
		popUp = GameObject.Find ("Popup").GetComponent<PopUp>();
		var group = popUp.GetComponent<CanvasGroup> ();
		group.alpha = 1f;
		group.interactable = true;
		group.blocksRaycasts = true;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
		
        switch(type)
        {
            default:
                Debug.Log("Type value not found for clicked Hex at (" + x + "," + y + ")");
                break;

		case TileType.Water:
				clicktile = type;
				clickx = x;
				clicky = y;
				Debug.Log (clicky);
				Debug.Log ("Water Hex at (" + x + "," + y + "), " + status.ToString ());
				pop = GameObject.Find ("Popup");
				pop.transform.position = new Vector3 (x, 3, y);
                //if (clickx < 4)
                //{
                //    pop.transform.Rotate(0, -45, 0);
                //}
                //else if (clickx > 7)
                //{
                //    pop.transform.Rotate(0, 45, 0);
                //}
                break;

            case TileType.Land:
			clicktile = type;
                Debug.Log("Land Hex at (" + x + "," + y + "), " + status.ToString());
				clickx = x;
				clicky = y;
				Debug.Log (clicky);
				Debug.Log ("Water Hex at (" + x + "," + y + "), " + status.ToString ());
				pop = GameObject.Find ("Popup");
				pop.transform.position = new Vector3 (x, 3, y);
                break;

			case TileType.Sheep:
			clicktile = type;
				clickx = x;
				clicky = y;
				Debug.Log (clicky);
				Debug.Log ("Water Hex at (" + x + "," + y + "), " + status.ToString ());
				pop = GameObject.Find ("Popup");
				pop.transform.position = new Vector3 (x, 3, y);
                break;

			case TileType.Forest:
			clicktile = type;
				//Debug.Log ("Forest Hex at (" + x + "," + y + "), " + status.ToString ());
				//if (Map.map[x,y] == Status.Stable) { 
				//	map.UpgradeWood (x, y);
				//}
				//map.CleanWood (x, y);
				clickx = x;
				clicky = y;
				Debug.Log (clicky);
				Debug.Log ("Water Hex at (" + x + "," + y + "), " + status.ToString ());
				pop = GameObject.Find ("Popup");
				pop.transform.position = new Vector3 (x, 3, y);
				break;

            case TileType.Mountain:
			clicktile = type;
				clickx = x;
				clicky = y;
				Debug.Log (clicky);
				Debug.Log ("Water Hex at (" + x + "," + y + "), " + status.ToString ());
				pop = GameObject.Find ("Popup");
				pop.transform.position = new Vector3 (x, 3, y);
				break;

            case TileType.City:
			clicktile = type;
				clickx = x;
				clicky = y;
				Debug.Log (clicky);
				Debug.Log ("Water Hex at (" + x + "," + y + "), " + status.ToString ());
				pop = GameObject.Find ("Popup");
				pop.transform.position = new Vector3 (x, 3, y);
				break;
        }
    }

    //void OnGUI()
    //{
    //    var position = Camera.main.WorldToScreenPoint(new Vector3(x,0,y));
    //}

    public override string ToString()
    {
        return "x:" + this.x + ", y:" + this.y + ", type:" + this.type.ToString() + ", status:" + this.status;
    }

	public void Clean(){
		if (map.Energy >= 10) {
			switch (clicktile) {
			default:
				Debug.Log ("NO CLEAN METHOD");
				break;

			case TileType.Water:
				Debug.Log ("Water Hex at (" + clickx + "," + clicky + "), " + status.ToString ());
				map.CleanWater (clickx, clicky);
				break;

			case TileType.Land:
				Debug.Log ("Land Hex at (" + x + "," + y + "), " + status.ToString ());
				map.CleanLand (clickx, clicky);
				break;

			case TileType.Sheep:
				Debug.Log ("Sheep Hex at (" + x + "," + y + "), " + status.ToString ());
				map.CleanFood (clickx, clicky);
				break;

			case TileType.Forest:
				Debug.Log ("Forest Hex at (" + x + "," + y + "), " + status.ToString ());
				map.CleanWood (clickx, clicky);
				break;

			case TileType.Mountain:
				Debug.Log ("Mountain Hex at (" + x + "," + y + "), " + status.ToString ());
				map.CleanMineral (clickx, clicky);
				break;

			case TileType.City:
				Debug.Log ("City Hex at (" + x + "," + y + "), " + status.ToString ());
				map.CleanCity (clickx, clicky);
				break;
			}
		}
	}

	public void Upgrade(){
		switch(clicktile)
		{
		default:
			Debug.Log ("NO UPGRADE METHOD");
			break;

		case TileType.Water:
			Debug.Log ("!!!!!AAAAAAAAAAAAAAAAAAAAAAAWater Hex at (" + clickx + "," + clicky + "), " + status.ToString());
			map.UpgradeWater(clickx,clicky);
			break;

		case TileType.Land:
			Debug.Log ("AAAAAAAAAAAAAAAAAAAAAAAAAALand Hex at (" + x + "," + y + "), " + status.ToString ());
			map.UpgradeLand (clickx, clicky);
			clicktile = TileType.Turbine;
			break;

		case TileType.Sheep:
			Debug.Log ("AAAAAAAAAAAAAAAAAAAAAAAAAAAaSheep Hex at (" + x + "," + y + "), " + status.ToString ());
			map.UpgradeSheep(clickx, clicky);
			break;

		case TileType.Forest:
			Debug.Log ("AAAAAAAAAAAAAAAAAAAAAAAForest Hex at (" + x + "," + y + "), " + status.ToString ());
			map.UpgradeWood (clickx, clicky);
			break;

		case TileType.Mountain:
			Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMountain Hex at (" + x + "," + y + "), " + status.ToString());
			map.UpgradeMountain(clickx, clicky);
			break;

		case TileType.Turbine:
			Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAATrubine Hex at (" + x + "," + y + "), " + status.ToString());
			map.UpgradeTurbine (clickx, clicky);
			break;

		}
	}

	public void destroy(){
		switch(clicktile)
		{
		default:
			Debug.Log ("NO UPGRADE METHOD");
			break;

		case TileType.Sheep:
			Debug.Log ("Sheep Hex at (" + x + "," + y + "), " + status.ToString ());
			GameObject sheep = GameObject.Find ("Sheep_" + clickx + "_" + clicky);
			Destroy (sheep);
			Map.mapType [clickx, clicky] = TileType.Land;
			break;

		case TileType.Forest:
			Debug.Log ("Forest Hex at (" + x + "," + y + "), " + status.ToString ());
			GameObject forest = GameObject.Find ("Forest_" + clickx + "_" + clicky);
			Destroy (forest);
			Map.mapType [clickx, clicky] = TileType.Land;
			break;

		case TileType.Mountain:
			Debug.Log("Mountain Hex at (" + x + "," + y + "), " + status.ToString());
			GameObject mountain = GameObject.Find ("Mountain_" + clickx + "_" + clicky);
			Destroy (mountain);
			Map.mapType [clickx, clicky] = TileType.Land;
			break;
		}
	}

	public void Gather(){
		if (map.Energy >= 10) {
			map.SingleGather (clickx, clicky);
		}
	}

    //public Hex[] GetNeighbours()
    //{
    //    GameObject leftneighbor=GameObject.Find("Hex_" + (x - 1) + "_" + y);

    //    GameObject rightneighbor = GameObject.Find("Hex_" + (x + 1) + "_" + y);
    //    return null;
    //}
}

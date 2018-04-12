using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour {
	public static Map map;
	public TextMesh text;
	public GameObject popup;
	public bool isShowing;

    void Start() {
		text = GameObject.Find ("WinText").GetComponent<TextMesh> ();
		text.text = "";
		map = GameObject.Find ("Map").GetComponent<Map> ();
	}

	void Update() {
		DisplayText ();
	}


	public void UpdateonClick() {
		map.UpdateButtonPush ();
	}

	public void GatherWoodonClick() {
		map.gatherResourceWood ();
	}
	public void GatherMineralonClick() {
		map.gatherResourceMineral ();
	}
	public void GatherFoodonClick() {
		map.gatherResourceFood ();
	}

	public void PlantaTree() {
		map.MakeTree ();
	}
	public void MakeaMine() {
		map.MakeMine ();
	}
	public void GrowaFarm() {
		map.MakeFarm ();
	}

	public void BuildaCity() {
		map.BuildCity ();
	}
	public void DisplayText() {
		if (map.Cities >= 4){
			text.text = "You Win!!!";
		}
		if (map.People <= 0) {
			text.text = "You Lose";
		}
	}

    public void CleanForest() {
        map.CleanWood();
    }

    public void CleanFood() {
        map.CleanFood();
    }

    public void CleanWater() {
        map.CleanWater(0, 0);
    }

    public void CleanMineral() {
        map.CleanMineral();
    }
}

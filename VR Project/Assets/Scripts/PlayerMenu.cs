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

	public void PopUp() {
		isShowing = !isShowing;
		popup.SetActive(isShowing);
	}
	public void UpdateonClick() {
		map.UpdateButtonPush ();
	}

	public void GatherWoodonClick() {
		if (map.Energy >= 10) {
			map.gatherResourceWood ();
		}
	}
	public void GatherMineralonClick() {
		if (map.Energy >= 10) {
			map.gatherResourceMineral ();
		}
	}
	public void GatherFoodonClick() {
		if (map.Energy >= 10) {
			map.gatherResourceFood ();
		}
	}

	public void PlantaTree() {
		if (map.Energy >= 20) {

			map.MakeTree ();
		}
	}
	public void MakeaMine() {
		if (map.Energy >= 20) {
			map.MakeMine ();
		}
	}
	public void GrowaFarm() {
		if (map.Energy >= 20) {
			map.MakeFarm ();
		}
	}

	public void BuildaCity() {
		if (map.Energy >= 50) {
			map.BuildCity ();
		}
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
		if (map.Energy >=5) {
			map.CleanWood ();
		}
    }

    public void CleanFood() {
		if (map.Energy >=5) {
			map.CleanFood ();
		}
    }

    public void CleanWater() {
		if (map.Energy >=5) {
			map.CleanWater ();
		}
	}

    public void CleanMineral() {
		if (map.Energy >=5) {
			map.CleanMineral ();
		}
    }
}

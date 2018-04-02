using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    Unit selectedUnit;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        //Debug.Log("Mouse Position: " + Input.mousePosition);
        //Ortho only
        //Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("World Point: " + worldPoint);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;


        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject ourHitObject = hitInfo.collider.transform.parent.gameObject;

            //Debug.Log("Clicked On: " + ourHitObject.name);

            // what selected
            if (ourHitObject.GetComponent<Hex>() != null)
            {
                // Hex
                MouseOver_Hex(ourHitObject);
            }
            else if (ourHitObject.GetComponent<Unit>() != null)
            {
                // Unit
                MouseOver_Unit(ourHitObject);

            }


        }

     


    }

    void MouseOver_Hex(GameObject ourHitObject)
    {
        Debug.Log("Raycast hit: " + ourHitObject.name);


        if (Input.GetMouseButtonDown(0))
        {

           //when clicked change the color
            MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();

            if (mr.material.color == Color.red)
            {
                mr.material.color = Color.white;
            }
            else
            {
                mr.material.color = Color.red;
            }

            //when the unit is selected move it to the tile that is clicked

            if (selectedUnit != null)
            {
                selectedUnit.destination = ourHitObject.transform.position;
            }


        }

    }

    void MouseOver_Unit(GameObject ourHitObject)
    {
        Debug.Log("Raycast hit: " + ourHitObject.name);

        if (Input.GetMouseButtonDown(0))
        {
            // We have click on the unit
            selectedUnit = ourHitObject.GetComponent<Unit>();
        }

    }
}
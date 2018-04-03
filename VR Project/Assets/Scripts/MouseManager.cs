using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    Unit selectedUnit;
    public float sec = 3f;

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
    IEnumerator Stall(GameObject ourHitObject)
    {
        MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();
        mr.material.color = Color.red;
        yield return new WaitForSeconds(sec);
        mr.material.color = Color.green;

    }

    void MouseOver_Hex(GameObject ourHitObject)
    {
        Debug.Log("Raycast hit: " + ourHitObject.name);
        MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();


        if (Input.GetMouseButtonDown(0))
        {

           //when clicked change the color
            

            if (mr.material.color == Color.red)
            {
                mr.material.color = Color.green;
                
            }
            else if (mr.material.color == Color.blue)
            {
               
            }
            else
            {
                StartCoroutine(Stall(ourHitObject));
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
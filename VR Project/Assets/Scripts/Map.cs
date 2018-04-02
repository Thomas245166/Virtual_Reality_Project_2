﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject hexPreFab;

    int width = 10;
    int height = 10;

    float XOffset = 0.882f;
    float ZOffset = 0.764f;

    // Use this for initialization
    void Start () {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x*XOffset;
                //Is the Row odd
                if (y % 2==1)
                {
                    xPos +=XOffset/2f;
                }
                 GameObject hex_go= (GameObject)Instantiate(hexPreFab, new Vector3(xPos,0,y*ZOffset), Quaternion.identity);

                //name
                hex_go.name = "Hex_" + x + "_" + y;
                //its position
                hex_go.GetComponent<Hex>().x = x;
                hex_go.GetComponent<Hex>().y = y;

                hex_go.transform.SetParent(this.transform);
                hex_go.isStatic = true;
              
                
            }
        }
            

            
        
        
     
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

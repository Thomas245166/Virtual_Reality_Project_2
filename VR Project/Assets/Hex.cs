using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour {

    public int x;
    public int y;

    public Hex[] GetNeighbours()
    {
        GameObject leftneighbor=GameObject.Find("Hex_" + (x - 1) + "_" + y);

        GameObject rightneighbor = GameObject.Find("Hex_" + (x + 1) + "_" + y);
        return null;
    }
}

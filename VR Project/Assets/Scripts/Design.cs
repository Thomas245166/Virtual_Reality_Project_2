using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Design : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = "new text set";
        t.color = Color.black;
        t.fontSize =300;
        t.transform.localEulerAngles += new Vector3(0, 0, 0);
        t.transform.localPosition += new Vector3(19.11f, 4.72f, 7.03f);
        t.transform.localScale -= new Vector3(0.001F, 0.001F, 0.001F);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

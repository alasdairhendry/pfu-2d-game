using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_Inputs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
            transform.Translate(0f, 0f, Input.GetAxis("Horizontal") * Time.deltaTime);
    }
}

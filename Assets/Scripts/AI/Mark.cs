﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour {
    public GameObject InCamera;
    public GameObject BackCamera;
    //public Canvas canvas;

	// Use this for initialization
	void Start () {
        
        //BackCamera = GameObject.FindGameObjectWithTag("PlayerBackCamera");
        //InCamera = GameObject.FindGameObjectWithTag("PlayerInCamera");
    }
	
	// Update is called once per frame
	void Update () {
        if (InCamera.activeSelf)
        {
            transform.rotation = InCamera.transform.rotation;
        }
        else
        {
            transform.rotation = BackCamera.transform.rotation;
        }
	}
}

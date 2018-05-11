using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour {

    public GameObject InCamera;
    public GameObject BackCamera;
	// Use this for initialization
	void Start () {
        InCamera = GameObject.FindGameObjectWithTag("PlayerInCamera");
        InCamera.SetActive(true);
        BackCamera = GameObject.FindGameObjectWithTag("PlayerBackCamera");
        BackCamera.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (InCamera.activeSelf)
                InCamera.SetActive(false);
            else
                InCamera.SetActive(true);
            if (BackCamera.activeSelf)
                BackCamera.SetActive(false);
            else
                BackCamera.SetActive(true);
        }
	}
}

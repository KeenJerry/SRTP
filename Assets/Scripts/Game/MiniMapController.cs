using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour {

    public GameObject Player;
    public List<GameObject> EnemyFlights;
    public List<GameObject> FriendFilghts;
	// Use this for initialization
	void Start () {
        EnemyFlights = new List<GameObject>(GameObject.FindGameObjectsWithTag("AIFriend"));
        EnemyFlights = new List<GameObject>(GameObject.FindGameObjectsWithTag("AIEnemy"));
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.GetComponent<PlayerController>().Target)
        {
            
        }
	}
}

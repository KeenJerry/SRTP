using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    private FlightSystem plane;
    public float Pitch;
    public float Roll;
    public float Yaw;
    public float Throttle;
    public GameObject Target;

	void Start () {
        Pitch = 0.0f;
        Roll = 0.0f;
        Yaw = 0.0f;
        Throttle = 0.0f;
        plane = GetComponent<FlightSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!Target)
        {
            FindTarget();
        }
        Pitch = Input.GetAxis("Pitch");
        Roll = Input.GetAxis("Roll");
        Throttle = Input.GetAxis("Throttle"); 
        Yaw = Input.GetAxis("Yaw");
        plane.Move(Pitch:Pitch, Roll:Roll, Yaw:Yaw, Throttle:Throttle);

        if (Input.GetKey(KeyCode.LeftControl)) plane.ShootMiniGun();
        if (Input.GetKeyDown(KeyCode.Space)) plane.LaunchMissile();
        
	}

    void FindTarget()
    {
        List<GameObject> targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("AIEnemy"));
        Target = targets[(int)Random.Range(0, targets.Count)];
        //GameObject.Find
    }
}

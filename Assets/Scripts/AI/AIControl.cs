using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour {

    // Use this for initialization
    private FlightSystem plane;
    public float Pitch;
    public float Roll;
    public float Yaw;
    public float Throttle;

    void Start()
    {
        Pitch = 0.0f;
        Roll = 0.0f;
        Yaw = 0.0f;
        Throttle = 0.0f;
        plane = GetComponent<FlightSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        plane.Move(Pitch: Pitch, Roll: Roll, Yaw: Yaw, Throttle: Throttle);
    }
}

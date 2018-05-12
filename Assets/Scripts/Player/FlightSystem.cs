using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightSystem : MonoBehaviour {

    
    // Aircraft
    public float AccerlerationSpeed = 5.0f;
    //public float SpeedNormal = 50.0f; // gravity no longer take effect
    public float SpeedMax = 60.0f;
    public float SpeedTakeOff = 40.0f;
    public float SpeedPitch = 10.0f;
    public float SpeedRoll = 3.0f;
    public float SpeedYaw = 2.0f;
    public int HP = 100;

    public float ForwardSpeed = 40.0f;
    // public Vector3 LiftForce = Vector3.zero;
    // Weapon system
    public WeaponController WeaponControl;

    public float roll = 0.0f;
    public float pitch = 0.0f;
    public float yaw = 0.0f;

    public Rigidbody rigid;
    public MiniGunLauncher miniGunLauncher;
    public MissileLauncher missileLauncher;
    public GameObject OnHitEffect;
    public GameObject OnHitSpark;
    

	// Use this for initialization
	void Start () {
        // define all components
        WeaponControl = GetComponent<WeaponController>();
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = new Vector3(-ForwardSpeed, 0.0f, 0.0f);
        miniGunLauncher = GetComponentInChildren<MiniGunLauncher>();
        missileLauncher = GetComponentInChildren<MissileLauncher>();
    }

    public void Move(float Pitch, float Roll, float Yaw, float Throttle)
    {
        pitch = Pitch;
        roll = Roll;
        yaw = Yaw;
        calculateTorque();
        calculateVelocity(Throttle);
        calculateLift();
    }
    private void calculateTorque()
    {
        Vector3 torque = Vector3.zero;
        torque += pitch * SpeedPitch * transform.forward;
        torque += roll * SpeedRoll * transform.right;
        torque += yaw * SpeedYaw * transform.up;
        rigid.AddTorque(torque);
    }
    private void calculateVelocity(float Throttle)
    {
        float accerlerate = Throttle * AccerlerationSpeed;
        ForwardSpeed = Mathf.Lerp(ForwardSpeed, ForwardSpeed + accerlerate, Time.deltaTime);
        if(ForwardSpeed >= SpeedMax)
        {
            ForwardSpeed = SpeedMax;
        }
        Vector3 newvelocity = Vector3.Lerp(rigid.velocity, ForwardSpeed * -transform.right, Time.deltaTime * 3);
        rigid.velocity = newvelocity;
    }
    private void calculateLift()
    {
        
        if(ForwardSpeed >= 40.0f)
        {
            rigid.AddForce(rigid.mass * 9.81f * transform.up);
           
        }
        else
        {
            //rigid.AddForce(rigid.mass * 9.81f * transform.up)
        }
    }

    public void ShootMiniGun()
    {
        miniGunLauncher.Shoot();
    }

    public void LaunchMissile()
    {
        missileLauncher.Shoot();
    }
}

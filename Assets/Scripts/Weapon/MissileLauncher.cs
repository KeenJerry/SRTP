using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour {

    public GameObject MissileL;
    public GameObject MissileR;
    public float FireRate;
    public float CoolDown;
    public float NextFireL;
    public float NextFireR;
    // Use this for initialization
    void Start () {
        FireRate = 3.0f;
        NextFireL = 0.0f;
        NextFireR = 0.0f;
        CoolDown = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}

    public void Shoot()
    {
       
        //if (Time.time > Mathf.Max(NextFireL, NextFireR + CoolDown))
        //{
            if ((Time.time > NextFireL && Time.time > NextFireR) || (Time.time > NextFireL && Time.time <= NextFireR))
            {
                NextFireL = Time.time + FireRate;

                GameObject missileL = GameObject.Instantiate(MissileL, transform.position + -transform.forward * 2.5f + -transform.up * 1, Quaternion.identity, transform.parent);
                if (GetComponentInParent<AISystem>())
                {
                    missileL.GetComponent<BulletMissile>().SetTarget(GetComponentInParent<AISystem>().Target);
                }
                else
                {
                    missileL.GetComponent<BulletMissile>().SetTarget(GetComponentInParent<PlayerController>().Target);
                }
            }
            else if (Time.time > NextFireR && Time.time <= NextFireL)
            {
                NextFireR = Time.time + FireRate;

                GameObject missileR = GameObject.Instantiate(MissileL, transform.position + transform.forward * 2.5f + -transform.up * 1, Quaternion.identity, transform.parent);
                if (GetComponentInParent<AISystem>())
                {
                    missileR.GetComponent<BulletMissile>().SetTarget(GetComponentInParent<AISystem>().Target);
                }
                else
                {
                    missileR.GetComponent<BulletMissile>().SetTarget(GetComponentInParent<PlayerController>().Target);
                }
            }
        //}
        
    }
}

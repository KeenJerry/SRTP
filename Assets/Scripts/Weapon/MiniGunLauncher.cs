using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGunLauncher : MonoBehaviour {

    // Use this for initialization
    //private List<GameObject> BulletNormalPool;
    public GameObject Bullet;
    public float FireRate;
    public float NextFire;
    //public BulletNormal Bullet;
	void Start () {
        //Bullet = GetComponent<BulletNormal>();
        FireRate = 0.1f;
        NextFire = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Shoot()
    {
        if(Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;
            GameObject bullet = GameObject.Instantiate(Bullet, transform.position, Quaternion.identity, transform.parent);
        }
        
    }
    
}

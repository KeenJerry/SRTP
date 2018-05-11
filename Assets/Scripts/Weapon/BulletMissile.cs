using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMissile : MonoBehaviour {

    public float LifeTime;
    public GameObject Owner;
    public GameObject Target;
    public Rigidbody Rigid;
    public float TimeStart;
    // Use this for initialization
    void Start () {
        Owner = transform.parent.gameObject;
        Rigid = GetComponent<Rigidbody>();
        Rigid.velocity = Owner.GetComponent<Rigidbody>().velocity * 5;
        TimeStart = Time.time;
        Debug.Log(TimeStart);
        LifeTime = 20.0f;
    }
	
	// Update is called once per frame
	void Update () {
        float TimeNow = Time.time;
        if (TimeNow - TimeStart >= LifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}

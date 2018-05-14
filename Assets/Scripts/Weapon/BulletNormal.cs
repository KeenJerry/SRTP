using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNormal : MonoBehaviour {

    public float LifeTime;
    public GameObject Owner;
    public GameObject Target;
    public Rigidbody Rigid;
    public float TimeStart;
    public int Damage;
	// Use this for initialization
	void Start () {
        Owner = transform.parent.gameObject;
        Rigid = GetComponent<Rigidbody>();
        Rigid.velocity = Owner.GetComponent<Rigidbody>().velocity * 5;
        TimeStart = Time.time;
        Debug.Log(TimeStart);
        LifeTime = 4.0f;
        Damage = 3;
	}
	
	// Update is called once per frame
	void Update () {
        float TimeNow = Time.time;
        if (TimeNow - TimeStart >= LifeTime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (Target)
            {
                //Ray ray = new Ray(transform.position, transform.forward);
                //RaycastHit hitInfo;
                if (Vector3.Distance(Target.transform.position, this.transform.position) < 2)
                {
                    //isCatch = true;
                    DoDamage(Target);
                    Destroy(this.gameObject);
                    Spark();
                    return;
                }
            }
            
        }
    }
    public void SetTarget(GameObject target)
    {
        Target = target;
    }
    public void Spark()
    {
        GameObject.Instantiate(Target.GetComponent<FlightSystem>().OnHitSpark, this.transform.position, Quaternion.identity, this.transform.parent);
    }

    public void DoDamage(GameObject target)
    {
        target.GetComponent<FlightSystem>().HP -= Damage;
    }
}

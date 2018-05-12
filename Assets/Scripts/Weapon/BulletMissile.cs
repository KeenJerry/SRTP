using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMissile : MonoBehaviour {

    public float LifeTime;
    public GameObject Owner;
    public GameObject Target;
    //public GameObject OnHitEffect;
    public Rigidbody Rigid;
    public int Damage;
    public float TimeStart;

    //public AISystem aisystem;
    // Use this for initialization
    void Start () {
        Damage = 50;
        Owner = transform.parent.gameObject;
        Rigid = GetComponent<Rigidbody>();
        //Rigid.rotation = GetComponentInParent<Rigidbody>().rotation;
        Rigid.velocity = Owner.GetComponent<Rigidbody>().velocity * 5;
        TimeStart = Time.time;
        //Debug.Log(TimeStart);
        LifeTime = 20.0f;
    }
	
	// Update is called once per frame
	void Update () {
        float TimeNow = Time.time;
        if (TimeNow - TimeStart >= LifeTime - 1)
        {
            Destroy(this.gameObject);
            Target = null;
        }
        else
        {
            if (Target)
            {
                //Ray ray = new Ray(transform.position, transform.forward);
                //RaycastHit hitInfo;
                if (Vector3.Distance(Target.transform.position, this.transform.position) < 5)
                {
                    //isCatch = true;
                    DoDamage(Target);
                    Destroy(this.gameObject);
                    Explode();
                    return;
                }
            }
            if (Target)
            {
                Debug.Log(Vector3.Dot(Rigid.velocity.normalized, (Target.transform.position - this.transform.position).normalized));
                if (Vector3.Dot(Rigid.velocity.normalized, (Target.transform.position - this.transform.position).normalized) < 0.0f)
                {
                    Target = null;
                }
            }
        }
        
    }
    public void SetTarget(GameObject target)
    {
        Target = target;
    }
    public void DoDamage(GameObject target)
    {
        target.GetComponent<FlightSystem>().HP -= Damage;
    }
    public void Explode()
    {
        GameObject.Instantiate(Target.GetComponent<FlightSystem>().OnHitEffect, this.transform.position, Quaternion.identity, this.transform.parent);
    }
}

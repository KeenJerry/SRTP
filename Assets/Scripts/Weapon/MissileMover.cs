using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMover : MonoBehaviour {

    public BulletMissile missile;
    public Vector3 TargetPosition;
    public Quaternion mainRot;
    public Rigidbody rigid;
	// Use this for initialization
	void Start () {
        missile = GetComponent<BulletMissile>();
        TargetPosition = Vector3.zero;
        mainRot = Quaternion.identity;
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (missile.Target)
        {
            TraceTarget(missile.Target);
            CalculateVelocity();
        }
        
	}
    void CalculateVelocity()
    {
        rigid.velocity = Vector3.Slerp(rigid.velocity, rigid.velocity.magnitude * this.transform.forward, Time.deltaTime * 3);
    }
    void TraceTarget(GameObject target)
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float directToGo = Vector3.Dot(this.transform.forward.normalized, targetDir.normalized);
        float rotateMult = 1.0f;

        //if (directToGo > 0.3f)
        //{
            rotateMult = 1.0f;
            TargetPosition = Vector3.Lerp(TargetPosition, target.transform.position, Time.deltaTime * 1);
        //}
        //else
        //{
            //Vector3 reflectionVector = Vector3.Reflect(targetDir.normalized, this.transform.forward);
            //reflectionVector.x = 0.3f;
            //reflectionVector.y = 0.5f;
            //TargetPosition = Vector3.Slerp(TargetPosition, this.transform.position + reflectionVector, Time.deltaTime);
            //rotateMult = 0.5f;
            //if (directToGo < -0.9f)
            //{
            //    TargetPosition.y += 0.1f;
            //}
        //}
        //this.transform.LookAt(target.transform);

        mainRot = Quaternion.LookRotation(target.transform.position - this.transform.position);
        //Vector3 relativePoint = this.transform.InverseTransformPoint(TargetPosition).normalized;
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, mainRot, rotateMult * 0.1f * 20.0f * Time.deltaTime);
        //rigid.rotation *= Quaternion.Euler(0, 0, target.transform.rotation.z * 5000 * 0.01f * Time.deltaTime);
        this.transform.rotation = mainRot;
    }
}

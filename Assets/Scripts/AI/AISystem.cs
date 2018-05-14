using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelfState
{
    Idle,
    Patrol,
    Attacking,
    TurnPostion,
}
public enum TargetState
{
    Flying,
    Static,
    Moving,
    IsLocked,
}
public enum Weapon
{
    MiniGun,
    Missile,
}
public class AISystem : MonoBehaviour {

    //public List<GameObject> Targets;

    public GameObject Target;
    public Vector3 TargetTempPosition;
    public float VersionSight;
    public float DistanceLock;
    public float LimitedDistance;
    //public float TimeToSelectTarget;
    public float AttackRate;
    public int WeaponSelected;
    public GameObject BattleCenter;
    public Vector3 TargetPosition;

    public SelfState selfState;
    public TargetState targetState;
    public FlightSystem flight;
    public Rigidbody rigid;

    public Quaternion mainRot;
    // Use this for initialization
    void Start () {
        mainRot = Quaternion.identity;
        DistanceLock = 10000000;
        LimitedDistance = 1000.0f;
        BattleCenter = GameObject.FindGameObjectWithTag("BattleCenter");
        VersionSight = 0.2f;
        //TimeToSelectTarget = 2.0f;
        AttackRate = 3.0f;
        TargetPosition = Vector3.zero;

        selfState = SelfState.Patrol;
        flight = GetComponent<FlightSystem>();
        rigid = GetComponent<Rigidbody>();

	}
	
    public void CalculateTargetBehavior()
    {
        if (Target)
        {
            // the target exists
            if((TargetTempPosition - Target.transform.position) == Vector3.zero)
            {
                targetState = TargetState.Static;
            }
            else
            {
                targetState = TargetState.Moving;
                if(Mathf.Abs(TargetTempPosition.y - Target.transform.position.y) >= 0.5f)
                {
                    targetState = TargetState.Flying;
                }
                
            }
            //Debug.Log(Vector3.Dot((Target.transform.position - this.transform.position).normalized, -this.transform.right.normalized));
            if (Vector3.Dot((Target.transform.position-this.transform.position).normalized, -this.transform.right.normalized) >= VersionSight)
            {
                targetState = TargetState.IsLocked;
            }
        }
        

    }

	// Update is called once per frame
	void Update () {
        CalculateTargetBehavior();

        switch (selfState)
        {
            case SelfState.Patrol: // Find Target automatically
                FindTarget();
                if (Vector3.Distance(transform.position, BattleCenter.transform.position) > LimitedDistance)
                {
                    selfState = SelfState.TurnPostion;
                }
                else
                {
                    if (Target)
                    {
                        selfState = SelfState.Attacking;
                    }
                }
                break;
            case SelfState.Attacking:
                if(Vector3.Distance(transform.position, BattleCenter.transform.position) > LimitedDistance)
                {
                    selfState = SelfState.TurnPostion;
                }
                else
                {
                    if (Target)
                    {
                        TraceTarget(Target);
                        if(targetState == TargetState.IsLocked)
                        {
                            if(Random.Range(0f, 250f) <= AttackRate)
                            {
                                WeaponSelected = (int)Random.Range(1.0f, 2.999f);
                                Attack(WeaponSelected);
                            }
                        } 
                    }
                    else
                    {
                        selfState = SelfState.Patrol;
                    }
                }
                
                break;
            case SelfState.Idle:
                break;
            case SelfState.TurnPostion:
                //Target = BattleCenter;
                if(Vector3.Distance(transform.position, BattleCenter.transform.position) <= LimitedDistance)
                {
                    selfState = SelfState.Patrol;
                }
                else
                {
                    TraceTarget(BattleCenter);
                }
                break;
        }
	}
    void TraceTarget(GameObject target)
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float directToGo = Vector3.Dot(this.transform.forward.normalized, targetDir.normalized);
        float rotateMult = 1.0f;

        if (directToGo > 0.3f)
        {
            rotateMult = 1.0f;
            TargetPosition = Vector3.Lerp(TargetPosition, target.transform.position, Time.deltaTime);
        }
        else
        {
            Vector3 reflectionVector = Vector3.Reflect(targetDir.normalized, this.transform.forward);
            reflectionVector.x = 0.3f;
            reflectionVector.y = 0.5f;
            TargetPosition = Vector3.Slerp(TargetPosition, this.transform.position + reflectionVector, Time.deltaTime);
            rotateMult = 0.5f;
            if(directToGo < -0.9f)
            {
                TargetPosition.y += 0.1f;
            }
        }
        //this.transform.LookAt(target.transform);

        mainRot = Quaternion.LookRotation(-Vector3.Cross((target.transform.position - this.transform.position).normalized, this.transform.up));
        Vector3 relativePoint = this.transform.InverseTransformPoint(TargetPosition).normalized;
        //Quaternion noise = new Quaternion(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
        mainRot.x += Random.Range(-0.3f, 0.3f);
        mainRot.y += Random.Range(-0.3f, 0.3f);
        mainRot.z += Random.Range(-0.3f, 0.3f);
        mainRot.w += Random.Range(-0.3f, 0.3f);
        rigid.rotation = Quaternion.Lerp(rigid.rotation, mainRot , rotateMult * 0.1f * 8.0f * Time.deltaTime);
        rigid.rotation *= Quaternion.Euler(0, 0, target.transform.rotation.z * 5000 * 0.01f * Time.deltaTime);
        //rigid.rotation = mainRot;
    }
    void Attack(int weaponselected)
    {
        if (Target)
        {
            if(weaponselected == 2)
            {
                if (targetState == TargetState.IsLocked)
                {
                    flight.LaunchMissile();
                }
            }
            else
            {
                flight.ShootMiniGun();
            }
        }
    }
    void FindTarget()
    {
        if(this.tag == "AIEnemy")
        {
            List<GameObject> targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("AIFriend"));
            targets.Add(GameObject.FindGameObjectWithTag("Player"));
            Target = targets[(int)Random.Range(0, targets.Count)];
        }
        else
        {
             
            List<GameObject> targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("AIEnemy"));
            Target = targets[(int)Random.Range(0, targets.Count)];
        }
    }
}

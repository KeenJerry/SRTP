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
    public float TimeToSelectTarget;
    public float AttackRate;
    public int WeaponSelected;
    public Vector3 BattleCenter;

    public SelfState selfState;
    public TargetState targetState;
    public FlightSystem flight;

    // Use this for initialization
    void Start () {
        DistanceLock = float.MaxValue;
        LimitedDistance = 1000.0f;
        BattleCenter = GameObject.FindGameObjectWithTag("BattleCenter").transform.position;
        VersionSight = 0.5f;
        TimeToSelectTarget = 2.0f;
        AttackRate = 0.6f;

        selfState = SelfState.Patrol;
        flight = GetComponent<FlightSystem>();

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
        }

    }

	// Update is called once per frame
	void Update () {
        CalculateTargetBehavior();

        switch (selfState)
        {
            case SelfState.Patrol: // Find Target automatically
                FindTarget();

                break;
            case SelfState.Attacking:
                break;
            case SelfState.Idle:
                break;
            case SelfState.TurnPostion:
                break;
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

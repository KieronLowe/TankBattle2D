using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAIMovement : MonoBehaviour
{
    public GameObject Crate;
    public GameObject Target;
    public Transform EnemyTank;
    public Transform RayCastOrigin;
    public float EnemySpeed = 5f;
    public float NextWaypointDistance = 3f;
    public float TargetKeepDistance = 2f;

    [SerializeField] private Transform fieldOfViewPrefab;
    private Seeker seeker;
    private Rigidbody2D enemyRigidBody;
    private Path path;
    private EnemyAIFieldOfView enemyAIFieldOfViewScript;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath;
    private float distance;
    private bool isNearPlayer = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        enemyRigidBody = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        enemyAIFieldOfViewScript = Instantiate(fieldOfViewPrefab, null).GetComponent<EnemyAIFieldOfView>();
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(enemyRigidBody.position, Target.transform.position, OnPathComplete);
            MoveEnemyTowardsCrate();
        }          
    }
    
    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        enemyAIFieldOfViewScript.SetAimDirection(-transform.right);
        enemyAIFieldOfViewScript.SetOrigin(RayCastOrigin.position);
    }

    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
            reachedEndOfPath = false;

        MoveEnemyTowardsPlayer();

        Crate = GameObject.FindGameObjectWithTag("Crate");
        distance = Vector2.Distance(enemyRigidBody.position, path.vectorPath[currentWaypoint]);

        if (distance < NextWaypointDistance)
            currentWaypoint++;
    }

    public void MoveEnemyTowardsPlayer()
    {
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - enemyRigidBody.position).normalized;
        Vector2 force = direction * EnemySpeed * Time.deltaTime;

        if (!isNearPlayer)
        {
            enemyRigidBody.AddForce(force);
            enemyRigidBody.transform.up = enemyRigidBody.velocity.normalized;
        }

        if (Vector2.Distance(transform.position, Target.transform.position) < EnemyAIFieldOfView.ViewDistance)
        {
            Vector2 dirToPlayer = (Target.transform.position - transform.position).normalized;
            if (Vector2.Angle(enemyRigidBody.transform.rotation.eulerAngles, dirToPlayer) < (EnemyAIFieldOfView.Fov / 2f))
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dirToPlayer, EnemyAIFieldOfView.ViewDistance);
                if (raycastHit2D.collider.gameObject == Target)
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, Target.transform.position - transform.position);
                    isNearPlayer = true;
                }
            }
        }
        else
        {
            isNearPlayer = false;
        }

    }

    public void MoveEnemyTowardsCrate()
    {
        if (Crate != null)
            if (Crate.tag == "Crate" && Vector2.Distance(enemyRigidBody.transform.position, Crate.transform.position) < EnemyAIFieldOfView.ViewDistance)
                seeker.StartPath(enemyRigidBody.position, Crate.transform.position, OnPathComplete);
    }  
}

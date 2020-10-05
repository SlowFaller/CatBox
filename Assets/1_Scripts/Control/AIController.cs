using UnityEngine;
using LD47.Pathing;
using LD47.Detection;

namespace LD47.Control
{
    public class AIController : MonoBehaviour
    {
        private const string TAG = "Player";
        [Header("Suspicion")]
        [SerializeField] [Range(0, 5f)] float suspicionMovementSpeed = 4f;
        [SerializeField] [Range(0, 10f)] float suspicionDwellTime = 5f;
        [Header("Patrolling")]
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] [Range(0, 5f)] float patrolMovementSpeed = 2.5f;
        [SerializeField] [Range(0, 9f)] float patrolDwellTime = 3f;
        [SerializeField] [Range(0, 2f)] float waypointTolerance = 1f;

        GameObject obj_player;
        Mover cmp_mover;

        int currentWaypointIndex = 0;
        int susWaypointObjID = 0;
        float timeSinceLastArrived = Mathf.Infinity;
        Vector3 guardPosition;
        [SerializeField] bool isSuspicious = false;

        void Awake()
        {
            obj_player = GameObject.FindWithTag(TAG);
            cmp_mover = GetComponent<Mover>();
        }

        void Start()
        {
            transform.position = patrolPath.GetWaypointPosition(0);
        }

        void Update()
        {
            if (isSuspicious)
            {

                PatrolBehavior(suspicionMovementSpeed, suspicionDwellTime);
            }
            else
            {
                PatrolBehavior(patrolMovementSpeed, patrolDwellTime);
            }

            UpdateTimers();
        }

        Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        void PatrolBehavior(float movementSpeed, float dwellTime)
        {
            cmp_mover.SetMoveSpeed(patrolMovementSpeed);
            Vector3 nextPosition = guardPosition;

            if (AtWaypoint())
            {
                if (isSuspicious && !CheckSuspiciousWaypoint())
                {
                    dwellTime = -1f;
                }
                timeSinceLastArrived = 0;
                CycleWaypoint();
            }

            nextPosition = GetWaypointPosition();

            if (!IsDwelling(dwellTime))
            {
                cmp_mover.StartMoveAction(nextPosition);
            }
            else
            {
                DwellBehavior();
            }
        }

        private bool CheckSuspiciousWaypoint()
        {
 
            return patrolPath.GetWaypointID(currentWaypointIndex) == susWaypointObjID;
        }

        private void DwellBehavior()
        {

        }

        bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetWaypointPosition());

            return (distanceToWaypoint <= waypointTolerance);
        }

        void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        Vector3 GetWaypointPosition()
        {
            return patrolPath.GetWaypointPosition(currentWaypointIndex);
        }

        bool IsDwelling(float dwellTime)
        {
            return timeSinceLastArrived < dwellTime;
        }

        void UpdateTimers()
        {
            timeSinceLastArrived += Time.deltaTime;
        }

        public void AlertGuardDog(int susWaypoint)
        {
            isSuspicious = true;
            print(isSuspicious);
            susWaypointObjID = susWaypoint;
            GetComponent<Detector>().SetGuardDogAlert(true);
        }
    }
}

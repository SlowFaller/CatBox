using UnityEngine;
using LD47.Pathing;
using LD47.Detection;

namespace LD47.Control
{
    public class AIController : MonoBehaviour
    {
        private const string TAG = "Player";
        [Header("Suspicion")]
        [SerializeField] [Range(0, 10f)] float suspicionMovementSpeed = 4f;
        [SerializeField] [Range(0, 10f)] float suspicionDwellTime = 5f;
        [Header("Patrolling")]
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] [Range(0, 5f)] float patrolMovementSpeed = 2.5f;
        [SerializeField] [Range(0, 9f)] float patrolDwellTime = 3f;
        [SerializeField] [Range(0, 2f)] float waypointTolerance = 1f;

        GameObject obj_player;
        Mover cmp_mover;
        Scanning cmp_scanner;

        int currentWaypointIndex = 0;
        int susWaypointObjID = 0;
        float timeSinceLastArrived = Mathf.Infinity;
        float searchTime = 0;
        Vector3 guardPosition;
        [SerializeField] bool isSuspicious = false;
        [SerializeField] bool atSusWaypoint = false;

        void Awake()
        {
            obj_player = GameObject.FindWithTag(TAG);
            cmp_mover = GetComponent<Mover>();
            cmp_scanner = GetComponent<Scanning>();
        }

        void Start()
        {
            transform.position = patrolPath.GetWaypointPosition(0);
        }

        void Update()
        {
            if (isSuspicious && searchTime > suspicionDwellTime)
            {
                isSuspicious = false;
                GetComponent<Detector>().SetGuardDogAlert(false);
                searchTime = 0;
                atSusWaypoint = false;
                cmp_scanner.Cancel();                
            }

            if (isSuspicious)
            {
                cmp_mover.SetMoveSpeed(suspicionMovementSpeed);
                Vector3 nextPosition = guardPosition;
                int tempWaypoint = currentWaypointIndex;

                if (AtWaypoint())
                {
                    if (CheckSuspiciousWaypoint())
                    {
                        atSusWaypoint = true;
                    }
                    timeSinceLastArrived = 0;
                    CycleWaypoint();
                    
                }

                if (IsDwelling(suspicionDwellTime) && atSusWaypoint)
                {
                    searchTime += Time.deltaTime;
                    if (timeSinceLastArrived < Mathf.Epsilon)
                    {
                        DwellBehavior(suspicionDwellTime);
                    }
                }
                else
                {
                    cmp_mover.StartMoveAction(GetWaypointPosition());
                    
                }

            }
            else
            {
                cmp_mover.SetMoveSpeed(patrolMovementSpeed);
                Vector3 nextPosition = guardPosition;

                if (AtWaypoint())
                {
                    timeSinceLastArrived = 0;
                    CycleWaypoint();
                }

                if (!IsDwelling(patrolDwellTime))
                {  
                    cmp_mover.StartMoveAction(GetWaypointPosition());
                }
                else
                {
                    if (timeSinceLastArrived < Mathf.Epsilon)
                    {
                        DwellBehavior(patrolDwellTime);
                    }
                }
            }

            UpdateTimers();
        }

        Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        void PatrolBehavior(float movementSpeed)
        {
            cmp_mover.SetMoveSpeed(patrolMovementSpeed);
            Vector3 nextPosition = guardPosition;

            if (AtWaypoint())
            {
                timeSinceLastArrived = 0;
            }           
        }

        private bool CheckSuspiciousWaypoint()
        {
 
            return patrolPath.GetWaypointID(currentWaypointIndex) == susWaypointObjID;
        }

        void DwellBehavior(float scanTime)
        {
            cmp_scanner.SetScanTimeAndStartSweep(scanTime);
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
            if (isSuspicious)
            {
                isSuspicious = false;
                GetComponent<Detector>().SetGuardDogAlert(false);
                searchTime = 0;
                atSusWaypoint = false;
                cmp_scanner.Cancel();
            }
            isSuspicious = true;
            susWaypointObjID = susWaypoint;
            GetComponent<Detector>().SetGuardDogAlert(true);

            if (AtWaypoint())
            {
                timeSinceLastArrived = Mathf.Infinity;
                cmp_mover.StartMoveAction(guardPosition);
            }
        }
    }
}

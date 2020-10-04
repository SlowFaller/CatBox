using UnityEngine;
using LD47.Core;
using LD47.Pathing;

namespace LD47.Control
{
    public class AIController : MonoBehaviour
    {
        private const string TAG = "Player";

        [Header("Attacking")]
        [SerializeField] [Range(0, 5f)] float attackMovementSpeed = 4f;
        [SerializeField] [Range(0, 10f)] float chaseDistance = 0.5f;
        [SerializeField] [Range(0, 10f)] float suspicionTime = 5f;
        [SerializeField] [Range(0, 10f)] float aggroCooldownTime = 5f;
        [SerializeField] [Range(0, 30f)] float shoutDistance = 10f;
        [Header("Patrolling")]
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] [Range(0, 5f)] float patrolMovementSpeed = 2.5f;
        [SerializeField] [Range(0, 9f)] float patrolDwellTime = 3f;
        [SerializeField] [Range(0, 2f)] float waypointTolerance = 1f;

        GameObject obj_player;
        //Fighter cmp_fighter;
        // Health cmp_health;
        Mover cmp_mover;

        int currentWaypointIndex = 0; 
        float timeSinceLastArrived = Mathf.Infinity;
        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceAggro = Mathf.Infinity;
        

        void Awake()
        {
            obj_player = GameObject.FindWithTag(TAG);
            cmp_mover = GetComponent<Mover>();
            // cmp_fighter = GetComponent<Fighter>();
            // cmp_health = GetComponent<Health>();
            

            
        }

        void Start()
        {
            transform.position = patrolPath.GetWaypointPosition(0);
        }


        void Update()
        {
            // if (cmp_health.IsDead()) { return; }

            if (IsEnemyActivated())
            {
                AttackBehavior();
            }
            else if (IsSuspicious())
            {
                WaitingBehavior();
            }
            else
            {
                PatrolBehavior();
            }

            PatrolBehavior();

            UpdateTimers();
        }

        Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        bool InAttackRange()
        {
            float distanceFromPlayer = Vector3.Distance(obj_player.transform.position, transform.position);

            return (distanceFromPlayer < chaseDistance);
        }

        bool IsEnemyActivated()
        {
            return IsAggro() || (InAttackRange());// && cmp_fighter.CanAttack(obj_player));
        }

        bool IsAggro()
        {
            return timeSinceAggro < aggroCooldownTime;
        }

        bool IsSuspicious()
        {
            return timeSinceLastSawPlayer < suspicionTime;
        }

        void AttackBehavior()
        {
            timeSinceLastSawPlayer = 0;
            AggrovateNeighbourEnemies();
            cmp_mover.SetMoveSpeed(attackMovementSpeed);
            //cmp_fighter.Attack(obj_player);
        }

        private void AggrovateNeighbourEnemies()
        {
            var hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);
            foreach (RaycastHit hit in hits)
            {
                var neighbour = hit.transform.GetComponent<AIController>();
                if (neighbour == null) { continue; }

                neighbour.Aggrovate();

            }
        }

        void WaitingBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void PatrolBehavior()
        {
            cmp_mover.SetMoveSpeed(patrolMovementSpeed);
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceLastArrived = 0;
                    CycleWaypoint();
                }
                nextPosition = GetWaypointPosition();
            }
            if (!IsDwelling())
            {
                cmp_mover.StartMoveAction(nextPosition);
            }

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

        bool IsDwelling()
        {
            return timeSinceLastArrived < patrolDwellTime;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        void UpdateTimers()
        {
            timeSinceLastArrived += Time.deltaTime;
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceAggro += Time.deltaTime;
        }
        public void Aggrovate()
        {
            timeSinceAggro = 0f;
            print("I'm pissed now!!");
        }
    }
}

using UnityEngine;
using UnityEngine.AI;
using LD47.Core;

namespace LD47.Control
{
    public class Mover : MonoBehaviour, IAction //, ISaveable
    {
        [SerializeField] float maxPathLength = 40f;
        Ray lastRay;
        NavMeshAgent cmp_agent;
        Animator cmp_animator;
        //Health cmp_health;
        ActionScheduler cmp_scheduler;

        void Awake()
        {
            cmp_agent = GetComponent<NavMeshAgent>();
            cmp_scheduler = GetComponent<ActionScheduler>();
            //cmp_health = GetComponent<Health>();  
            //cmp_animator = GetComponent<Animator>();
        }
        
        float GetPathLength(NavMeshPath path)
        {
            float pathLength = 0f;
            if (path.corners.Length < 2) { return pathLength; }
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                pathLength += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return pathLength;
        }

        public void StartMoveAction(Vector3 destination)
        {
            cmp_scheduler.StartAction(this);
            MoveTo(destination);
        }

        public bool CanMoveTo(Vector3 target)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
            if (!hasPath || path.status != NavMeshPathStatus.PathComplete) { return false; }

            if (GetPathLength(path) > maxPathLength) { return false; }

            return true;
        }

        public void MoveTo(Vector3 destination)
        {
            cmp_agent.isStopped = false;
            cmp_agent.destination = destination;
        }

        public void SetMoveSpeed(float speed)
        {
            cmp_agent.speed = speed;
        }

        public void Cancel()
        {
            cmp_agent.isStopped = true;
        }

        // public object CaptureState()
        // {
        //     return new SerializableVector3(transform.position);
        // }

        // public void RestoreState(object state)
        // {
        //     SerializableVector3 position = (SerializableVector3)state;
        //     cmp_agent.enabled = false;
        //     transform.position = position.ToVector();
        //     cmp_agent.enabled = true;
        //     cmp_scheduler.CancelCurrentAction();
        // }
    }
}
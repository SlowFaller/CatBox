using System.Collections.Generic;
using UnityEngine;

namespace LD47.Pathing
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] List<Transform> waypoints = new List<Transform>();

        private void Awake()
        {
            BuildList();
        }

        void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = (i + 1) % transform.childCount;
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(j).position);
            }
        }

        public int GetNextIndex(int i)
        {
            return (i + 1) % waypoints.Count;
        }

        public Vector3 GetWaypointPosition(int i)
        {
            return waypoints[i].position;
        }

        public int GetWaypointID(int i)
        {
            return waypoints[i].gameObject.GetInstanceID();
        }
       
        private void BuildList()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.tag == "Waypoint")
                {
                    waypoints.Add(child);
                }
            }
        }

    }
}

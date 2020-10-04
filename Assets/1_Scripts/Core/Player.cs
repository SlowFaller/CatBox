using UnityEngine;
using LD47.Pathing;

namespace LD47.Core
{
    public class Player : MonoBehaviour
    {
        private const string TAG = "Waypoint";

        [Header("Object Sockets")]
        [SerializeField] Transform costumeSocket;
        [SerializeField] GameObject pickupSocket;

        bool playerCanPickup = false;
        Waypoint waypointInRange = null;
        Waypoint waypointHeld = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != TAG) { return; }
            if (waypointHeld != null) { return; }

            playerCanPickup = true;
            waypointInRange = other.GetComponent<Waypoint>();
            print("Player Can Pickup " + waypointInRange);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag != TAG) { return; }
            if (waypointHeld != null) { return; }

            playerCanPickup = false;
            waypointInRange = null;
            print("Player Can't Pickup " + waypointInRange);
        }

        public void InteractWithWaypoint()
        {
            
            if (waypointHeld != null)
            {
                PlaceWaypoint();
            }
            else if (playerCanPickup)
            {
                PickupWaypoint();
            }
        }

        private void PickupWaypoint()
        {
            waypointInRange.PickupWaypoint();
            waypointHeld = waypointInRange;
            pickupSocket.SetActive(true);
        }

        private void PlaceWaypoint()
        {
            waypointHeld.PlaceWaypoint();
            waypointHeld = null;
            pickupSocket.SetActive(false);
        }
    }
}

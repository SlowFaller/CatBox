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
        Waypoint waypointToPickup = null;
        Waypoint waypointPickedUp = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != TAG) { return; }
            if (waypointPickedUp != null) { return; }

            playerCanPickup = true;
            waypointToPickup = other.GetComponent<Waypoint>();
            print("Player Can Pickup " + waypointToPickup);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag != TAG) { return; }
            if (waypointPickedUp != null) { return; }

            playerCanPickup = false;
            waypointToPickup = null;
            print("Player Can't Pickup " + waypointToPickup);
        }
        
        public void PickupWaypoint()
        {
            if (playerCanPickup == false) { return; }

            waypointToPickup.PickupWaypoint();
            waypointPickedUp = waypointToPickup;
            waypointToPickup = null;
            playerCanPickup = false;
            pickupSocket.SetActive(true);          
        }
    }
}

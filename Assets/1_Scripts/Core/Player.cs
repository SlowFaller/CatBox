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
        [SerializeField] GameObject popupText;

        bool playerCanPickup = false;
        Waypoint waypointInRange = null;
        Waypoint waypointHeld = null;
        Vector3 tempPos;
        [SerializeField] bool showPopup;

        private void Start()
        {
            if (showPopup)
            {
                popupText.SetActive(true);   
            }
            else
            {
                popupText.SetActive(false);
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                popupText.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != TAG) { return; }
            if (waypointHeld != null) { return; }

            playerCanPickup = true;
            waypointInRange = other.GetComponent<Waypoint>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag != TAG) { return; }
            if (waypointHeld != null) { return; }

            playerCanPickup = false;
            waypointInRange = null;
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

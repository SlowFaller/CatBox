using UnityEngine;
using UnityEngine.UI;

namespace LD47.Pathing
{
    [SelectionBase]
    public class Waypoint : MonoBehaviour
    {
        private const string TAG = "Player";

        [SerializeField] bool isPickedUp = false;
        [SerializeField] ParticleSystem waypointFX;
        [Header("Waypoint Rotation")]
        [SerializeField] float degreesPerSecond = 15.0f;
        [SerializeField] float amplitude = 0.5f;
        [SerializeField] float frequency = 1f;
        [SerializeField] Material idleMaterial;
        [SerializeField] Material pickupMaterial;
        [SerializeField] Material idleTextMaterial;
        [SerializeField] Material pickupTextMaterial;
        [SerializeField] GameObject popupText;


        Vector3 posOffset = new Vector3();
        Vector3 tempPos = new Vector3();
        GameObject obj_player;

        void Awake()
        {
            obj_player = GameObject.FindWithTag(TAG);
            posOffset = transform.position;
            WaypointColor(idleMaterial);

        }

        void Update()
        {
            if (isPickedUp)
            {
                transform.position = obj_player.transform.position;
            }
            else
            {
                Rotate();
                OscillateVertically();
            }     
        }

        void OscillateVertically()
        {
            tempPos = posOffset;
            tempPos.y += (Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude) + amplitude;

            transform.position = tempPos;
        }

        void Rotate()
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
        }

        public void PickupWaypoint()
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponentInParent<DrawPaths>().DrawingPathLines(true);
            popupText.GetComponentInChildren<Text>().material = pickupTextMaterial;
            waypointFX.Stop();
            isPickedUp = true;
            GetComponentInParent<PatrolPath>().WaypointDisrupted(gameObject.GetInstanceID(),true);
            print("I broadcasted " + gameObject.GetInstanceID());
        }

        public void PlaceWaypoint()
        {
            GetComponentInChildren<MeshRenderer>().enabled = true;
            GetComponentInParent<DrawPaths>().DrawingPathLines(false);
            popupText.GetComponentInChildren<Text>().material = idleTextMaterial;
            waypointFX.Play();
            isPickedUp = false;
            posOffset = transform.position;
            GetComponentInParent<PatrolPath>().WaypointDisrupted(gameObject.GetInstanceID(), false);

        }
        void WaypointColor(Material material)
        {
            waypointFX.GetComponent<ParticleSystemRenderer>().material = material;
            GetComponentInChildren<MeshRenderer>().material = material;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player") {return;} 
            WaypointColor(pickupMaterial);
            popupText.SetActive(true);           
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag != "Player") { return; }
            WaypointColor(idleMaterial);
            popupText.SetActive(false);
        }
    }
}
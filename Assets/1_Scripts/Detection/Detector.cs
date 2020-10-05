using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD47.Detection
{
    public class Detector : MonoBehaviour
    {
        Light cmp_pointLight;
        bool alert = false;

        void Awake()
        {
            cmp_pointLight = GetComponentInChildren<Light>();
        }

        // Update is called once per frame
        void Update()
        {
            if (alert)
            {
                cmp_pointLight.color = Color.red;
            }
            else
            {
                cmp_pointLight.color = Color.blue;
            }
        }

        // should trigger when clipping the cone of vision
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player") {return;}
            alert = true;
            print("I see you biatch!");
        }

        public void SetGuardDogAlert(bool isAlerted)
        {
            alert = isAlerted;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD47.Control;

namespace LD47.Detection
{
    public class Detection : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        // should trigger when clipping the cone of vision
        private void OnTriggerEnter(Collider other)
        {
            print("Something collided");
            if (other.gameObject.tag != "Player") {return;}
            gameObject.GetComponent<AIController>().Aggrovate();
            print("I see you biatch!");
        }
    }
}

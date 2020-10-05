using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD47.Control
{
    public class ScanTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.GetComponent<Scanning>().SetScanTimeAndStartSweep(4.0f);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

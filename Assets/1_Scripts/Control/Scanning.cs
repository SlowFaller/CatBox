using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD47.Control
{
    public class Scanning : MonoBehaviour
    {
        float scanTimer = 0.0f;
        float scanDuration = 0.0f;
        [SerializeField] bool isScanning = false;
        float scanRemainder = 0.0f;
        float scanSpeed = 1.0f;
        bool startLeft = false;

        float minDegreesSweep = 65.0f;
        float maxDegreesSweep = 90.0f;

        float leftSweep = 0.0f;
        float rightSweep = 0.0f;

        // Update is called once per frame
        void Update()
        {
            // math shit (from Unity docs)
            if(isScanning)
            {
                scanTimer += Time.deltaTime;

                Scan();

                // Reset the animation time if it is greater than the planned time.
                if (scanTimer > scanDuration)
                {
                    scanTimer = 0.0f;
                    isScanning = false;
                }
            }
        }

        bool StartLeftNotRight()
        {
            float randomValue = Random.Range(0.0f, 1.0f);

            if(randomValue > 0.5f)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void SetScanTimeAndStartSweep(float scanTime = 3.0f)
        {
            scanDuration = scanTime;
            isScanning = true;
            startLeft = StartLeftNotRight();

            leftSweep = Random.Range(minDegreesSweep, maxDegreesSweep);
            rightSweep = Random.Range(minDegreesSweep, maxDegreesSweep);

            if(startLeft)
            {
                scanRemainder = leftSweep;
            }
            else
            {
                scanRemainder = rightSweep;
            }
        }

        void toggleDirection()
        {
            startLeft = !startLeft;
        }

        void Scan()
        {
            if(startLeft)
            {
                // get next rotational slice for this frame
                float delta = Time.deltaTime * leftSweep;
                float bobAmt = Mathf.Sin(scanTimer) * 0.5f; //0.5f to slow it down a bit

                //transform.position = new Vector3(transform.position.x, bobAmt, transform.position.z);
                transform.Rotate(0.0f, delta, 0.0f, Space.Self);

                // recalculate remainder
                scanRemainder -= delta;
                if(scanRemainder <= 0.0f)
                {
                    toggleDirection();
                    if(startLeft)
                    {
                        scanRemainder = leftSweep;
                    }
                    else
                    {
                        scanRemainder = rightSweep;
                    }
                }

                // check for collision with wall here
                
            }
            else
            {
                // get next rotational slice for this frame
                float delta = Time.deltaTime * rightSweep;
                float bobAmt = Mathf.Sin(scanTimer) * 0.5f; //0.5f to slow it down a bit

                //transform.position = new Vector3(transform.position.x, bobAmt, transform.position.z);
                transform.Rotate(0.0f, -delta, 0.0f, Space.Self);

                // recalculate remainder
                scanRemainder -= delta;
                if(scanRemainder <= 0.0f)
                {
                    toggleDirection();
                    if(startLeft)
                    {
                        scanRemainder = leftSweep;
                    }
                    else
                    {
                        scanRemainder = rightSweep;
                    }
                }

                // check for collision with wall here

            }
        }
    }
}

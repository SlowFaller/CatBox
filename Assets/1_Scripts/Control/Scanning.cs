using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD47.Control
{
    public class Scanning : MonoBehaviour
    {
        float scanTimer = 0.0f;
        float scanDuration = 0.0f;
        bool isScanning = false;
        float scanRemainder = 0.0f;
        float scanSpeed = 1.0f;
        bool startLeft = false;

        float minDegreesSweep = 65.0f;
        float maxDegreesSweep = 90.0f;

        float leftSweep = 0.0f;
        float rightSweep = 0.0f;


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // if(scanTimer < scanDuration)
            // {
            //     scanTimer += Time.deltaTime;
            // }
            // else
            // {
            //     scanTimer = scanDuration;
            //     stopScanning = true;
            // }

            // math shit (from Unity docs)
            if(isScanning)
            {
                scanTimer += Time.deltaTime;
                float scanSin = Mathf.Sin(scanTimer);

                // Increase animation time.
                float scanDelta = scanSpeed * scanSin;

                Scan(scanDelta);

                // Reset the animation time if it is greater than the planned time.
                // if (scanDelta > Mathf.PI * 2.0f)
                // {
                //     scanTimer = 0.0f;
                // }

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

        public void SetScanTimeAndStartSweep(float scanTime = 3.0f, float speed = 1.0f)
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

        void Scan(float delta)
        {
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

            if(startLeft)
            {
                //float rotateAmt = Mathf.Clamp(delta, minDegreesSweep, maxDegreesSweep);
                //transform.position = new Vector3(0.0f, rotateAmt, 0.0f);
                transform.Rotate(0.0f, delta, 0.0f, Space.Self);

                // check for collision with wall here
            }
            else
            {
                //float rotateAmt = Mathf.Clamp(delta, minDegreesSweep, maxDegreesSweep);
                //transform.position = new Vector3(0.0f, rotateAmt, 0.0f);
                transform.Rotate(0.0f, -delta, 0.0f, Space.Self);

                // check for collision with wall here
            }
        }
    }
}

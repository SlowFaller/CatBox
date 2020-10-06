using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD47.Core;

namespace LD47.Control
{
    public class Scanning : MonoBehaviour, IAction
    {
        float scanTimer = 0.0f;
        float scanDuration = 0.0f;
        [SerializeField] bool isScanning = false;
        float scanRemainder = 0.0f;
        float scanSpeed = 1.0f;
        bool startLeft = false;
        bool wallDetected = false;

        float minDegreesSweep = 65.0f;
        float maxDegreesSweep = 90.0f;

        float leftSweep = 0.0f;
        float rightSweep = 0.0f;

        [SerializeField] float amplitude = 0.5f;
        [SerializeField] float frequency = 1f;

        Vector3 posOffset = new Vector3();
        Vector3 tempPos = new Vector3();

        ActionScheduler cmp_scheduler;

        private void Awake()
        {
            cmp_scheduler = GetComponent<ActionScheduler>();
            frequency = Random.Range(.6f, 1.3f);
            amplitude = Random.Range(0f, .2f);
            
        }

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
            else if (!isScanning && wallDetected)
            {
                SetScanTimeAndStartSweep();
            }
            OscillateVertically();     
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

                transform.Rotate(0.0f, delta, 0.0f, Space.Self);

                // recalculate remainder
                scanRemainder -= delta;
                if(scanRemainder < 0.0f)
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
                else if (wallDetected)
                {
                    toggleDirection();
                    if (startLeft)
                    {
                        scanRemainder += leftSweep;
                    }
                    else
                    {
                        scanRemainder += rightSweep;
                    }
                    wallDetected = false;
                }
                
            }
            else
            {
                // get next rotational slice for this frame
                float delta = Time.deltaTime * rightSweep;
                float bobAmt = Mathf.Sin(scanTimer) * 0.5f; //0.5f to slow it down a bit

                transform.Rotate(0.0f, -delta, 0.0f, Space.Self);

                // recalculate remainder
                scanRemainder -= delta;
                if(scanRemainder < 0.0f)
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
                else if (wallDetected)
                {
                    toggleDirection();
                    if (startLeft)
                    {
                        scanRemainder += leftSweep;
                    }
                    else
                    {
                        scanRemainder += rightSweep;
                    }
                    wallDetected = false;
                } 
            }
        }

        void OscillateVertically()
        {
            if (Time.timeScale < 1) { return; }
            tempPos = transform.position;
            tempPos.y += (Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude) + amplitude;
            tempPos.x = transform.position.x;
            tempPos.z = transform.position.y;

            transform.position = new Vector3(transform.position.x, tempPos.y, transform.position.z);
        }


        public void Cancel()
        {
            scanTimer = 0.0f;
            isScanning = false;
        }
        public void SetWallDetected(bool wallToLeft)
        {
            // wallDetected = true;
            // startLeft = wallToLeft;
        }
    }
}

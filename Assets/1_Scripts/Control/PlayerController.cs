using ECM.Components;
using ECM.Controllers;
using UnityEngine;
using LD47.Core;
using LD47.UI;

namespace LD47.Control
{
    public class PlayerController : BaseCharacterController
    {
        Fader obj_fader;

        void start()
        {
            obj_fader = FindObjectOfType<Fader>();
        }
        protected override void HandleInput()
        {
            // Toggle pause / resume.
            // By default, will restore character's velocity on resume (eg: restoreVelocityOnResume = true)

            if (Input.GetKeyDown(KeyCode.P))
                {
                    pause = !pause;
                    if(Time.timeScale > 0)
                    {
                        Time.timeScale = 0;
                        //obj_fader.FadeToAlpha(0.7f, 0.5f);
                    }
                    else
                    {
                        Time.timeScale = 1.0f;
                        //obj_fader.FadeToAlpha(0.0f, 0.5f);
                    }
                }

            // Handle user input

            moveDirection = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = 0.0f,
                z = Input.GetAxisRaw("Vertical")
            };

            jump = Input.GetButton("Jump");

            crouch = Input.GetKey(KeyCode.C);

            if (Input.GetKeyDown(KeyCode.F))
            {
                GetComponent<Player>().InteractWithWaypoint();
            }
        }
        
        protected override void Animate()
        {
            Animator cmp_animator = GetComponentInChildren<Animator>();
            cmp_animator.SetFloat("forwardSpeed", GetComponent<CharacterMovement>().forwardSpeed);
        }
    }
}

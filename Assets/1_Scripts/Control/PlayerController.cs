using ECM.Components;
using ECM.Controllers;
using UnityEngine;
using LD47.Core;

namespace LD47.Control
{
    public class PlayerController : BaseCharacterController
    {
        protected override void HandleInput()
        {
            // Toggle pause / resume.
            // By default, will restore character's velocity on resume (eg: restoreVelocityOnResume = true)

            if (Input.GetKeyDown(KeyCode.P))
                pause = !pause;

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

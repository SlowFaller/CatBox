using ECM.Components;
using ECM.Controllers;
using UnityEngine;

namespace LD47.Control
{
    public class PlayerController : BaseCharacterController
    {
        
        protected override void Animate()
        {
            Animator cmp_animator = GetComponentInChildren<Animator>();
            cmp_animator.SetFloat("forwardSpeed", GetComponent<CharacterMovement>().forwardSpeed);
        }
    }
}

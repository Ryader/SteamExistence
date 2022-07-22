using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{

    public class MainController : MonoBehaviour
    {

        public Vector3 moveDirection;
        public CharacterController contr;
        public Animator anim;
        public float gravity = 10;
        public float jump = 70;
        public GameObject cam;

        void Update()
        {
            if (contr.isGrounded)
            {
                anim.SetFloat("moveX", Input.GetAxis("Horizontal"));
                anim.SetFloat("moveY", Input.GetAxis("Vertical"));
            }
            else
            {
                moveDirection.y -= gravity;
            }
            contr.Move(moveDirection * Time.deltaTime);

        }
    }
}

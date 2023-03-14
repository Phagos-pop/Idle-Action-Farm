using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    [SerializeField] Joystick joystick;
    [SerializeField] float sensitivity;
    [SerializeField] float speedMove;
    [SerializeField] private Transform TransformMain;

    private Vector3 moveVector;
    private float gravityForce;

    private void Start()
    {
        gravityForce = 0;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Gravity();
    }

    private void Move()
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.Direction.x;
        moveVector.z = joystick.Direction.y;

        if (moveVector.x != 0 || moveVector.z != 0)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 derect = Vector3.RotateTowards(TransformMain.forward, moveVector, speedMove, 0.0f);
            TransformMain.rotation = Quaternion.LookRotation(derect);
        }

        moveVector.x = joystick.Direction.x * sensitivity;
        moveVector.z = joystick.Direction.y * sensitivity;

        moveVector.y = gravityForce;
        characterController.Move(moveVector);
        TransformMain.position = this.transform.position;
    }

    private void Gravity()
    {
        if (!characterController.isGrounded)
        {
            gravityForce -= 20f * Time.deltaTime;
        }
        else
        {
            gravityForce = -1f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("PlayerSpeed")] [SerializeField] float playerSpeed;
    public float PlayerSpeed { get { return playerSpeed; } set { playerSpeed = value; } }

    Animator animator;
    public Animator PlayerAnimator { get { return animator; } set { animator = value; } }

    CharacterController characterController;
    Vector3 velocity;
    Vector2 direction;
    bool isPlayerGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        PlayerMovement();
        LookInMousePosition();
    }


    void PlayerMovement()
    {
        isPlayerGrounded = characterController.isGrounded;
        if (isPlayerGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        movement = Vector3.ClampMagnitude(movement, 1) * playerSpeed * Time.deltaTime;

        characterController.Move(movement);

        animator.SetFloat("BlendY", Input.GetAxisRaw("Vertical"));
        animator.SetFloat("BlendX", Input.GetAxisRaw("Horizontal"));

    }

    void LookInMousePosition()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(mouseRay, out RaycastHit hit))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, 0f, hit.point.z);
            transform.LookAt(targetPosition);
            Debug.DrawLine(transform.position, targetPosition, Color.green);
        }
    }
}


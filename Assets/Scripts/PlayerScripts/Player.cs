using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Stats stats;

    Animator animator;
    public Animator PlayerAnimator { get { return animator; } set { animator = value; } }

    CharacterController characterController;
    Vector3 velocity;
    Vector2 direction;
    bool isPlayerGrounded;
    bool isAttacking;
    [SerializeField] LayerMask enemyLayer;
    //PlayerWeaponCollider
    [Header("PlayerWeapon")][SerializeField] Transform axe;
    [SerializeField] float axeDmg;


    void Start()
    {
        stats = GetComponent<Stats>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMovement();

        LookInMousePosition();

        Attack();

    }


    void PlayerMovement()
    {
        if (!isAttacking)
        {
            isPlayerGrounded = characterController.isGrounded;
            if (isPlayerGrounded && velocity.y < 0)
            {
                velocity.y = 0;
            }

            Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            movement = Vector3.ClampMagnitude(movement, 1) * stats.Speed * Time.deltaTime;

            characterController.Move(movement);

            animator.SetFloat("BlendY", Input.GetAxisRaw("Vertical"));
            animator.SetFloat("BlendX", Input.GetAxisRaw("Horizontal"));
        }

    }

    void LookInMousePosition()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out RaycastHit hit))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, 0f, hit.point.z);
            transform.LookAt(targetPosition);
            Debug.DrawLine(transform.position, targetPosition, Color.green);
        }
    }

    void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            PlayerAnimator.SetBool("isAttacking", true);
            PlayerAnimator.speed = stats.AttackSpeed;
            isAttacking = true;
            Collider[] enemys = Physics.OverlapSphere(axe.position, 0.1f, enemyLayer);
            foreach(var obj in enemys)
            {
                if (obj.GetComponent<IDamageable>() != null)
                {
                    obj.GetComponent<IDamageable>().TakeDmg(axeDmg);
                }
            }
        }
        else
        {
            PlayerAnimator.SetBool("isAttacking", false);
            isAttacking = false;
        }
    }

}


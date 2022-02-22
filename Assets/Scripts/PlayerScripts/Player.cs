using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Stats stats;

    Animator animator;
    public Animator PlayerAnimator { get { return animator; } set { animator = value; } }
    Ray mouseRay;
    CharacterController characterController;
    Vector3 velocity;
    bool isPlayerGrounded;
    bool isAttacking;
    [SerializeField] LayerMask enemyLayer;
    //PlayerWeapon
    [Header("PlayerWeapon")] [SerializeField] Transform axe;
    [SerializeField] float axeDmg;
    bool isHittingOnce;

    //Playerdirections
    Vector3 rightCornerUp;
    Vector3 leftCornerUp;
    Vector3 rightCornerDown;
    Vector3 leftCornerDown;

    void Start()
    {
        stats = GetComponent<Stats>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rightCornerUp = new Vector3(transform.rotation.x, 45f, transform.rotation.z);
        leftCornerUp = new Vector3(transform.rotation.x, -45f, transform.rotation.z);
        rightCornerDown = new Vector3(transform.rotation.x, 135f, transform.rotation.z);
        leftCornerDown = new Vector3(transform.rotation.x, -135f, transform.rotation.z);
    }

    void Update()
    {
        PlayerMovement();

        LookInMousePosition();

        Attack();

    }


    void PlayerMovement()
    {
        Vector3 playerheight = new Vector3(transform.position.x, 0f, transform.position.z);
        transform.position = playerheight;

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

            if (transform.localEulerAngles.y <= rightCornerUp.y && transform.localEulerAngles.y >= leftCornerUp.y)
            {
                Debug.Log("LinksOben, rechtsOben");
                animator.SetFloat("BlendY", Input.GetAxisRaw("Vertical"));
                animator.SetFloat("BlendX", Input.GetAxisRaw("Horizontal"));
            }
            else if(transform.localEulerAngles.y >= rightCornerUp.y && transform.localEulerAngles.y <= rightCornerDown.y)
            {
                Debug.Log("rechtsOben, rechtsUnten");
                animator.SetFloat("BlendX", -Input.GetAxisRaw("Vertical"));
                animator.SetFloat("BlendY", Input.GetAxisRaw("Horizontal"));
            }
            else if (transform.localEulerAngles.y >= leftCornerDown.y && transform.localEulerAngles.y >= leftCornerUp.y)
            {
                Debug.Log("linksUnten, linksOben");
                animator.SetFloat("BlendX", Input.GetAxisRaw("Vertical"));
                animator.SetFloat("BlendY", -Input.GetAxisRaw("Horizontal"));
            }
            else if(transform.localEulerAngles.y >= rightCornerDown.y && transform.localEulerAngles.y >= leftCornerDown.y)
            {
                Debug.Log("rechtsUnten, linksUnten");
                animator.SetFloat("BlendY", -Input.GetAxisRaw("Vertical"));
                animator.SetFloat("BlendX", -Input.GetAxisRaw("Horizontal"));
            }

        }

    }

    void LookInMousePosition()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hit))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, 0f, hit.point.z);
            transform.LookAt(new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
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
            PlayerAnimator.SetBool("isAttacking", isAttacking);
            Collider[] enemys = Physics.OverlapSphere(axe.position, 0.1f, enemyLayer);
            if (!isHittingOnce)
            {
                isHittingOnce = true;
                foreach (var obj in enemys)
                {
                    if (obj.GetComponent<IDamageable>() != null)
                    {
                        obj.GetComponent<IDamageable>().TakeDmg(axeDmg);
                    }
                }
            }
        }
        else
        {
            isHittingOnce = false;
            isAttacking = false;
            PlayerAnimator.SetBool("isAttacking", isAttacking);
        }
    }

}


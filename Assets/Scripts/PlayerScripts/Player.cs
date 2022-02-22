using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Stats stats;

    CharacterController characterController;
    Animator animator;
    Ray mouseRay;
    Vector3 velocity;
    bool isPlayerGrounded;
    bool isAttacking;
    public bool IsAttacking { set { isAttacking = value; } }
    [SerializeField] LayerMask enemyLayer;

    //PlayerWeapon
    [Header("PlayerWeapon")] [SerializeField] Transform axe;
    bool isHittingOnce;

    //Player Animation directions
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
        leftCornerUp = new Vector3(transform.rotation.x, 315f, transform.rotation.z);
        rightCornerDown = new Vector3(transform.rotation.x, 135f, transform.rotation.z);
        leftCornerDown = new Vector3(transform.rotation.x, 225f, transform.rotation.z);
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
            if(movement.magnitude >= 0)
            {
                SoundManager.instance.PlayAudioClip(ESoundType.PlayerWalking, GetComponent<AudioSource>());
            }

            //Movement Animations
            if (transform.localEulerAngles.y <= rightCornerUp.y || transform.localEulerAngles.y >= leftCornerUp.y)
            {
                animator.SetFloat("BlendY", Input.GetAxisRaw("Vertical"));
                animator.SetFloat("BlendX", Input.GetAxisRaw("Horizontal"));
            }
            if(transform.localEulerAngles.y >= rightCornerUp.y && transform.localEulerAngles.y <= rightCornerDown.y)
            {
                animator.SetFloat("BlendX", -Input.GetAxisRaw("Vertical"));
                animator.SetFloat("BlendY", Input.GetAxisRaw("Horizontal"));
            }
            if (transform.localEulerAngles.y >= leftCornerDown.y && transform.localEulerAngles.y <= leftCornerUp.y)
            {
                animator.SetFloat("BlendX", Input.GetAxisRaw("Vertical"));
                animator.SetFloat("BlendY", -Input.GetAxisRaw("Horizontal"));
            }
            if(transform.localEulerAngles.y >= rightCornerDown.y && transform.localEulerAngles.y <= leftCornerDown.y)
            {
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
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.instance.PlayAudioClip(ESoundType.EnemyHit, GetComponent<AudioSource>());
            animator.SetBool("isAttacking", true);
            animator.speed = stats.AttackSpeed;

            Collider[] enemys = Physics.OverlapSphere(axe.position, 0.1f, enemyLayer);
            if (!isHittingOnce)
            {
                foreach (var obj in enemys)
                {
                    if (obj.GetComponent<IDamageable>() != null)
                    {
                        SoundManager.instance.PlayAudioClip(ESoundType.PlayerAttack, GetComponent<AudioSource>());
                        obj.GetComponent<IDamageable>().TakeDmg(stats.Dmg);
                    }
                }
            }
        }
    }

}


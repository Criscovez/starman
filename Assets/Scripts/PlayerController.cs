// PlayerController.cs
// Usando referencias de:
// www.udemy.com/course/unity-metvania/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;

    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    private bool isOnGround;
    public LayerMask whatIsGround;

    public Animator anim;

    public BulletController shotToFire;

    public Transform shotPoint;

    private bool canDoubleJump;

    private PlayerAbilityTracker abilities;
    public bool canMove;

    void Start()
    {
        abilities = GetComponent<PlayerAbilityTracker>();

        canMove = true;
    }

    void Update()
    {
        if (canMove && Time.timeScale != 0)
        {
            // moviminto hacia los lados
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

            //Manejador de la direccion del personaje

            if (theRB.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (theRB.velocity.x > 0)
            {
                transform.localScale = Vector3.one;
            }

            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

            if (Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilities.canDoubleJump)))
            {
                AudioController.instance.PlaySFXAdjusted(1);
                if (isOnGround)
                {
                    canDoubleJump = true;
                }
                else
                {
                    canDoubleJump = false;
                    anim.SetTrigger("doubleJump");
                    AudioController.instance.PlaySFXAdjusted(0);
                }



                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }

            if (Input.GetButtonDown("Fire2") && isOnGround)
            {
                anim.SetTrigger("kick");
                AudioController.instance.PlaySFXAdjusted(10);
                //theRB.velocity = new Vector2(jumpForce / 2, theRB.velocity.y);
                //theRB.velocity = new Vector2(theRB.velocity.x, jumpForce * 0.7f);
                //Debug



            }

            if (Input.GetButtonDown("Fire1") && abilities.canUseGun)
            {

                if (PlayerHealthController.instance.currentBullets>0) {
                    Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
                    AudioController.instance.PlaySFXAdjusted(3);
                    PlayerHealthController.instance.currentBullets--;
                    UIController.instance.bulletsText.text = PlayerHealthController.instance.currentBullets.ToString();
                }

                anim.SetTrigger("shotFired");
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }
        anim.SetBool("isOnGround", isOnGround);
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
    }
}

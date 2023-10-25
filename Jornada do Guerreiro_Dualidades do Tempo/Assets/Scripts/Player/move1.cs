using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 5f;
    private bool isFacingRight = true;

    public Animator animator;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;


    void Star()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //retorna caso o personagem esteja em dash
        if(isDashing)
        {
            return;
        }
        
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        
        //----------------------------------------------------------------------------------

        if (Input.GetAxis("Horizontal") != 0)
        {
            //animar quando estiver Correndo
            animator.SetBool("taAndando", true);
        }
        else
        {
            //animar parado
            animator.SetBool("taAndando", false);
        }


        if (Input.GetAxis("Horizontal") = 0)
        {
            animator.SetBool("taParado", true);
        }
        else
        {
            animator.SetBool("taParado", false);
        }

        if (IsGrounded())
        {
            animator.SetBool("taPulando", false);
        }
        else
        {
            animator.SetBool("taPulando", true);
        }    

        Flip();
    }

    private void FixedUpdate()
    {   
        if(isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    { 

        animator.SetTrigger("dash");
 
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentação : MonoBehaviour
{
    
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;   
    private bool isFront;
    private bool isWallSliding;
    private bool isWallJumping;
    private bool isDashing = false;
    private bool isInCooldown=false; 
    private float dir;
    private float dir2;
    private int facingDir; 
    private bool isFacingRight = true;
    private bool parado = true;
    public bool isControllable = true;
    
    public Transform frontCheck;
    public float speed;
    public float wallSlideSpeed;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    public float jumpforce;
    public float dashforce;
    public float dashDuration;
    public float dashCooldown;

    public Animator animator2;

    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask platformLayerMask;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    

    // Use this for initialization
    void Start()
    {

        body = GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        JumpControl();

        if (Input.GetKeyDown(KeyCode.Z) && !isInCooldown)
        {
            parado = false;
            animator2.SetTrigger("dash2");
            StartCoroutine(DashControl());
        }

        WallJumpControl();

        Flip();
        
        //-------------------------------------------------- animador

        if (Input.GetAxis("Horizontal") != 0)
        {
            //animar quando estiver Correndo
            animator2.SetBool("taAndando", true);
            animator2.SetBool("parado", true);
        }
        else
        {
            //animar parado
            animator2.SetBool("taAndando", false);
            animator2.SetBool("parado", true);
        }

        if (IsGrounded())
        {
            animator2.SetBool("taPulando", false);
            animator2.SetBool("wall", false);
            animator2.SetBool("no ar", false);
            animator2.SetBool("ARtaque", false);
        }
        else
        {
            animator2.SetBool("taPulando", true);
            animator2.SetBool("no ar", true);    
            animator2.SetBool("taAndando", false);    
              
        } 


        if(isWallSliding)
        {
            animator2.SetBool("wall", true);
            animator2.SetBool("no ar", false);       
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    void FixedUpdate() //basic horizontal movement
    {
        if (!isWallJumping)
        {
            dir = Input.GetAxisRaw("Horizontal");

            if (dir < 0)
            {
                facingDir = 0; 
            }
            else if (dir > 0) {
                facingDir = 1; 
            }

            if (!isDashing)
            {
                body.velocity = new Vector2(dir * speed, body.velocity.y);
                
            }
        }
    }

    private bool OnGround() //ground check
    {
        float extraHeight = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, platformLayerMask);

        return ((raycastHit.collider != null));

    }

    private void JumpControl()
    {

        if (Input.GetKeyDown(KeyCode.Space) && OnGround())
        {
            body.velocity = Vector2.up * jumpforce;
            animator2.SetBool("ARtaque", true);
        }

    }

    private void WallJumpControl()
    {
        isFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, platformLayerMask);
        if (isFront && !OnGround() && dir != 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, wallSlideSpeed, float.MaxValue));

        }

        if (Input.GetKeyDown(KeyCode.Space) && isWallSliding)
        {
            isWallJumping = true;
            StartCoroutine(WallJumpCooldown());
        }

        if (isWallJumping)
        {
            
            body.velocity = new Vector2(xWallForce * -dir, yWallForce);

            isFacingRight = !isFacingRight;
            transform.Rotate(0,180,0);
            
        }
    }

    private IEnumerator DashControl()
    {
        isDashing = true;
        isInCooldown = true;
        animator2.SetBool("dashdow", true);


        if (facingDir == 1)
        {
            body.velocity = Vector2.right * dashforce;
        }
        else if(facingDir == 0)
        {
            body.velocity = Vector2.left* dashforce;
        }  

        yield return new WaitForSeconds(dashDuration); 
        isDashing = false;
        animator2.SetBool("dashdow", false);

        
        

        yield return new WaitForSeconds(dashDuration);
        isInCooldown = false;
        
        
        

    }

    private IEnumerator WallJumpCooldown()
    {
        
        yield return new WaitForSeconds(wallJumpTime);
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && dir < 0f || !isFacingRight && dir > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        
    }   
  
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D boxCollider2d;
    public delegate void PlayerDetectedCallback();
    //HuntressStateController huntressState;
    LayerMask groundLayerMask;

    public float velocity;
    public float detectRangeX;
    public float attackRange;
    public float attackCooldown;

    private bool grounded;
    private float vel;
    private float xDistanceToPlayer;
    private float attackTime;
    private float animationDefaultSpeed;
    private bool startedCoroutine = false;
    public states state;
    public types type;

    public enum states
    {
        onRoof,
        chasing,
        idle,
    }

    public enum types
    {
        moving,
        still,
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Huntress");
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        groundLayerMask = LayerMask.GetMask("Ground");
        spriteRenderer.flipY = true;
        animationDefaultSpeed = animator.speed;
        //type = types.moving;
        state = states.onRoof;
        facingRight();

    }

    // Update is called once per frame
    void Update()
    {
        xDistanceToPlayer = Mathf.Abs(player.transform.position.x - transform.position.x);
        CheckIfGrounded();
        switch (state)
        {
            case states.onRoof:
                OnRoofAction();
                break;
            case states.chasing:
                ChasingAction();
                break;
            case states.idle:
                IdleAction();
                break;
            default:
                break;
        }

    }

    private void OnRoofAction()
    {
        if (type.Equals(types.moving))
        {
            rigidBody.velocity = new Vector2(vel, rigidBody.velocity.y);
            if (!startedCoroutine)
            {
                StartCoroutine(changeDirection());
                startedCoroutine = true;
            }
        }

        PlayerDetectedCallback onPlayerDetect = playerDetected;
        PlayerInRange(onPlayerDetect);

        void playerDetected()
        {
            if (state == states.onRoof)
            {
                StopCoroutine(changeDirection());
                animator.SetTrigger("Jump");
                state = states.idle;
            }
        }

        
    }

    private void ChasingAction()
    {
        PlayerDetectedCallback onPlayerDetect = playerDetected;
        bool isInRange = PlayerInRange(onPlayerDetect);
        setSlimeVel();
        attackTime += Time.deltaTime;
        Debug.Log(attackTime);

        void playerDetected()
        {
            if (grounded)
            {
                
                if (Vector2.Distance(player.transform.position, transform.position) > attackRange)
                {
                    
                    rigidBody.velocity = new Vector2(vel, rigidBody.velocity.y);
                }
                else
                {
                    if (Vector2.Distance(player.transform.position, transform.position) <= attackRange)
                    {
                        
                        if (attackTime >= attackCooldown)
                        {
                            animator.SetTrigger("Attack");
                            attackTime = 0.0f;
                        }
                    }
                }
            }
        }
        if (!isInRange)
        {
            state = states.idle;
        }
    }

    private void IdleAction()
    {
        PlayerDetectedCallback onPlayerDetect = playerDetected;
        PlayerInRange(onPlayerDetect);

        void playerDetected()
        {
            state = states.chasing;
        }
    }

    private bool PlayerInRange(PlayerDetectedCallback callback)
    {
        //Debug.Log(Mathf.Abs(player.transform.position.x - transform.position.x));
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= detectRangeX)
        {
            callback();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void setSlimeVel()
    {
        if(player.transform.position.x < transform.position.x)
        {
            facingLeft();
        }
        else
        {
            facingRight();            
        }
    }

    private void facingLeft()
    {
        vel = -velocity;
        spriteRenderer.flipX = false;
    }

    private void facingRight()
    {
        vel = velocity;
        spriteRenderer.flipX = true;
    }

    private void onJumpDrop()
    {
        rigidBody.gravityScale = 5;
        spriteRenderer.flipY = false;
    }

    private void landingFrame()
    {
        animator.speed = 0;
    }

    IEnumerator changeDirection()
    {
        while (true)
        {
            vel *= -1;
            yield return new WaitForSeconds(3);
        }
    }

    private void CheckIfGrounded()
    {
        float extraHeight = 0.2f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeight, groundLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
            grounded = true;
            animator.speed = animationDefaultSpeed;
        }
        else
        {
            rayColor = Color.red;
            grounded = false;
        }

        //Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeight), rayColor);
        //Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeight), rayColor);
        //Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeight), Vector2.right * (boxCollider2d.bounds.size.x), rayColor);
        //Debug.Log(raycastHit.collider);

    }


}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementController : MonoBehaviour
{

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2d;
    HuntressStateController huntressState;
    LayerMask groundLayerMask;

    [HideInInspector] public float velocity_y;
    [HideInInspector] public bool grounded = true;
    [HideInInspector] public float inputX;
    [HideInInspector] public int facingDirection = 1;
    
    public float speed;
    public float jumpForce;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        huntressState = GetComponent<HuntressStateController>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();

        velocity_y = rigidBody.velocity.y;

        inputX = Input.GetAxis("Horizontal");

        if (!huntressState.dead)
        {
            rigidBody.velocity = new Vector2(inputX * speed, rigidBody.velocity.y);
            CheckIfFlip(inputX);
        }

        //Jump
        if (Input.GetKeyDown("space") && grounded)
        {
            grounded = true;  
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    private void CheckIfFlip(float inputX)
    {
        if (inputX > 0)
        {
            spriteRenderer.flipX = false;
            facingDirection = 1;
        }
        else if (inputX < 0)
        {
            spriteRenderer.flipX = true;
            facingDirection = -1;
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
        }
        else
        {
            rayColor = Color.red;
            grounded = false;
        }        
        
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeight), Vector2.right * (boxCollider2d.bounds.size.x), rayColor);
        //Debug.Log(raycastHit.collider);
        
    }
}


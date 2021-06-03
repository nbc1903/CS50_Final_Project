using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntressAnimationsController : MonoBehaviour
{
    Animator animator;
    PlayerMovementController playerMovement;
    HuntressStateController huntressState;

    private float delayToIdle = 0.0f;
    private float timeSinceAttack = 0.0f;
    private float currentAttack = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovementController>();
        huntressState = GetComponent<HuntressStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;

        animator.SetFloat("AirSpeedY", playerMovement.velocity_y);
        animator.SetBool("Grounded", playerMovement.grounded);

        if (huntressState.hp <= 0)
        {
            if (!huntressState.dead)
            {
                animator.SetTrigger("Death");
            }
        }
        else if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("j")) && timeSinceAttack > 0.5f)
        {

            currentAttack++;
            //playerMovement.attacking = true;

            if (currentAttack > 3)
                currentAttack = 1;

            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            //attackCount = 0;

            animator.SetTrigger("Attack" + currentAttack);
            timeSinceAttack = 0.0f;
        }
        else if (Input.GetKeyDown("space") && playerMovement.grounded)
        {
            animator.SetTrigger("Jump");
        }
        else if (Mathf.Abs(playerMovement.inputX) > Mathf.Epsilon)
        {
            delayToIdle = 0.05f;
            animator.SetBool("Running", true);
        }
        else
        {
            delayToIdle -= Time.deltaTime;
            if (delayToIdle < 0)
            {
                animator.SetBool("Running", false);
            }
        }
    }
}

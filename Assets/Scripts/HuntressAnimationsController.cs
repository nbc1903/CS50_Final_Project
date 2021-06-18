using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntressAnimationsController : MonoBehaviour
{
    Animator animator;
    PlayerMovementController playerMovement;
    EventsController eventsController;

    private float delayToIdle = 0.0f;
    private float timeSinceAttack = 0.0f;
    private float currentAttack = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovementController>();
        eventsController = GetComponent<EventsController>();

        eventsController.OnPlayerDeath += OnPlayerDeath;
        eventsController.OnPlayerAttack += OnPlayerAttack;
        eventsController.OnPlayerJump += OnPlayerJump;
        eventsController.OnPlayerMove += OnPlayerMove;
        eventsController.OnPlayerIdle += OnPlayerIdle;

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;

        animator.SetFloat("AirSpeedY", playerMovement.velocity_y);
        animator.SetBool("Grounded", playerMovement.grounded);

    }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        animator.SetTrigger("Death");
    }

    void OnPlayerAttack(object sender, EventArgs e)
    {
        if(timeSinceAttack > 0.5f)
        {
            currentAttack++;
            //playerMovement.attacking = true;

            if (currentAttack > 2)
                currentAttack = 1;

            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            //attackCount = 0;

            animator.SetTrigger("Attack" + currentAttack);
            timeSinceAttack = 0.0f;
        }
    }

    void OnPlayerJump(object sender, EventArgs e)
    {
        animator.SetTrigger("Jump");
    }

    void OnPlayerMove(object sender, EventArgs e)
    {
        delayToIdle = 0.05f;
        animator.SetBool("Running", true);
    }

    void OnPlayerIdle(object sender, EventArgs e)
    {
        delayToIdle -= Time.deltaTime;
        if (delayToIdle < 0)
        {
            animator.SetBool("Running", false);
        }
    }
}

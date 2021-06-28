using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsController : MonoBehaviour
{
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnPlayerAttack;
    public event EventHandler OnPlayerJump;
    public event EventHandler OnPlayerMove;
    public event EventHandler OnPlayerIdle;
    public event EventHandler OnPlayerInteract;

    HuntressStateController huntressState;
    PlayerMovementController playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovementController>();
        huntressState = GetComponent<HuntressStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (huntressState.hp <= 0)
        {
            if (!huntressState.dead)
            {
                OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            }
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("j"))
        {
            OnPlayerAttack?.Invoke(this, EventArgs.Empty);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            OnPlayerInteract?.Invoke(this, EventArgs.Empty);            
        }
        else if (Input.GetKeyDown("space") && playerMovement.grounded)
        {
            OnPlayerJump?.Invoke(this, EventArgs.Empty);
        }
        else if (Mathf.Abs(playerMovement.inputX) > Mathf.Epsilon)
        {
            OnPlayerMove?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnPlayerIdle?.Invoke(this, EventArgs.Empty);
        }
    }
}

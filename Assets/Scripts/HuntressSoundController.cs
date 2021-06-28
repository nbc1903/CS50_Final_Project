using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntressSoundController : MonoBehaviour
{
    public AudioClip hurtSound;
    public AudioClip attackSound;
    public AudioClip hpUpSound;
    public AudioClip spUpSound;

    public AudioClip spSound;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip shieldSound;

    AudioSource aSource;

    PlayerMovementController playerMovement;
    HuntressStateController huntressState;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
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
                aSource.PlayOneShot(deathSound);
            }
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("j"))
        {
            aSource.PlayOneShot(attackSound);
        }
        else if (Input.GetKeyDown("space") && playerMovement.grounded)
        {
            aSource.PlayOneShot(jumpSound);
        }
        else if (Mathf.Abs(playerMovement.inputX) > Mathf.Epsilon)
        {
        }
        else
        {
        }
    }
}

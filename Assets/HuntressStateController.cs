using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntressStateController : MonoBehaviour
{

    public int maxHp = 10;
    public int maxSP = 10;
    public int hp = 10;
    public int sp = 0;

    [HideInInspector] public bool dead = false;

    PlayerMovementController playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hp <= 0)
        {
            if (!dead)
            {
                if (playerMovement.grounded)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
                dead = true;
            }
            else
            {
                if (playerMovement.grounded)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
            }
        }

    }

    private bool AddHP(int cant)
    {
        bool usado = false;
        if (hp >= maxHp)
        {
            usado = false;
        }
        else if ((hp + cant) >= maxHp)
        {
            hp = maxHp;
            usado = true;
        }
        else if ((hp + cant) <= maxHp)
        {
            hp += cant;
            usado = true;
        }
        return usado;
    }

    private bool AddSP(int cant)
    {
        bool usado = false;
        if (sp >= maxSP)
        {
            usado = false;
        }
        else if ((sp + cant) >= maxSP)
        {
            sp = maxSP;
            usado = true;
        }
        else if ((sp + cant) <= maxSP)
        {
            sp += cant;
            usado = true;
        }
        return usado;
    }

    public void Hurt(int direction)
    {
        if (!dead)
            hp--;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntressStateController : MonoBehaviour
{

    public int maxHp = 10;
    public int maxSP = 10;
    public int hp = 10;
    public int sp = 0;

    public float invincibilityTime = 5f;

    public bool isInvincible = false;

    private List<Collider2D> collisions;


    [HideInInspector] public bool dead = false;

    PlayerMovementController playerMovement;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collisions = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkCollisions();
        if (hp > 0)
        {
            if (isInvincible)
            {
                //InvokeRepeating("Blink", 0, 0.4f);
                /*
                _t += Time.deltaTime;
                StartCoroutine(InvincibleAnimation());  
                Debug.Log(spriteRenderer.color.a);

                if (_t > invincibilityTime)
                {
                    StopAllCoroutines();
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                    isInvincible = false;
                    _t = 0.0f;
                }
                */
            }
        }
        else
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

    public void Hurt(int dmg)
    {
        if (!dead)
        {
            if (!isInvincible)
            {
                hp -= dmg;
                StartCoroutine(InvincibleAnimation(5, 0.1f));
                isInvincible = true;
            }
        }
    }

    IEnumerator InvincibleAnimation(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {

            //toggle renderer
            spriteRenderer.enabled = !spriteRenderer.enabled;

            //wait for a bit
            yield return new WaitForSeconds(seconds);
        }

        //make sure renderer is enabled when we exit
        spriteRenderer.enabled = true;
        isInvincible = false;
    }


    private void checkCollisions()
    {
        foreach (Collider2D collision in collisions)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.layer == 10)
            {
                if (!isInvincible)
                {
                    Hurt(1);
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisions.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collisions.Remove(collision);
    }

}

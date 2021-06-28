using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntressController : MonoBehaviour
{
    private int attackCount = 0;
    private bool sliding = false;
    [HideInInspector] public bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAttackingEnemy();
        attacking = false;
    }

    private void CheckIfAttackingEnemy()
    {
        /*
        if (rightSensor.State())
        {
            foreach (Collider2D collider in rightSensor.colliders.ToArray())
            {
                if (collider)
                {
                    if (collider.tag == "Enemy")
                    {
                        if (attacking)
                        {
                            if (facingDirection == 1)
                            {
                                if (attackCount < 1)
                                {
                                    bool golpeado = collider.GetComponent<ControlEnemigo>().GolpeJugador();
                                    aSource.PlayOneShot(attackSound);
                                    //Debug.Log("Golpeando enemigo a la derecha: " + golpeado);
                                    attackCount++;
                                }
                            }
                        }
                    }

                    else if (collider.tag == "Enemy2")
                    {
                        if (attacking)
                        {
                            if (facingDirection == 1)
                            {
                                if (attackCount < 1)
                                {
                                    bool golpeado = collider.GetComponent<ControlEnemigo2>().GolpeJugador();
                                    aSource.PlayOneShot(attackSound);
                                    //Debug.Log("Golpeando enemigo a la derecha: " + golpeado);
                                    attackCount++;
                                }
                            }
                        }
                    }
                    else if (collider.gameObject.tag == "Box")
                    {
                        if (lifting)
                        {
                            if (firstLift)
                            {
                                collider.attachedRigidbody.isKinematic = true;
                                collider.transform.position = this.transform.Find("LiftingPosition").position;
                                collider.transform.parent = this.transform.Find("LiftingPosition").transform;
                                collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                                firstLift = false;
                                liftingBox = collider;
                            }
                        }
                    }
                }
            }
        }

        if (leftSensor.State())
        {
            foreach (Collider2D collider in leftSensor.colliders.ToArray())
            {
                if (collider)
                {
                    if (collider.tag == "Enemy")
                    {
                        if (attacking)
                        {
                            if (facingDirection == -1)
                            {
                                if (attackCount < 1)
                                {
                                    bool golpeado = collider.GetComponent<ControlEnemigo>().GolpeJugador();
                                    aSource.PlayOneShot(attackSound);
                                    //Debug.Log("Golpeando enemigo a la izquierda: " + golpeado);
                                    attackCount++;
                                }
                            }
                        }
                    }

                    else if (collider.tag == "Enemy2")
                    {
                        if (attacking)
                        {
                            if (facingDirection == -1)
                            {
                                if (attackCount < 1)
                                {
                                    bool golpeado = collider.GetComponent<ControlEnemigo2>().GolpeJugador();
                                    aSource.PlayOneShot(attackSound);
                                    //Debug.Log("Golpeando enemigo a la izquierda: " + golpeado);
                                    attackCount++;
                                }
                            }
                        }
                    }
                    else if (collider.gameObject.tag == "Box")
                    {
                        if (lifting)
                        {
                            if (firstLift)
                            {
                                collider.attachedRigidbody.isKinematic = true;
                                collider.transform.position = this.transform.Find("LiftingPosition").position;
                                collider.transform.parent = this.transform.Find("LiftingPosition").transform;
                                collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                                firstLift = false;
                                liftingBox = collider;
                            }
                        }

                    }
                }
            }
        }
        */
    }



}

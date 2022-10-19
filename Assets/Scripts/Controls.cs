using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public float speed = 7f; //Variable Speed pour vitesse du joueur

    //public float _dash = 20f; //Variable dash pour dash du joueur
    //public float _dash = 20f; //Variable dash pour dash du joueur

    private Rigidbody2D rb; // def rigidbody en rb

    private int direction;

    private SpriteRenderer monSprite; // S�lection des sprites
    private Animator anim; // s�l�ctions de l'animateur

    public float dashSpeed = 10f;
    public float dashLenght = 5f;
    private float dashTarget;
    public bool dashing;
    private bool waitForDash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //def rigidbody en rb
        monSprite = GetComponent<SpriteRenderer>(); //def des sprites
        anim = GetComponent<Animator>(); //def de l'animation
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * speed);

        if (Input.GetAxis("Horizontal") < 0)
        { //regard � gauche
            //monSprite.flipX = true;
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            direction = -1;
            //attackPosition = (Vector2)transform.position + new Vector2(attackPositionSave.x, attackPositionSave.y);
        }
        if (Input.GetAxis("Horizontal") > 0)
        { //regard � droite
            //monSprite.flipX = false;
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            direction = 1;
            //attackPosition = (Vector2)transform.position + new Vector2(-attackPositionSave.x, attackPositionSave.y);
            //attackPoint = (Vector2)transform.position + new Vector2(-attackPointSave.x, attackPointSave.y);
        }

        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);

        if (Input.GetButtonDown("Jump") && !dashing && !waitForDash)
        {
            //Mettre ici une ligne pour lancer l'animation de Dash
            anim.SetTrigger("dash");
            anim.SetBool("dashing", true);
            dashing = true;
            if (GetComponent<Collider>() != null)
            {
                //collider.size = new Vector2(2.932996f, 0.639156f);
                //collider.direction = 0;
            }
            if (direction == 1)
            {
                dashTarget = transform.position.x + dashLenght;
            }
            if (direction == -1)
            {
                dashTarget = transform.position.x - dashLenght;
            }
        }

        if (dashing)
        {
            if (direction == 1)
            {
                rb.velocity = new Vector2(dashSpeed, 0);
                if (transform.position.x > dashTarget)
                {
                    anim.SetBool("dashing", false);
                    dashing = false;
                    StartCoroutine("dashingRoutine");
                    waitForDash = true;
                    if (GetComponent<Collider>() != null)
                    {
                        //collider.size = new Vector2(0.639156f, 2.932996f);
                        //collider.direction = 1;
                    }
                }

            }
            if (direction == -1)
            {
                rb.velocity = new Vector2(-dashSpeed, 0);
                if (transform.position.x < dashTarget)
                {
                    anim.SetBool("dashing", false);
                    dashing = false;
                    StartCoroutine("dashingRoutine");
                    waitForDash = true;
                    if (GetComponent<Collider>() != null)
                    {
                        //collider.size = new Vector2(0.639156f, 2.932996f);
                        //collider.direction = 1;
                    }
                }
            }
        }
    }

    IEnumerator dashingRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        waitForDash = false;
    }
}

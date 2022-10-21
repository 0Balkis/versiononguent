using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //METTRE CE SCRIPT SUR LES ENNEMIS

    private Rigidbody2D rb;                                             // Le rigidbody de l'ennemi

    public float direction = 1f;                                       // Direction vers laquelle l'ennemi se dirige (1 = droite, -1 = gauche)
    private SpriteRenderer skin;                                        // Le sprite de l'ennemi, pour qu'on puisse le retourner quand il change de direction

    private Animator anim;
    private Collider2D monCollider;

    private RaycastHit2D hit;


    /////////////////////////////////////////////////////////

    [SerializeField]
    Transform player;

    public float agroRangeX = 50f;

    public float agroRangeY1 = 50f;

    public float agroRangeY2 = 50f;

    [SerializeField]
    float moveSpeed = 30f;

    private float distToPlayer;
    private float highToPlayer;

    //public GameObject attackZone;

    // Au lancement du jeu, on enregistre le rigidbody et le sprite de l'ennemi
    // On transforme aussi les valeurs de limite Droite et Gauche en coordonnées réelles
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skin = GetComponent<SpriteRenderer>();
        monCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();


        //rb.freezeRotation = true;
        //Physics2D.queriesStartInColliders = false;
        //Physics2D.queriesHitTriggers = false;

    }


    void Update()
    {


        //distance to player
        //distToPlayer = Vector2.Distance(transform.position, player.position);
        distToPlayer = transform.position.x - player.transform.position.x;
        print("distToPlayer is : " + distToPlayer);

        highToPlayer = (transform.position.y - player.transform.position.y);
        //print("highToPlayer is : " + highToPlayer);
        //Debug.Log("verif");
        chase();

        if (distToPlayer < agroRangeX && agroRangeY1 >= highToPlayer && highToPlayer >= agroRangeY2)
        {
            chase();
            Debug.Log("chasing");
        }
        else
        {
            stopChase();
        }




        //anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        //anim.SetFloat("velocityY", rb.velocity.y);
        //anim.SetBool("isGrounded", isGrounded);
    }

    private void chase()
    {
        if (transform.position.x < player.position.x)
        {
            //Droite left go
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            direction = 1;
        }
        else if (transform.position.x > player.position.x)
        {
            //Gauche right go
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            direction = -1;
        }

        //if (transform.position.y < player.position.y)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        //}
        //else if (transform.position.y > player.position.y)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
        //}

    }

    void stopChase()
    {
        rb.velocity = new Vector2(0, 0);
        //rb.velocity = Vector2.zero;
    }


}

using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour
{
    public int vie = 3;
    public bool collided;
    public GameObject contact;
    private CapsuleCollider2D col;
    private SpriteRenderer sprite;

    private Rigidbody2D rb;
    public int vieMax;
    public bool invincible;
    private Animator anim;

    public GameObject lifebar;
    private Animator lifeBarAnim;
    private int direction;

    public GameObject screenGameOver;

    //public GameObject detecteurBoss;

    private void Start()
    {
        vieMax = vie;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lifeBarAnim = lifebar.GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        lifeBarAnim.SetInteger("vie", vie);
        if (vie < 1)
        {
            lifebar.SetActive(false);
                //col.enabled = false;
                Invoke("GameOverScreen", 0.5f);
                
            }
            
        

        if (collided)
        {
            collided = false;
            if (!invincible)
            {
                vie = vie - 1;
                //Debug.Log("vie restante :" + vie);
                invincible = true;
                Invoke("resetInvincible", 2.5f);
                if (vie > 0)
                {
                    StartCoroutine(InvicibilityFlash());
                }

            }
        }

    }

    private void GameOverScreen()
    {
       screenGameOver.gameObject.SetActive(true);
       Time.timeScale = 0f;
    }




    void OnCollisionEnter2D(Collision2D truc)
    {
        contact = truc.gameObject;
        if (contact.tag == "Enemy" && !invincible)
        {
            collided = true;
        }
    }


    void resetInvincible()
    {
        invincible = false;
    }

    public IEnumerator InvicibilityFlash()
    {
        while (invincible)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
            yield return new WaitForSeconds(0.2f);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}


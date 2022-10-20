using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    // Script a mettre sur votre projectile que vous allez tirer depuis le script attackDIST
    // IL doit y avoir un TRIGGER sur l'objet et un rigidbody

    public int degats = 1;          // Les dégats du projectile
    public float lifeTime = 5.0f;   // Le temps maximal que vivra le projectile (pour être sur qu'il se détruise au bout d'un moment)

    private void Start() {
        Destroy(gameObject, lifeTime);
    }

    // La fonction OnTriggerEnter s'enclenche quand votre Trigger touche un autre collider/trigger
    void OnTriggerEnter2D(Collider2D truc) {
        if (truc.tag == "Ennemi") {                 // Si le truc qu'on touche a le tag "Ennemi"
            truc.SendMessage("takeDamage", degats); // On cherche sur lui une fonction qui s'appel "takeDamage", et on la lance en lui donnant le nombre de dégat qu'on fait
            Destroy(gameObject);                    // Enfin on détruit le projectile
        }

        else if (!truc.isTrigger && truc.tag != "Player") {     // Sinon si on touche un mur (un collider qui n'est PAS un trigger) et que ce n'est pas le joueur
            Destroy(gameObject);        // On détruit simplement le projectile
        }
    }
}

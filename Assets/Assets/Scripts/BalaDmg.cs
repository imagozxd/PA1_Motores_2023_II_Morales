using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaDmg : MonoBehaviour
{    
    public int damageAmount = 10; // Cantidad de daño infligido por la bala

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy1") || other.CompareTag("Enemy2"))
        {
            // Si la bala colisiona con un enemigo, reduce su vida
            HealthBarController enemyHealth = other.GetComponent<HealthBarController>();

            if (enemyHealth != null)
            {
                enemyHealth.ReduceHealth(damageAmount); // Reduce la vida del enemigo
                Fade.Instance.FadeHit();
            }
            Debug.Log("impacta");

            // Destruye la bala al impactar con un enemigo
            Destroy(gameObject);
        }
    }
}

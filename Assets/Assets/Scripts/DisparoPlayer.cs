using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPlayer : MonoBehaviour
{    
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto de origen del disparo
    public float bulletSpeed = 10f; // Velocidad del proyectil

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detecta el clic izquierdo del mouse
        {
            Shoot(); // Llama a la funci�n de disparo
        }
    }

    void Shoot()
    {
        // Crea un proyectil desde el prefab y configura su posici�n y rotaci�n
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Obtiene la direcci�n hacia el puntero del mouse desde la posici�n actual del jugador
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        // Configura la velocidad del proyectil
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        // Destruye el proyectil despu�s de un tiempo para evitar que se acumulen en la escena
        Destroy(bullet, 2f);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreController : MonoBehaviour
{
    public GameObject Player;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public int speed;
    private Vector2 initialPosition;
    private bool isPlayerInsideZone = false;
    private float timeSinceLastShot = 0f;
    public float shootInterval = 2f; // Intervalo de disparo
    private float projectileSpeed = 10f; // Velocidad del proyectil

    [SerializeField] private int damageAmount = 20; // Ajusta la cantidad de daño que inflige la bala del Enemigo 2 al jugador


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInsideZone)
        {
            // Moverse hacia el jugador
            Vector2 directionToPlayer = Player.transform.position - transform.position;
            directionToPlayer.Normalize();
            transform.Translate(directionToPlayer * speed * Time.deltaTime);

            // Disparar cada 2 segundos
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= shootInterval)
            {
                Shoot();
                timeSinceLastShot = 0f; // Reiniciar el temporizador
            }
        }
    }

    // Este método se llama cuando un objeto entra en el collider del Ogre
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideZone = true;
        }
    }

    // Este método se llama cuando un objeto sale del collider del Ogre
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideZone = false;            
        }
    }
    void Shoot()
    {
        // Crea un proyectil y configura su dirección y velocidad
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 directionToPlayer = (Player.transform.position - firePoint.position).normalized;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = directionToPlayer * projectileSpeed; // Ajusta la velocidad del proyectil aquí
    }

}

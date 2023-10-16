using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    public LayerMask layermask;

    [SerializeField] private float chaseSpeedModifier = 10f; // Velocidad aumentada cuando detecta al jugador
    [SerializeField] private float raycastDistance = 10f; // Distancia máxima del raycast para detectar al jugador
    private bool isChasing = false; // Indicador de si está persiguiendo al jugador

    [SerializeField] private int damageAmount = 10; // Ajusta la cantidad de daño que inflige el Enemigo 1 al jugador

    public static PatrolMovementController Instance { get; private set; }

    /*private void Awake()
    {
        if (Instance!= null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }*/

    private void Start() {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }

    private void Update() {
        if (isChasing)
        {
            // Si está persiguiendo al jugador, usa la velocidad aumentada
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * chaseSpeedModifier;
        }
        else
        {
            // Si no está persiguiendo al jugador, usa la velocidad normal
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
        }

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
        //AnimatorController.Instance.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        // Verifica si el jugador está en línea de visión
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentPositionTarget.position - transform.position, raycastDistance, layermask);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // Si el raycast golpea al jugador, cambia al estado de persecución
            isChasing = true;
        }
        else
        {
            // Si el raycast no golpea al jugador, vuelve al estado normal
            isChasing = false;
        }

        CheckNewPoint();

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
        //AnimatorController.Instance.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
    }
    private void FixedUpdate()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 100f);
    }

    private void CheckNewPoint(){
        
        if (Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25){
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length? 0: patrolPos+1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
            CheckFlip(myRBD2.velocity.x);
            
        }
        
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Colisión con el jugador, reduce su vida
            HealthBarController playerHealth = collision.gameObject.GetComponent<HealthBarController>();
            if (playerHealth != null)
            {
                playerHealth.ReduceHealth(damageAmount); // Ajusta damageAmount a la cantidad de daño que el Enemigo 1 inflige al jugador
            }
        }
    }
}

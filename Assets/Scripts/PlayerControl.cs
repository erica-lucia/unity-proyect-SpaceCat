using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour 
{
    // Fuerza de salto
    public float jumpPower = 6f;
    public float runVelocity= 2f;
    
    // Referencia al Rigidbody2D del jugador
    private Rigidbody2D rigidBody;

    // Capa que define el suelo
    public LayerMask groundLayer;

    // Referencia al Animator
    private Animator animator;

    // Variables constantes para los parámetros del Animator
    private const string STATE_ALIVE = "isALive";
    private const string STATE_ON_THE_GROUND = "isOnTheGround";

    private const string STATE_ON_THE_JUMP= "isJumpingUp";

    private void Awake()
    {
        // Obtiene los componentes necesarios
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start() 
    {
        // Configuración inicial de las variables del Animator
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);
    }

    private void Update() 
    {
        // Detecta si se presiona espacio o clic izquierdo del ratón para saltar
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            PerformJump();
        }

        // Actualiza el estado de si está en el suelo
        animator.SetBool(STATE_ON_THE_GROUND, IsGrounded());

        // Línea roja para depurar el rayo que detecta el suelo
        Debug.DrawRay(transform.position, Vector2.down * 1.2f, Color.red);
    }

    //hacer que el personaje camine
    private void FixedUpdate()
    {
    float currentSpeed = rigidBody.velocity.x;

    if (currentSpeed < runVelocity)
    {
        Vector2 newVelocity = new Vector2(runVelocity, rigidBody.velocity.y);
        rigidBody.velocity = newVelocity;
    }
    }


    // Función que realiza el salto
    void PerformJump()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);


             // Actualiza el estado del Animator
            animator.SetBool("isOnTheGround", false);
            animator.SetBool("isJumpingUp", true);
        }
    }

    // Comprueba si el personaje está tocando el suelo
    bool IsGrounded()
    {
    // Lanza un rayo hacia abajo para detectar el suelo
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, groundLayer);

    // Devuelve directamente si el rayo detecta un collider
    return hit.collider != null;
    }

}

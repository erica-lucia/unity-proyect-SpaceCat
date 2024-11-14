using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    // Fuerza de salto
    public float jumpPower = 6f;
    
    // Referencia al Rigidbody2D del jugador
    private Rigidbody2D rb;

    // Capa que define el suelo
    public LayerMask groundLayer;

    void Awake()
    {
        // Obtiene el componente Rigidbody2D al que está adjunto el script
        rb = GetComponent<Rigidbody2D>();
    }

    // Inicialización (puede dejarse vacío)
    void Start() 
    {
    }

    // Se llama una vez por fotograma
    void Update() 
    {
        // Comprueba si se presiona espacio o el clic izquierdo del ratón
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            PerformJump();
        }

        // Dibuja una línea roja debajo del jugador para visualizar el área de detección de suelo
        Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.red);
    }

    // Función que realiza el salto
    void PerformJump()
    {
        // Comprueba si está en el suelo antes de saltar
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    // Comprueba si el personaje está en el suelo
    bool IsGrounded()
    {
        // Lanza un rayo hacia abajo para detectar el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);
        
        // Retorna true si el rayo detecta una colisión con el suelo
        return hit.collider != null;
    }
}

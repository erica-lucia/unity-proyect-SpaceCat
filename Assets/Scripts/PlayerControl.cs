using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour 
{
    public float jumpPower = 6f;
    public float runVelocity = 2f;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private Vector3 startPosition;

    public LayerMask groundLayer;

    private const string STATE_ALIVE = "isALive";
    private const string STATE_ON_THE_GROUND = "isOnTheGround";

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start() 
    {
        startPosition = this.transform.position;
    }

    public void StartGame()
    {
        // Configuración inicial
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        RestartPlayer();
    }

    private void Update() 
    {
        if (Input.GetButtonDown("Jump"))
        {
            PerformJump();
        }

        animator.SetBool(STATE_ON_THE_GROUND, IsGrounded());
        Debug.DrawRay(transform.position, Vector2.down * 1.2f, Color.red);
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.activePhase == GamePhase.Playing)
        {
            float targetSpeed = Mathf.Max(rigidBody.velocity.x, runVelocity);
            rigidBody.velocity = new Vector2(targetSpeed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }

    void PerformJump()
    {
        if (GameManager.sharedInstance.activePhase == GamePhase.Playing && IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, groundLayer);
        return hit.collider != null;
    }

    public void Die()
    {
        Debug.Log("Método Die() ejecutado. Cambiando a GameOver...");
        animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }

    public void RestartPlayer()
    {
        Debug.Log("Reiniciando al jugador...");
        transform.position = startPosition;
        rigidBody.velocity = Vector2.zero;

        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
    }
}

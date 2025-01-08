using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float jumpPower = 3f; // Ajusta según la escala del mundo
    public float runVelocity = 2f; // Velocidad al correr

    private Rigidbody2D rigidBody;
    private Animator animator;
    private Vector3 startPosition;

    public LayerMask groundLayer;

    private const string STATE_ALIVE = "isAlive";
    private const string STATE_ON_THE_GROUND = "isOnTheGround"; 
    private int healthPoints, manaPoints;


    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15,
        MAX_HEALTH = 200, MAX_MANA = 30,
        MIN_HEALTH = 10, MIN_MANA = 0; 

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.5f;




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
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

        Invoke("RestartPosition", 0.2f);//0.2
    }

    public void RestartPlayer()
    {
        transform.position = startPosition;
        rigidBody.velocity = Vector2.zero; 

        GameObject mainCamera=GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();

        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump(false);
        } 
        if (Input.GetButtonDown("Superjump")){
            Jump(true);
        }


        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        Debug.DrawRay(this.transform.position, Vector2.down * 1.5f, Color.red);
    }

    void FixedUpdate()
    {
        if (GameManager.sharedInstance.activePhase == GamePhase.Playing)
        {
            rigidBody.velocity = new Vector2(runVelocity, rigidBody.velocity.y);
        }
    }

    bool IsTouchingTheGround()
    {
        Vector3 raycastOrigin = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        Debug.DrawRay(raycastOrigin, Vector2.down * 1.5f, Color.red);
        return Physics2D.Raycast(raycastOrigin, Vector2.down, 1.5f, groundLayer);
    }

    void Jump(bool superjump)
    { 
        float jumpForceFactor = jumpPower;

        if(superjump&&manaPoints>=SUPERJUMP_COST){
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }

        if (IsTouchingTheGround())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
            Debug.Log(superjump ? "El personaje realizó un super salto." : "El personaje realizó un salto normal.");
        }
    }

    public void Die()
    {
        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore",0f);
        if(travelledDistance > previousMaxDistance){
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }
        
        
        if (animator == null)
        {
            Debug.LogError("Animator no está asignado al objeto Player.");
            return;
        }

    // Elimina esta verificación si estás seguro de que el parámetro existe.
    // if (!animator.HasParameter(STATE_ALIVE)) 
    // {
    //    Debug.LogError($"El parámetro '{STATE_ALIVE}' no existe en el Animator.");
    //    return;
    // }

        animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    } 
    public void CollectHealth(int points){
        this.healthPoints += points;
        if(this.healthPoints >= MAX_HEALTH){
            this.healthPoints = MAX_HEALTH;
        }
    }

    public void CollectMana(int points){
        this.manaPoints += points;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth(){
        return healthPoints;
    }

    public int GetMana(){
        return manaPoints;
    } 
    public float GetTravelledDistance(){
        return this.transform.position.x - startPosition.x;
    }
}

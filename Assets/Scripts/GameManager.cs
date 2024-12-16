using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Crear los enumerados
public enum GamePhase {
    MainMenu,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour {

    public GamePhase activePhase = GamePhase.MainMenu;
    public static GameManager sharedInstance; //un singleton, instanciacompartida

    private PlayerControl control;

    
    public void Awake(){ 
    //inicializamos el shareInstance
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    
    
    // Called when the script is first initialized
    private void Start () {
        control= GameObject.Find("Player").GetComponent<PlayerControl>();
        
    }

    

    // Called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
        if (activePhase == GamePhase.GameOver)
        {
            RestartGame();
        }
        else if (activePhase == GamePhase.MainMenu)
        {
            BeginGame();
        }
    }
    }


        
    

    //Metodo para iniciar la partida( Logic to start the game)
    public void BeginGame() {
       ChangePhase(GamePhase.Playing);
    } 

    //Metodo para finalizar la partida(Logic for when the game ends)
    public void GameOver()
    {
        Debug.Log("EndGame() llamado. Cambiando a GamePhase.GameOver...");
        ChangePhase(GamePhase.GameOver);
    }



    //Metodo para volver al menu(Logic to return to the menu)
    public void ReturnToMainMenu() {
        ChangePhase(GamePhase.MainMenu);
         
    }

    //unico//
        public void RestartGame()
    {
        Debug.Log("Reiniciando el juego...");
        ChangePhase(GamePhase.Playing);

        // Reinicia la posición del jugador (asume que tienes acceso al jugador)
        PlayerControl player = FindObjectOfType<PlayerControl>();
        if (player != null)
        {
        player.RestartPlayer();
        }
    }




    private void ChangePhase(GamePhase nextPhase) {
        if (nextPhase == GamePhase.MainMenu) {
            // TODO: colocar la logica del menu
        } else if (nextPhase == GamePhase.Playing) {
            // TODO: Preparar la escena para jugar
        } else if (nextPhase == GamePhase.GameOver) {
            // TODO: preparar el juego para el gameover
        }

        this.activePhase = nextPhase;
    }
}


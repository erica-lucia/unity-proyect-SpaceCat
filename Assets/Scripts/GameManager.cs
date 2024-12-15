using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Crear los enumerados
public enum GamePhase {
    MainMenu,
    Playing,
    Ended
}

public class GameManager : MonoBehaviour {

    public GamePhase activePhase = GamePhase.MainMenu;
    public static GameManager sharedInstance; //un singleton, instanciacompartida

    
    public void Awake(){ 
    //inicializamos el shareInstance
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    
    
    // Called when the script is first initialized
    private void Start () {
        
    }

    

    // Called once per frame
    private void Update () {
        if(Input.GetKeyDown(KeyCode.S)){
            BeginGame();
        }
	}
        
    

    //Metodo para iniciar la partida( Logic to start the game)
    public void BeginGame() {
       ChangePhase(GamePhase.Playing);
    } 

    //Metodo para finalizar la partida(Logic for when the game ends)
    public void EndGame() {
        ChangePhase(GamePhase.Ended);
         
    }


    //Metodo para volver al menu(Logic to return to the menu)
    public void ReturnToMainMenu() {
        ChangePhase(GamePhase.MainMenu);
         
    }



    private void ChangePhase(GamePhase nextPhase) {
        if (nextPhase == GamePhase.MainMenu) {
            // TODO: colocar la logica del menu
        } else if (nextPhase == GamePhase.Playing) {
            // TODO: Preparar la escena para jugar
        } else if (nextPhase == GamePhase.Ended) {
            // TODO: preparar el juego para el gameover
        }

        this.activePhase = nextPhase;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    MainMenu,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public GamePhase activePhase = GamePhase.MainMenu;
    public static GameManager sharedInstance;

    private PlayerControl playerControl;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    private void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    private void Update()
    {
        // Detectar inicio o reinicio del juego
        if (Input.GetButtonDown("Submit") && activePhase != GamePhase.Playing)
        {
            if (activePhase == GamePhase.GameOver)
            {
                RestartGame();
            }
            else if (activePhase == GamePhase.MainMenu)
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        Debug.Log("Iniciando el juego...");
        ChangePhase(GamePhase.Playing);
    }

    public void GameOver()
    {
        Debug.Log("Fin del juego. Cambiando a GameOver...");
        ChangePhase(GamePhase.GameOver);
    }

    public void RestartGame()
    {
        Debug.Log("Reiniciando el juego...");
        ChangePhase(GamePhase.Playing);

        // Reiniciar el jugador
        if (playerControl != null)
        {
            playerControl.RestartPlayer();
        }
    }

    private void ChangePhase(GamePhase nextPhase)
    {
        Debug.Log($"Cambiando a la fase: {nextPhase}");
        activePhase = nextPhase;

        switch (nextPhase)
        {
            case GamePhase.MainMenu:
                Debug.Log("Preparando menú principal...");
                break;
            case GamePhase.Playing:
                Debug.Log("Juego iniciado...");
                break;
            case GamePhase.GameOver:
                Debug.Log("Preparando Game Over...");
                break;
        }
    }
}

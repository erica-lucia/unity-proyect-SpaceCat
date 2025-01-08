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
    public int collectedObject = 0;
    private LevelManager levelManager;
    private MenuManager menuManager;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        playerControl = GameObject.Find("Player")?.GetComponent<PlayerControl>();
        if (playerControl == null)
        {
            Debug.LogError("PlayerControl no encontrado.");
        }

        levelManager = LevelManager.sharedInstance;
        menuManager = MenuManager.sharedInstance;

        if (levelManager == null || menuManager == null)
        {
            Debug.LogError("Faltan referencias a LevelManager o MenuManager.");
        }
    }

    private void Update()
    {
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
        ChangePhase(GamePhase.Playing);
    }

    public void GameOver()
    {
        ChangePhase(GamePhase.GameOver);
    }

    public void RestartGame()
    {
        ChangePhase(GamePhase.Playing);
        playerControl?.RestartPlayer();
    }

    private void ChangePhase(GamePhase nextPhase)
    {
        activePhase = nextPhase;

        switch (nextPhase)
        {
            case GamePhase.MainMenu:
                menuManager?.ShowMainMenu();
                menuManager?.HideGameMenu();
                menuManager?.HideGameOverMenu();
                break;

            case GamePhase.Playing:
                levelManager?.RemoveAllLevelBlocks();
                levelManager?.GenerateInitialBlocks();
                playerControl?.StartGame();
                menuManager?.HideMainMenu();
                menuManager?.ShowGameMenu();
                break;

            case GamePhase.GameOver:
                menuManager?.ShowGameOverMenu();
                menuManager?.HideGameMenu();
                menuManager?.HideMainMenu();
                break;
        }
    } 
    public void CollectObject(Collectable collectable){
        collectedObject += collectable.value;
    }
}

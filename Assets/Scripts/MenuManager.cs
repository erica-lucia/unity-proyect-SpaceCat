using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;
    public Canvas menuCanvas;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    public void ShowMainMenu()
    {
        Debug.Log("Mostrando el menú principal.");
        menuCanvas.enabled = true;
    }

    public void HideMainMenu()
    {
        Debug.Log("Ocultando el menú principal.");
        menuCanvas.enabled = false;
    }

    public void ShowGameMenu()
    {
        Debug.Log("Mostrando menú de juego (GameMenu).");
        // Implementa la lógica para mostrar el menú del juego aquí
    }

    public void HideGameMenu()
    {
        Debug.Log("Ocultando menú de juego (GameMenu).");
        // Implementa la lógica para ocultar el menú del juego aquí
    }

    public void ShowGameOverMenu()
    {
        Debug.Log("Mostrando menú de Game Over.");
        // Implementa la lógica para mostrar el menú de Game Over aquí
    }

    public void HideGameOverMenu()
    {
        Debug.Log("Ocultando menú de Game Over.");
        // Implementa la lógica para ocultar el menú de Game Over aquí
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

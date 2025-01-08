using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameView : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText; 

    private PlayerControl controller; 



    void Start () {
        controller = GameObject.Find("Player").GetComponent<PlayerControl>();
	}

    

    void Update()
    { 
        if(GameManager.sharedInstance.activePhase == GamePhase.Playing){
            int coins = GameManager.sharedInstance.collectedObject;
            float score = controller.GetTravelledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxscore", 0);

            coinsText.text = coins.ToString();
            scoreText.text = "Score: " + score.ToString("f1");
            maxScoreText.text = "MaxScore: " + maxScore.ToString("f1");
        }
	
        // Verifica si el estado del juego es Playing
        //if (GameManager.sharedInstance.activePhase == GamePhase.Playing)
        //{
            //int coins = GetCoins(); // Método para obtener monedas
            //float score = GetScore(); // Método para obtener puntaje actual
            //float maxScore = GetMaxScore(); // Método para obtener puntaje máximo

            // Actualiza los textos en pantalla
            //coinsText.text = coins.ToString();
            //scoreText.text = "Score: " + score.ToString("f1");
            //maxScoreText.text = "MaxScore: " + maxScore.ToString("f1");
        //}
    }

    // Métodos temporales para simular valores
    //private int GetCoins()
    //{
        // Implementa la lógica para obtener monedas aquí
       // return 999;
    //}

    //private float GetScore()
    //{
        // Implementa la lógica para obtener el puntaje actual aquí
        //return 12345.67f;
    //}

    //private float GetMaxScore()
    //{
        // Implementa la lógica para obtener el puntaje máximo aquí
       // return 99999.99f;
    //}
}

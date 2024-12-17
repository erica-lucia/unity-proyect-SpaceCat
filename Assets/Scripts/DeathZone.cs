using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Detecta cuando un objeto entra en la zona de muerte
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            Debug.Log("DeathZone activada: Jugador detectado.");

            // Obtiene el componente PlayerControl y llama a Die
            PlayerControl player = collision.GetComponent<PlayerControl>();
            player?.Die(); // Llamada segura (null check implícito)
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    healthPotion,
    manaPotion,
    money
}

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.money;

    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;

    private bool hasBeenCollected = false;
    public int value = 1;

    private GameObject player; // Referencia al jugador

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        // Encuentra al jugador en la escena
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No se encontró un objeto con el tag 'Player' en la escena.");
        }
    }

    // Método para mostrar el objeto coleccionable
    void Show()
    {
        sprite.enabled = true;
        itemCollider.enabled = true;
        hasBeenCollected = false;
    }

    // Método para ocultar el objeto coleccionable
    void Hide()
    {
        sprite.enabled = false;
        itemCollider.enabled = false;
    }

    // Método para recoger el objeto coleccionable
    void Collect()
    {
        if (hasBeenCollected) return; // Evitar recoger el objeto más de una vez

        Hide();
        hasBeenCollected = true;

        switch (this.type)
        {
            case CollectableType.money:
                if (GameManager.sharedInstance != null)
                {
                    GameManager.sharedInstance.CollectObject(this);
                }
                else
                {
                    Debug.LogWarning("GameManager.sharedInstance no está definido.");
                }
                break;

            case CollectableType.healthPotion:
                if (player != null)
                {
                    var playerController = player.GetComponent<PlayerControl>();
                    if (playerController != null)
                    {
                        playerController.CollectHealth(this.value);
                    }
                    else
                    {
                        Debug.LogError("El jugador no tiene el componente 'PlayerController'.");
                    }
                }
                break;

            case CollectableType.manaPotion:
                if (player != null)
                {
                    var playerController = player.GetComponent<PlayerControl>();
                    if (playerController != null)
                    {
                        playerController.CollectMana(this.value);
                    }
                    else
                    {
                        Debug.LogError("El jugador no tiene el componente 'PlayerControl'.");
                    }
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }
}

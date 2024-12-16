using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Metodo que mientras un objeto esta en el interior de otro
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("El jugador ha entrado en la DeathZone.");
            PlayerControl control = collision.GetComponent<PlayerControl>();
            if (control != null)
            {
            control.Die();
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbeVida : MonoBehaviour
{
    [SerializeField] private int vidaRecupera=1;

 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            VidaJugador vida = collision.GetComponent<VidaJugador>();

            if (vida != null)
            {
                vida.Curar(vidaRecupera);
            }

            Destroy(gameObject);
        }
    }

}

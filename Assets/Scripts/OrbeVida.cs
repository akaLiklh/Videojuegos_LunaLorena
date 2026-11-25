using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbeVida : MonoBehaviour
{
    public int vidaRecupera=1;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
 
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

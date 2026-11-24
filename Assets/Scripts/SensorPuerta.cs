using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorPuerta : MonoBehaviour
{
    BoxCollider2D colision;
    public Transform puntoDestinoJugador;
    public Transform puntoDestinoCamara;

    // Start is called before the first frame update
    void Start()
    {
        colision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            collision.transform.position = puntoDestinoJugador.position;
            Camera.main.transform.position = puntoDestinoCamara.position;
        }

    }


}

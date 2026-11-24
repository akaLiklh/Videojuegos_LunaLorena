using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorPuerta : MonoBehaviour
{
    public GameObject destino;
    public GameObject origen;
    bool haCruzado;
    float temporizador;
    BoxCollider2D colision;
    public Transform puntoDestinoJugador;

    // Start is called before the first frame update
    void Start()
    {
        colision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (temporizador > 0)
        {
            temporizador -= Time.deltaTime;
        }
        else
        {
            colision.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (!haCruzado)
        {
            haCruzado = true;
            collision.transform.position = puntoDestinoJugador.position;
            Camera.main.transform.position = destino.transform.position;

        }
        else
        {
            haCruzado = false;
            collision.transform.position = puntoDestinoJugador.position;
            Camera.main.transform.position = origen.transform.position;

        }
        colision.enabled = false;
        temporizador = 2.0f;
       
    }


}

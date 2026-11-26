using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorFinal : MonoBehaviour
{
    float temporizador=2f;
    bool haCruzado;
    BoxCollider2D colision;


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
            FindObjectOfType<GestorVictoria>().MostrarVictoria();
        }
        else
        {
            haCruzado = false;


        }
        colision.enabled = false;
        temporizador = 2.0f;

    }

}

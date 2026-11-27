using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScriptPuerta : MonoBehaviour
{
    // Vector que reccoge todos los botones que hay que pulsar para abrir la puerta
    public GameObject[] listaBotones;

    [SerializeField] private float tiempoEspera = 2f;
    [SerializeField] private GameObject puertaCerrada;

    private bool puertaAbierta = false;

    // Update is called once per frame
    void Update()
    {
        // Cantidad de botones pulsados
        int contador = 0;
        foreach (GameObject boton in listaBotones)
        {
            IPulsable scr = boton.GetComponent<IPulsable>();
            if (scr.EstaPulsado())
            {
                contador++;
            }
        }

        TilemapRenderer dibujo = GetComponent<TilemapRenderer>();
        TilemapCollider2D colision = GetComponent<TilemapCollider2D>();

        if (contador == listaBotones.Length)
        {
            if (!puertaAbierta)
            {
                puertaAbierta = true;
                MensajePantalla.instancia.Mostrar("HAS CONSEGUIDO ENTRAR AL TEMPLO", tiempoEspera);
            }
            dibujo.enabled = false;
            colision.enabled = false;
        }
        else
        {
            dibujo.enabled = true;
            colision.enabled = true;
        }

    }
}

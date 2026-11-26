using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScriptPuerta : MonoBehaviour
{
    public GameObject[] listaBotones;
    [SerializeField] private float tiempoEspera = 2f;
    [SerializeField] private GameObject puertaCerrada;
    private bool puertaAbierta = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int contador = 0;
        foreach (GameObject go in listaBotones)
        {
            IPulsable scr = go.GetComponent<IPulsable>();
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

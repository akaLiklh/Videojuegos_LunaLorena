using UnityEngine;

public class BotonPulsar : MonoBehaviour, IPulsable
{
    public Color colorPulsado;
    public Color colorEnReposo;
    private bool jugadorCerca = false;
    private bool pulsado=false;


    // Update is called once per frame
    void Update()
    {
        if (jugadorCerca && !pulsado && Input.GetKeyDown(KeyCode.E))
        {
            pulsado = true;

            MensajePantalla.instancia.Mostrar("HAS ABIERTO LA PUERTA AL TEMPLO", 2f);

            SpriteRenderer dibujo = GetComponent<SpriteRenderer>();
            dibujo.color = colorPulsado;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            MensajePantalla.instancia.Mostrar("Pulsa E para activar", 1f);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }

    public bool EstaPulsado()
    {
        return pulsado;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

// Panel de la fábrica: con la tarjeta y pulsando E abre la puerta.
public class PanelAcceso : MonoBehaviour
{
    [SerializeField] private GameObject puertaCerrada;
    [SerializeField] private string escenaDestino = "";
    [SerializeField] private float tiempoEspera = 2f;

    private Inventario jugadorCerca;
    private bool activado = false;
    private float contador = 0;

    void Update()
    {
        if (activado)
        {
            if (contador > 0)
            {
                contador -= Time.deltaTime;

                if (contador <= 0 && escenaDestino != "")
                {
                    SceneManager.LoadScene(escenaDestino);
                }
            }
            return;
        }

        if (jugadorCerca != null && Input.GetKeyDown(KeyCode.E))
        {
            if (jugadorCerca.tieneTarjeta)
            {
                activado = true;
                contador = tiempoEspera;
                MensajePantalla.instancia.Mostrar("HAS CONSEGUIDO ENTRAR A LA FÁBRICA", tiempoEspera);

                if (puertaCerrada != null)
                {
                    puertaCerrada.SetActive(false);
                }
            }
            else
            {
                MensajePantalla.instancia.Mostrar("NECESITAS UNA TARJETA DE ACCESO", 2f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        Inventario inventario = otro.GetComponent<Inventario>();

        if (inventario != null)
        {
            jugadorCerca = inventario;

            if (!activado)
            {
                MensajePantalla.instancia.Mostrar("[E] USAR PANEL", 1.5f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D otro)
    {
        if (otro.GetComponent<Inventario>() != null)
        {
            jugadorCerca = null;
        }
    }
}
using UnityEngine;

// Al tocar la tarjeta de acceso, Melínoe la recoge y suma puntos
public class TarjetaAcceso : MonoBehaviour
{
    [SerializeField] private int puntos = 50;

    void OnTriggerEnter2D(Collider2D otro)
    {
        Inventario inventario = otro.GetComponent<Inventario>();

        if (inventario != null)
        {
            inventario.tieneTarjeta = true;

            if (GestorJuego.instancia != null)
            {
                GestorJuego.instancia.SumarPuntos(puntos);
            }

            if (MensajePantalla.instancia != null)
            {
                MensajePantalla.instancia.Mostrar("TARJETA DE ACCESO CONSEGUIDA", 2f);
            }

            Destroy(gameObject);
        }
    }
}
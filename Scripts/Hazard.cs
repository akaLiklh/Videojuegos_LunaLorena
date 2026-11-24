using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int danio = 1;

    void OnTriggerStay2D(Collider2D otro)
    {
        VidaJugador jugador = otro.GetComponent<VidaJugador>();

        if (jugador != null)
        {
            jugador.RecibirDanio(danio, transform.position);
        }
    }
}
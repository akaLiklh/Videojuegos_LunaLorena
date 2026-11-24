using UnityEngine;

// Guarda la puntuación de la partida y no se borra al cambiar de escena.
public class GestorJuego : MonoBehaviour
{
    public static GestorJuego instancia;

    private int puntos = 0;

    void Awake()
    {
        // Para q solo pueda haber uno
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
    }

    public int GetPuntos()
    {
        return puntos;
    }

    public void ReiniciarPuntos()
    {
        puntos = 0;
    }
}
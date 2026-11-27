using UnityEngine;

public class PlataformaMueve : MonoBehaviour
{
    public Transform[] puntos;
    public float velocidad = 2f;
    private int indiceActual = 0;
    private Rigidbody2D rb;
    private Vector2 posicionAnterior;
    public Vector2 velocidadPlataforma;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicionAnterior = rb.position;
    }

    void FixedUpdate()
    {
        Vector2 posicionActual = rb.position;

        // Calcula la velocidad real de la plataforma usando la posición actual y la anterior.
        velocidadPlataforma = (posicionActual - posicionAnterior) / Time.fixedDeltaTime;

        posicionAnterior = posicionActual;

        // Mueve la plataforma hacia el punto actual.
        rb.MovePosition( Vector2.MoveTowards(rb.position, puntos[indiceActual].position, velocidad * Time.fixedDeltaTime));

        // Comprueba si ha llegado cerca del punto objetivo.
        if (Vector2.Distance(rb.position, puntos[indiceActual].position) < 0.1f)
        {
            indiceActual++;

            if (indiceActual >= puntos.Length)
            {
                indiceActual = 0;
            }
        }
    }
}
using UnityEngine;

// Vida de los enemigos: parpadean en rojo al recibir daño y mueren al llegar a 0.
public class VidaKer : MonoBehaviour
{
    [SerializeField] private int vida = 3;
    [SerializeField] private float tiempoRojo = 0.15f;
    [SerializeField] private float tiempoMuerte = 0.6f;
    [SerializeField] private GameObject orbeVida;

    private SpriteRenderer sprite;
    private Animator anim;
    private Rigidbody2D rb;

    private float contadorRojo = 0;
    private float contadorMuerte = 0;
    private bool muerto = false;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (contadorRojo > 0)
        {
            contadorRojo -= Time.deltaTime;

            if (contadorRojo <= 0)
            {
                sprite.color = Color.white;
            }
        }

        if (muerto)
        {
            contadorMuerte -= Time.deltaTime;

            if (contadorMuerte <= 0)
            {
                Instantiate(orbeVida, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public void RecibirDanio(int danio)
    {
        if (muerto)
        { 
            return;
        }

            vida -= danio;
        sprite.color = Color.red;
        contadorRojo = tiempoRojo;

        if (vida <= 0)
        {
            muerto = true;
            contadorMuerte = tiempoMuerte;

            // Deja de moverse y de chocar
            EnemigoKer ker = GetComponent<EnemigoKer>();
            if (ker != null)
            {
                ker.enabled = false;
            }
            rb.simulated = false;

            if (anim != null)
            {
                anim.SetTrigger("Muerte");
            }

            if (GestorJuego.instancia != null)
            {
                GestorJuego.instancia.SumarPuntos(100);
            }
        }
    }
}
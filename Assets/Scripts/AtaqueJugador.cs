using UnityEngine;

public class AtaqueJugador : MonoBehaviour
{
    //se puede modificar las variables desde el inspector pero no desde otro Script
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque = 0.6f;
    [SerializeField] private int danio = 1;
    [SerializeField] private float tiempoEntreGolpes = 0.3f;
    [SerializeField] private float ventanaCombo = 0.7f;

    private Animator anim;
    private SpriteRenderer sprite;
    private float distanciaPunto;
    private int golpeActual = 0;
    private float contadorGolpe = 0;
    private float contadorCombo = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        distanciaPunto = puntoAtaque.localPosition.x;
    }

    void Update()
    {
        // Reduce el tiempo de espera entre ataques.
        if (contadorGolpe > 0)
        {
            contadorGolpe -= Time.deltaTime;
        }

        // Si tarda mucho en volver a atacar, el combo empieza de nuevo
        if (contadorCombo > 0)
        {
            contadorCombo -= Time.deltaTime;
        }
        else
        {
            golpeActual = 0;
        }

        // El punto de ataque se pone delante según hacia dónde mira
        if (sprite.flipX)
        {
            puntoAtaque.localPosition = new Vector3(-distanciaPunto, puntoAtaque.localPosition.y, 0);
        }
        else
        {
            puntoAtaque.localPosition = new Vector3(distanciaPunto, puntoAtaque.localPosition.y, 0);
        }

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J)) && contadorGolpe <= 0)
        {
            Atacar();
        }
    }

    private void Atacar()
    {
        golpeActual++;
        if (golpeActual > 3)
        {
            golpeActual = 1;
        }

        contadorGolpe = tiempoEntreGolpes;
        contadorCombo = ventanaCombo;
        anim.SetTrigger("Ataque" + golpeActual);

        int danioGolpe = danio;
        if (golpeActual == 3)
        {
            danioGolpe = danio * 2;
        }

        // Busca enemigos
        Collider2D[] golpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque);

        foreach (Collider2D golpeado in golpeados)
        {
            VidaKer enemigo = golpeado.GetComponent<VidaKer>();

            if (enemigo != null)
            {
                enemigo.RecibirDanio(danioGolpe);
            }
        }
    }

    // Dibuja el círculo de ataque en la escena
    void OnDrawGizmosSelected()
    {
        if (puntoAtaque != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(puntoAtaque.position, radioAtaque);
        }
    }
}
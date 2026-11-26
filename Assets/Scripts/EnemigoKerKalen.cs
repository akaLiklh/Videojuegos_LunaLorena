using UnityEngine;

// Ker: patrulla, se para en los extremos, detecta a Mel�noe, la persigue y le hace da�o al tocarla.
public class EnemigoKerKalen : MonoBehaviour
{
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float velocidadPersecucion = 3.5f;
    [SerializeField] private float distanciaPatrulla = 3f;
    [SerializeField] private float distanciaDeteccion = 5f;
    [SerializeField] private float tiempoEspera = 1f;
    [SerializeField] private float tiempoAlerta = 0.6f;
    [SerializeField] private int danio = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private Transform jugador;

    private float inicioX;
    private int direccion = 1;
    private int estado = 0;
    private float contadorEspera = 0;
    private float contadorAlerta = 0;
    private bool persiguiendo = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        inicioX = transform.position.x;
    }

    void Start()
    {
        VidaJugador melinoe = FindObjectOfType<VidaJugador>();
        if (melinoe != null)
        {
            jugador = melinoe.transform;
        }
    }

    void Update()
    {
        // �Est� Mel�noe cerca?
        bool cerca = false;
        if (jugador != null)
        {
            cerca = Vector2.Distance(transform.position, jugador.position) < distanciaDeteccion;
        }

        if (cerca && !persiguiendo)
        {
            persiguiendo = true;
            contadorAlerta = tiempoAlerta;
        }
        if (!cerca)
        {
            persiguiendo = false;
        }

        if (persiguiendo)
        {
            // Primero se queda quieto escaneando, luego la persigue
            if (contadorAlerta > 0)
            {
                contadorAlerta -= Time.deltaTime;
                estado = 2;
            }
            else
            {
                if (jugador.position.x > transform.position.x)
                {
                    direccion = 1;
                }
                else
                {
                    direccion = -1;
                }
                estado = 3;
            }
        }
        else if (contadorEspera > 0)
        {
            // Parado en un extremo de la patrulla
            contadorEspera -= Time.deltaTime;
            estado = 1;
        }
        else
        {
            // Patrulla y da la vuelta al llegar al final
            if (direccion == 1 && transform.position.x > inicioX + distanciaPatrulla)
            {
                direccion = -1;
                contadorEspera = tiempoEspera;
            }
            else if (direccion == -1 && transform.position.x < inicioX - distanciaPatrulla)
            {
                direccion = 1;
                contadorEspera = tiempoEspera;
            }
            estado = 0;
        }

        sprite.flipX = (direccion == -1);
        anim.SetInteger("Estado", estado);
    }

    void FixedUpdate()
    {
        if (estado == 0)
        {
            rb.velocity = new Vector2(direccion * velocidad, 0);
        }
        else if (estado == 3)
        {
            rb.velocity = new Vector2(direccion * velocidadPersecucion, 0);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnCollisionStay2D(Collision2D otro)
    {
        VidaJugador melinoe = otro.gameObject.GetComponent<VidaJugador>();

        if (melinoe != null)
        {
            melinoe.RecibirDanio(danio, transform.position);
        }
    }

    // Dibuja la zona de patrulla (amarillo) y de detecci�n (rojo)
    void OnDrawGizmosSelected()
    {
        float x = transform.position.x;
        if (Application.isPlaying)
        {
            x = inicioX;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(x - distanciaPatrulla, transform.position.y, 0), new Vector3(x + distanciaPatrulla, transform.position.y, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaDeteccion);
    }
}
using UnityEngine;

// Movimiento de Melínoe: correr, salto variable, dash y rodar.
public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] private float velocidad = 6f;
    [SerializeField] private float fuerzaSalto = 14f;
    [SerializeField] private float margenSalto = 0.1f;

    [SerializeField] private float velocidadDash = 16f;
    [SerializeField] private float duracionDash = 0.18f;
    [SerializeField] private float esperaDash = 0.6f;

    [SerializeField] private float velocidadRodar = 9f;
    [SerializeField] private float duracionRodar = 0.4f;
    [SerializeField] private float esperaRodar = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private float entradaX;
    private bool enSuelo;
    private float contadorMargenSalto;
    private bool haciendoDash;
    private float tiempoDash;
    private float proximoDash;
    private bool rodando;
    private float tiempoRodar;
    private float proximoRodar;
    private float gravedadNormal;
    private int direccion = 1;
    private SpriteRenderer sprite;
    private PlataformaMueve plataformaActual;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gravedadNormal = rb.gravityScale;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Si está en el suelo reiniciamos el margen, si no, se va gastando
        if (enSuelo)
        {
            contadorMargenSalto = margenSalto;
        }
        else
        {
            contadorMargenSalto -= Time.deltaTime;
        }

        // Mientras dura el dash solo contamos el tiempo
        if (haciendoDash)
        {
            tiempoDash -= Time.deltaTime;
            if (tiempoDash <= 0)
            {
                rb.gravityScale = gravedadNormal;
                haciendoDash = false;
            }
            return;
        }

        // Mientras rueda solo contamos el tiempo
        if (rodando)
        {
            tiempoRodar -= Time.deltaTime;
            if (tiempoRodar <= 0)
            {
                rodando = false;
            }
            return;
        }
        // Guarda en la variable entradaX lo que está pulsando el jugador en el eje horizontal
        entradaX = Input.GetAxisRaw("Horizontal");

        if (entradaX > 0)
        {
            Girar(1);
        }
        else if (entradaX < 0)
        {
            Girar(-1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && contadorMargenSalto > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            contadorMargenSalto = 0;
        }

        // Cuando soltamos el espacio mientras sube, salta menos
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }

        // Dash con Shift
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= proximoDash)
        {
            EmpezarDash();
        }

        // Rodar con Ctrl, solo para el suelo
        if (Input.GetKeyDown(KeyCode.LeftControl) && enSuelo && Time.time >= proximoRodar)
        {
            EmpezarRodar();
        }

        // Animaciones
        anim.SetBool("Corriendo", entradaX != 0);
        anim.SetBool("EnSuelo", enSuelo);
    }

    void FixedUpdate()
    {
        Debug.Log("entradaX = " + entradaX);
        // Mientras rueda, avanza hacia donde mira
        if (rodando)
        {
            rb.velocity = new Vector2(direccion * velocidadRodar, rb.velocity.y);
        }
        else if (!haciendoDash)
        {
            //si la plataforma se mueve con el personaje encima este se queda quieto y se mueve a la vez
            if (plataformaActual != null && entradaX == 0)
            {
                rb.velocity = new Vector2(plataformaActual.velocidadPlataforma.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(entradaX * velocidad, rb.velocity.y);
            }
        }
    }

    void EmpezarDash()
    {
        haciendoDash = true;
        tiempoDash = duracionDash;
        proximoDash = Time.time + esperaDash;
        anim.SetTrigger("Dash");
        rb.gravityScale = 0;

        // La lanzamos hacia donde mira
        rb.velocity = new Vector2(direccion * velocidadDash, 0);
    }

    void EmpezarRodar()
    {
        rodando = true;
        tiempoRodar = duracionRodar;
        proximoRodar = Time.time + duracionRodar + esperaRodar;
        anim.SetTrigger("Rodar");
    }

    void Girar(int nuevaDireccion)
    {
        direccion = nuevaDireccion;
        sprite.flipX = (direccion == -1);   // si mira a la izquierda, giramos la imagen
    }

    // Si choca con algo que tiene debajo, está en el suelo
    void OnCollisionStay2D(Collision2D choque)
    {
        if (choque.GetContact(0).normal.y > 0.5f)
        {
            enSuelo = true;
            plataformaActual = choque.gameObject.GetComponent<PlataformaMueve>();
        }
    }

    // Al dejar de tocarlo, ya no está en el suelo
    void OnCollisionExit2D(Collision2D choque)
    {
        enSuelo = false;
        if (choque.gameObject.GetComponent<PlataformaMueve>())
        {
            plataformaActual = null;
        }
    }
}
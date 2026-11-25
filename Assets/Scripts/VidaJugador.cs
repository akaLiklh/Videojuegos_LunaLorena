using UnityEngine;
using UnityEngine.SceneManagement;

// Vida de Melínoe: recibe daño, parpadea en rojo, sale empujada y muere al llegar a 0.
public class VidaJugador : MonoBehaviour
{
    [SerializeField] private int vidaMaxima = 5;
    [SerializeField] private float tiempoSinDanio = 1f;
    [SerializeField] private float tiempoEmpuje = 0.2f;
    [SerializeField] private float fuerzaEmpuje = 8f;
    [SerializeField] private float tiempoMuerte = 1.5f;
    [SerializeField] private string escenaDerrota = "";

    private float contadorSinDanio = 0;
    private float contadorEmpuje = 0;
    private float contadorMuerte = 0;
    private bool muerta = false;
    public int vida = 5;


    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private MovimientoJugador movimiento;
    private AtaqueJugador ataque;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        movimiento = GetComponent<MovimientoJugador>();
        ataque = GetComponent<AtaqueJugador>();
    }

    void Update()
    {
        // Parpadeo en rojo mientras no puede recibir daño
        if (contadorSinDanio > 0)
        {
            contadorSinDanio -= Time.deltaTime;
            bool rojo = (int)(contadorSinDanio * 10) % 2 == 0 && contadorSinDanio > 0;

            if (rojo)
            {
                sprite.color = Color.red;
            }
            else
            {
                sprite.color = Color.white;
            }
        }

        // Al acabar el empujón vuelve a moverse
        if (contadorEmpuje > 0)
        {
            contadorEmpuje -= Time.deltaTime;

            if (contadorEmpuje <= 0 && !muerta)
            {
                movimiento.enabled = true;
            }
        }

        // Espera un poco tras morir y carga la derrota
        if (muerta)
        {
            contadorMuerte -= Time.deltaTime;

            if (contadorMuerte <= 0)
            {
                Derrota();
            }
        }
    }

    public void RecibirDanio(int danio, Vector2 origen)
    {
        if (muerta || contadorSinDanio > 0)
        {
            return;
        }

        vida -= danio;
        contadorSinDanio = tiempoSinDanio;
        contadorEmpuje = tiempoEmpuje;

        // Empujón hacia el lado contrario del golpe
        float lado = 1;
        if (transform.position.x < origen.x)
        {
            lado = -1;
        }
        movimiento.enabled = false;
        rb.velocity = new Vector2(lado * fuerzaEmpuje, fuerzaEmpuje / 2);

        if (vida <= 0)
        {
            muerta = true;
            ataque.enabled = false;
            contadorMuerte = tiempoMuerte;
        }
    }

    public void Curar(int cantidadAumenta)
    {
        vida += cantidadAumenta;
        if(vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }
    }

    private void Derrota()
    {
        if (escenaDerrota == "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(escenaDerrota);
        }
    }

    public int GetVida()
    {
        return vida;
    }
}
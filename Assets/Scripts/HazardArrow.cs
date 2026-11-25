using UnityEngine;

public class HazardArrow : MonoBehaviour
{
    public Vector2 tamannoAtaque;
    public float distanciaAtaque;
    public LayerMask capaJugador;
    public int danio = 1;

    private Animator anim;
    private bool activado;
    private float temporizador;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
       
        if (DetectarJugador())
        {
  
            anim.SetBool("Disparar",true);
          
        }
        else
        {
            anim.SetBool("Disparar",false);
            
        }
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        VidaJugador jugador = other.GetComponent<VidaJugador>();

        if (jugador != null)
        {
            jugador.RecibirDanio(danio, transform.position);
        }
    }*/
    public void HacerDanio()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            tamannoAtaque,
            0,
            Vector2.left,
            distanciaAtaque,
            capaJugador
        );

        if (hit.collider != null)
        {
            VidaJugador jugador = hit.collider.GetComponent<VidaJugador>();

            if (jugador != null)
            {
                jugador.RecibirDanio(danio, transform.position);
            }
        }
    }
    private bool DetectarJugador()
    {
        RaycastHit2D rayo = Physics2D.BoxCast(transform.position,tamannoAtaque, 0, Vector2.left,distanciaAtaque,capaJugador );

        return rayo;
    }

    
}
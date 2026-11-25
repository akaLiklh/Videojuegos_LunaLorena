using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] private VidaJugador melinoe;

    private Label textoVida;
    private Label textoPuntos;

    void OnEnable()
    {
        VisualElement raiz = GetComponent<UIDocument>().rootVisualElement;
        textoVida = raiz.Q<Label>("TextoVida");
        textoPuntos = raiz.Q<Label>("TextoPuntos");
    }

    void Start()
    {
   
    }

    void Update()
    {
        if (melinoe != null)
        {
            int vida = melinoe.GetVida();

            if (vida < 0)
            {
                vida = 0;
            }

            textoVida.text = "VIDA " + vida;
        }

        if (GestorJuego.instancia != null)
        {
            textoPuntos.text = "PUNTOS " + GestorJuego.instancia.GetPuntos();
        }
    }
}
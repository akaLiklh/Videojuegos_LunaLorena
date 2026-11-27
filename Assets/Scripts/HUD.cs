using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] private VidaJugador melinoe;
    [SerializeField] private StyleSheet estilos;
    [SerializeField] private int vidaMaxima = 5;
    [SerializeField] private Sprite corazonLleno;
    [SerializeField] private Sprite corazonVacio;
    [SerializeField] private Sprite corazonCritico;
    private VisualElement contenedorCorazones;
    private Label textoPuntos;
    private VisualElement[] corazones;
    private int vidaMostrada = -1;
    private int puntosMostrados = -1;

    void OnEnable()
    {
        VisualElement raiz = GetComponent<UIDocument>().rootVisualElement;

        if (estilos != null && !raiz.styleSheets.Contains(estilos))
        {
            raiz.styleSheets.Add(estilos);
        }

        contenedorCorazones = raiz.Q("Corazones");
        textoPuntos = raiz.Q<Label>("TextoPuntos");
        CrearCorazones();
        vidaMostrada = -1;
        puntosMostrados = -1;
    }

    void Update()
    {
        ActualizarVida();
        ActualizarPuntos();
    }

    private void CrearCorazones()
    {
        contenedorCorazones.Clear();
        corazones = new VisualElement[vidaMaxima];

        for (int i = 0; i < vidaMaxima; i++)
        {
            VisualElement corazon = new VisualElement();
            corazon.AddToClassList("corazon");
            corazon.style.backgroundImage = new StyleBackground(corazonLleno);
            contenedorCorazones.Add(corazon);
            corazones[i] = corazon;
        }
    }

    private void ActualizarVida()
    {
        if (melinoe == null) return;

        int vida = melinoe.GetVida();

        if (vida < 0)
        {
            vida = 0;
        }

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        if (vida == vidaMostrada) return;

        vidaMostrada = vida;

        bool critica = vida == 1 && corazonCritico != null;

        for (int i = 0; i < corazones.Length; i++)
        {
            Sprite sprite;

            if (i >= vida)
            {
                sprite = corazonVacio;
            }
            else if (critica)
            {
                sprite = corazonCritico;
            }
            else
            {
                sprite = corazonLleno;
            }

            corazones[i].style.backgroundImage = new StyleBackground(sprite);
        }
    }

    private void ActualizarPuntos()
    {
        if (GestorJuego.instancia == null) return;

        int puntos = GestorJuego.instancia.GetPuntos();
        if (puntos == puntosMostrados) return;

        puntosMostrados = puntos;
        textoPuntos.text = puntos.ToString("000000");
    }
}
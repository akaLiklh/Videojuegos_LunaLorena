using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// Muestra la cinemática del comienzo viñeta a viñeta y luego carga el juego.
public class GestorCinematica : MonoBehaviour
{
    [SerializeField] private Sprite[] imagenes;             // falta rellenar
    [TextArea][SerializeField] private string[] textos;
    [SerializeField] private float segundosPorVineta = 5f;
    [SerializeField] private string escenaSiguiente = "Kalen";

    private VisualElement imagen;
    private Label subtitulo;
    private int vinetaActual = 0;
    private float tiempo = 0f;

    void Start()
    {
        VisualElement raiz = GetComponent<UIDocument>().rootVisualElement;
        imagen = raiz.Q("Imagen");
        subtitulo = raiz.Q<Label>("txtSubtitulo");

        raiz.Q<Button>("bSaltar").clicked += IrAlJuego;
        raiz.Q("Fondo").RegisterCallback<ClickEvent>(ClicEnFondo);

        if (textos.Length == 0)
        {
            IrAlJuego();
            return;
        }

        MostrarVineta();
    }

    void Update()
    {
        // Cuando acaba el tiempo de la viñeta, pasa a la siguiente
        tiempo += Time.deltaTime;
        if (tiempo >= segundosPorVineta)
        {
            SiguienteVineta();
        }
    }

    // Un clic en cualquier sitio pasa de viñeta (menos en el botón Saltar)
    void ClicEnFondo(ClickEvent evento)
    {
        if (evento.target is Button)
        {
            return;
        }
        SiguienteVineta();
    }

    // Pone la imagen (si hay) y el texto de la viñeta actual
    void MostrarVineta()
    {
        if (vinetaActual < imagenes.Length && imagenes[vinetaActual] != null)
        {
            imagen.style.backgroundImage = new StyleBackground(imagenes[vinetaActual]);
        }
        else
        {
            imagen.style.backgroundImage = StyleKeyword.None;
        }

        subtitulo.text = textos[vinetaActual];
        tiempo = 0f;
    }

    // Pasa a la siguiente viñeta. Si era la última, empieza la partida
    void SiguienteVineta()
    {
        vinetaActual++;
        if (vinetaActual < textos.Length)
        {
            MostrarVineta();
        }
        else
        {
            IrAlJuego();
        }
    }

    void IrAlJuego()
    {
        SceneManager.LoadScene(escenaSiguiente);
    }
}
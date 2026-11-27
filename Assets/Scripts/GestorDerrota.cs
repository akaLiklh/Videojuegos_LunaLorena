using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GestorDerrota : MonoBehaviour
{
    [SerializeField] private GameObject hud;
    [SerializeField] private string nombreEscena = "Eutimia";

    private UIDocument documento;
    private VisualElement raiz;
    private Label textoPuntuacion;
    private Button bReintentar;
    private Button bSalir;

    void OnEnable()
    {
        ObtenerElementos();
        raiz.style.display = DisplayStyle.None;
    }

    private void ObtenerElementos()
    {
        if (raiz != null) return;

        documento = GetComponent<UIDocument>();
        raiz = documento.rootVisualElement;

        textoPuntuacion = raiz.Q<Label>("TextoPuntuacion");
        bReintentar = raiz.Q<Button>("bReintentar");
        bSalir = raiz.Q<Button>("bSalir");

        bReintentar.clicked += Reintentar;
        bSalir.clicked += Menu;
    }

    public void MostrarDerrota()
    {
        ObtenerElementos();
        OcultarHUD();

        int puntos = 0;
        if (GestorJuego.instancia != null)
        {
            puntos = GestorJuego.instancia.GetPuntos();
        }

        textoPuntuacion.text = "PUNTUACIÓN: " + puntos;
        raiz.style.display = DisplayStyle.Flex;

        Time.timeScale = 1f;
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;

        if (GestorJuego.instancia != null)
        {
            GestorJuego.instancia.ReiniciarPuntos();
        }

        SceneManager.LoadScene(nombreEscena);
    }

    public void Menu()
    {
        Time.timeScale = 1f;

        if (GestorJuego.instancia != null)
        {
            GestorJuego.instancia.ReiniciarPuntos();
        }

        SceneManager.LoadScene("MenuPrincipal");
    }

    private void OcultarHUD()
    {
        if (hud != null)
        {
            hud.SetActive(false);
        }
    }
}
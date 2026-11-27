using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GestorVictoria : MonoBehaviour
{
    [SerializeField] private string textoTitulo = "HAS CONSEGUIDO SALIR DE EUTIMIA";
    [SerializeField] private GameObject hud;

    private VisualElement raiz;
    private Label titulo;
    private Label textoPuntuacion;
    private Button bSalir;

    void OnEnable()
    {
        raiz = GetComponent<UIDocument>().rootVisualElement;

        titulo = raiz.Q<Label>("Titulo");
        textoPuntuacion = raiz.Q<Label>("TextoPuntuacion");
        bSalir = raiz.Q<Button>("bSalir");

        titulo.text = textoTitulo;
        bSalir.clicked += Menu;

        raiz.style.display = DisplayStyle.None;
    }

    void OnDisable()
    {
        if (bSalir != null)
        {
            bSalir.clicked -= Menu;
        }
    }

    public void MostrarVictoria()
    {
        OcultarHUD();

        textoPuntuacion.text = "PUNTUACIÓN FINAL: " + GestorJuego.instancia.GetPuntos();
        raiz.style.display = DisplayStyle.Flex;

        Time.timeScale = 0f;
    }

    public void Menu()
    {
        Time.timeScale = 1f;

        GestorJuego.instancia.ReiniciarPuntos();

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
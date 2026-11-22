using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// Conecta los botones del menú principal hecho con UI Toolkit.
public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private GameObject menuOpciones;
    [SerializeField] private GameObject menuConfirmacionSalir;

    private VisualElement botones;
    private VisualElement pantallaPartida;

    void OnEnable()
    {
        VisualElement raiz = GetComponent<UIDocument>().rootVisualElement;

        botones = raiz.Q("Botones");
        pantallaPartida = raiz.Q("PantallaPartida");

        raiz.Q<Button>("BotonJugar").clicked += Jugar;
        raiz.Q<Button>("BotonOpciones").clicked += Opciones;
        raiz.Q<Button>("BotonSalir").clicked += Salir;

        raiz.Q<Button>("BotonNuevaPartida").clicked += NuevaPartida;
        raiz.Q<Button>("BotonCargarPartida").clicked += CargarPartida;
        raiz.Q<Button>("BotonVolver").clicked += Volver;

        // Si no hay partida guardada, no se puede cargar
        if (!PlayerPrefs.HasKey("EscenaGuardada"))
        {
            raiz.Q<Button>("BotonCargarPartida").SetEnabled(false);
        }
    }

    // Esconde los botones principales y enseña los de partida
    void Jugar()
    {
        botones.style.display = DisplayStyle.None;
        pantallaPartida.style.display = DisplayStyle.Flex;
    }

    // Vuelve a los botones principales
    void Volver()
    {
        pantallaPartida.style.display = DisplayStyle.None;
        botones.style.display = DisplayStyle.Flex;
    }

    // Empieza una partida nueva viendo la cinemática
    void NuevaPartida()
    {
        PlayerPrefs.DeleteKey("EscenaGuardada");
        SceneManager.LoadScene("CinematicaInicio");
    }

    // Carga el último nivel en el que estuvo la jugadora
    void CargarPartida()
    {
        string escena = PlayerPrefs.GetString("EscenaGuardada");
        SceneManager.LoadScene(escena);
    }

    // Abre el menú de opciones y oculta este
    void Opciones()
    {
        menuOpciones.GetComponent<MenuOpciones>().Abrir(gameObject);
    }

    // Pregunta antes de salir
    void Salir()
    {
        menuConfirmacionSalir.SetActive(true);
        gameObject.SetActive(false);
    }
}
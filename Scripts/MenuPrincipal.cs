using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// Menú principal: Jugar (Nueva / Cargar partida), Opciones y Salir.
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
        raiz.Q<Button>("BotonOpciones").clicked += AbrirOpciones;
        raiz.Q<Button>("BotonSalir").clicked += Salir;

        raiz.Q<Button>("BotonNuevaPartida").clicked += NuevaPartida;
        Button botonCargar = raiz.Q<Button>("BotonCargarPartida");
        botonCargar.clicked += CargarPartida;
        raiz.Q<Button>("BotonVolver").clicked += Volver;

        // Si no hay partida guardada, no se puede cargar
        if (!PlayerPrefs.HasKey("EscenaGuardada"))
        {
            botonCargar.SetEnabled(false);
        }
    }

    // Oculta los botones principales y enseña Nueva / Cargar / Volver
    private void Jugar()
    {
        botones.style.display = DisplayStyle.None;
        pantallaPartida.style.display = DisplayStyle.Flex;
    }

    // Vuelve a los botones principales
    private void Volver()
    {
        pantallaPartida.style.display = DisplayStyle.None;
        botones.style.display = DisplayStyle.Flex;
    }

    // Borra la partida guardada y empieza desde la cinemática
    private void NuevaPartida()
    {
        PlayerPrefs.DeleteKey("EscenaGuardada");
        SceneManager.LoadScene("CinematicaInicio");
    }

    // Carga la última escena guardada
    private void CargarPartida()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("EscenaGuardada"));
    }

    private void AbrirOpciones()
    {
        menuOpciones.GetComponent<MenuOpciones>().Abrir(gameObject);
    }

    private void Salir()
    {
        menuConfirmacionSalir.SetActive(true);
    }
}
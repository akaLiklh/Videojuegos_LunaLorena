using UnityEngine;
using UnityEngine.UIElements;

// Usamos esta clase para que funcione el menú de opciones del menú principal.
public class MenuOpciones : MonoBehaviour
{
    private GameObject menuAnterior;

    private VisualElement pantallaEleccion;
    private VisualElement pantallaAjustes;
    private VisualElement pantallaControles;
    private Label txtGuardado;

    private Button bAjustes;
    private Button bControles;
    private Button bGuardar;

    private Slider sVolumenGeneral;
    private Slider sVolumenMusica;
    private Slider sVolumenEfectos;
    private Toggle tPantallaCompleta;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        pantallaEleccion = root.Q("PantallaEleccion");
        pantallaAjustes = root.Q("PantallaAjustes");
        pantallaControles = root.Q("PantallaControles");
        txtGuardado = root.Q<Label>("txtGuardado");

        bAjustes = root.Q<Button>("bAjustes");
        bControles = root.Q<Button>("bControles");
        bGuardar = root.Q<Button>("bGuardar");

        sVolumenGeneral = root.Q<Slider>("sVolumenGeneral");
        sVolumenMusica = root.Q<Slider>("sVolumenMusica");
        sVolumenEfectos = root.Q<Slider>("sVolumenEfectos");
        tPantallaCompleta = root.Q<Toggle>("tPantallaCompleta");


        // Carga los ajustes guardados.
        sVolumenGeneral.value = PlayerPrefs.GetFloat("VolumenGeneral", 100);
        sVolumenMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 100);
        sVolumenEfectos.value = PlayerPrefs.GetFloat("VolumenEfectos", 100);
        tPantallaCompleta.value = Screen.fullScreen;

        bAjustes.clicked += MostrarAjustes;
        bControles.clicked += MostrarControles;
        bGuardar.clicked += GuardarAjustes;
        root.Query<Button>("bVolver").ForEach(b => b.clicked += Volver);

        // Al abrir el menú, empieza en la pantalla de elegir apartado.
        MostrarPantalla(pantallaEleccion);
    }

    // Abre el menú de opciones y recuerda desde qué menú se ha abierto.
    public void Abrir(GameObject desde)
    {
        menuAnterior = desde;
        desde.SetActive(false);
        gameObject.SetActive(true);
    }

    // Enseña solo una de las tres pantallas.
    private void MostrarPantalla(VisualElement pantalla)
    {
        pantallaEleccion.style.display = DisplayStyle.None;
        pantallaAjustes.style.display = DisplayStyle.None;
        pantallaControles.style.display = DisplayStyle.None;
        pantalla.style.display = DisplayStyle.Flex;
        txtGuardado.text = "";
    }

    // Abre el apartado de ajustes del juego.
    private void MostrarAjustes()
    {
        MostrarPantalla(pantallaAjustes);
    }

    // Abre el apartado de controles.
    private void MostrarControles()
    {
        MostrarPantalla(pantallaControles);
    }

    // Aplica y guarda todos los ajustes al pulsar "Guardar".
    private void GuardarAjustes()
    {
        AudioListener.volume = sVolumenGeneral.value / 100f;
        Screen.fullScreen = tPantallaCompleta.value;

        PlayerPrefs.SetFloat("VolumenGeneral", sVolumenGeneral.value);
        PlayerPrefs.SetFloat("VolumenMusica", sVolumenMusica.value);
        PlayerPrefs.SetFloat("VolumenEfectos", sVolumenEfectos.value);
        PlayerPrefs.Save();

        txtGuardado.text = "Ajustes guardados";
    }

    // Si está en un apartado, vuelve a la elección. Si no, vuelve al menú desde el que se abrió.
    private void Volver()
    {
        if (pantallaEleccion.style.display == DisplayStyle.None)
        {
            MostrarPantalla(pantallaEleccion);
        }
        else
        {
            menuAnterior.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        bAjustes.clicked -= MostrarAjustes;
        bControles.clicked -= MostrarControles;
        bGuardar.clicked -= GuardarAjustes;
    }
}
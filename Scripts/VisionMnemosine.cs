using UnityEngine;
using UnityEngine.UIElements;

// Animaciones del menú principal (vista de Mnemósine)
public class VisionMnemosine : MonoBehaviour
{
    [SerializeField] private float velocidadEscaneo = 0.25f;
    [SerializeField] private float duracionAnalisis = 4f;
    [SerializeField] private float tiempoPorLetra = 0.06f;

    private VisualElement lineaEscaneo;
    private VisualElement barraAnalisis;
    private VisualElement mira;
    private VisualElement puntoRec;
    private Label textoAnalisis;
    private Label textoRec;
    private Label textoConexion;
    private string frase;

    private float tiempo = 0;
    private float tiempoMira = 0;

    void OnEnable()
    {
        VisualElement raiz = GetComponent<UIDocument>().rootVisualElement;

        lineaEscaneo = raiz.Q("LineaEscaneo");
        barraAnalisis = raiz.Q("BarraAnalisis");
        mira = raiz.Q("Mira");
        puntoRec = raiz.Q("PuntoRec");
        textoAnalisis = raiz.Q<Label>("TextoAnalisis");
        textoRec = raiz.Q<Label>("TextoRec");
        textoConexion = raiz.Q<Label>("TextoConexion");

        frase = textoConexion.text;
    }

    void Update()
    {
        tiempo += Time.deltaTime;

        // Línea de escaneo: baja y vuelve a empezar
        float posicion = (tiempo * velocidadEscaneo) % 1;
        lineaEscaneo.style.top = Length.Percent(posicion * 100 - 13);

        // Barra de análisis
        float porcentaje = tiempo / duracionAnalisis * 100;
        if (porcentaje < 100)
        {
            barraAnalisis.style.width = Length.Percent(porcentaje);
            textoAnalisis.text = "ESCANEANDO " + (int)porcentaje + "%";
        }
        else
        {
            barraAnalisis.style.width = Length.Percent(100);
            textoAnalisis.text = "COINCIDENCIA 99,9%";
        }

        // Contador REC y punto que parpadea
        int segundos = (int)tiempo;
        int minutos = segundos / 60;
        textoRec.text = "REC 00:" + minutos.ToString("00") + ":" + (segundos % 60).ToString("00");
        puntoRec.visible = segundos % 2 == 0;

        // Mira que se abre y se cierra
        tiempoMira += Time.deltaTime;
        if (tiempoMira >= 1.2f)
        {
            tiempoMira = 0;
            mira.ToggleInClassList("mira-cerrada");
        }

        // Texto del pie que se escribe letra a letra
        int letras = (int)(tiempo / tiempoPorLetra);
        if (letras < frase.Length)
        {
            textoConexion.text = frase.Substring(0, letras) + "_";
        }
        else if ((int)(tiempo * 2) % 2 == 0)
        {
            textoConexion.text = frase + "_";
        }
        else
        {
            textoConexion.text = frase;
        }
    }
}
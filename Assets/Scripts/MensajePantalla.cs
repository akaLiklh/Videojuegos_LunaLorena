using UnityEngine;
using UnityEngine.UIElements;

public class MensajePantalla : MonoBehaviour
{
    public static MensajePantalla instancia;

    private Label texto;
    private float contador = 0;

    void OnEnable()
    {
        instancia = this;
        texto = GetComponent<UIDocument>().rootVisualElement.Q<Label>("TextoMensaje");
        texto.style.display = DisplayStyle.None;
    }

    void Update()
    {
        if (contador > 0)
        {
            contador -= Time.deltaTime;

            if (contador <= 0)
            {
                texto.style.display = DisplayStyle.None;
            }
        }
    }

    public void Mostrar(string mensaje, float segundos)
    {
        texto.text = mensaje;
        texto.style.display = DisplayStyle.Flex;
        contador = segundos;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// Conecta los botones del menú hecho con UI Toolkit.
public class MenuPrincipal : MonoBehaviour
{
    void OnEnable()
    {
        VisualElement raiz = GetComponent<UIDocument>().rootVisualElement;

        raiz.Q<Button>("BotonJugar").clicked += Jugar;
        raiz.Q<Button>("BotonOpciones").clicked += Opciones;
        raiz.Q<Button>("BotonSalir").clicked += Salir;
    }

    void Jugar()
    {
        SceneManager.LoadScene("Kalen");
    }

    void Opciones()
    {
        Debug.Log("Opciones (pendiente)");
    }

    void Salir()
    {
        Application.Quit();
    }
}
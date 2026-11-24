using UnityEngine;
using UnityEngine.UIElements;

// Usamos esta clase para preguntar al jugador si quiere salir del juego.
public class MenuConfirmacionSalir : MonoBehaviour
{
    // Menú al que vuelve si pulsa "No".
    public GameObject menuAnterior;

    private Button bSi;
    private Button bNo;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        bSi = root.Q<Button>("bSi");
        bNo = root.Q<Button>("bNo");

        bSi.clicked += Salir;
        bNo.clicked += Volver;
    }

    // Cierra el juego.
    private void Salir()
    {
        Application.Quit();
    }

    private void Volver()
    {
        menuAnterior.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        bSi.clicked -= Salir;
        bNo.clicked -= Volver;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorVictoria : MonoBehaviour
{
    public GameObject panelVictoria;
    public TextMeshProUGUI textoPuntuacion;

    public void MostrarVictoria()
    {
        panelVictoria.SetActive(true);

        textoPuntuacion.text =
            "PUNTUACIÓN FINAL: " +
            GestorJuego.instancia.GetPuntos();

        Time.timeScale = 0f;
    }

    public void Menu()
    {
        Time.timeScale = 1f;

        GestorJuego.instancia.ReiniciarPuntos();

        SceneManager.LoadScene("MenuPrincipal");
    }
}
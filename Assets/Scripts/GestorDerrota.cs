using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorDerrota : MonoBehaviour
{
    public GameObject panelDerrota;
    public TextMeshProUGUI textoPuntuacion;

    public void MostrarDerrota()
    {
        panelDerrota.SetActive(true);

        textoPuntuacion.text =
            "PUNTUACIÓN FINAL: " +
            GestorJuego.instancia.GetPuntos();

        Time.timeScale = 0f;
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;

        GestorJuego.instancia.ReiniciarPuntos();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        Time.timeScale = 1f;

        GestorJuego.instancia.ReiniciarPuntos();

        SceneManager.LoadScene("MenuPrincipal");
    }
}
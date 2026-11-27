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
            "PUNTUACIÓN: " +
            GestorJuego.instancia.GetPuntos();

        Time.timeScale = 1f;
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;

        GestorJuego.instancia.ReiniciarPuntos();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        Time.timeScale = 1f;

        GestorJuego.instancia.ReiniciarPuntos();

        SceneManager.LoadScene("MenuPrincipal");
    }
}
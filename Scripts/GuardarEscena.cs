using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardarEscena : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetString("EscenaGuardada", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }
}
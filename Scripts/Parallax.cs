using UnityEngine;

// Mueve una capa de fondo más despacio que la cámara para dar sensación de profundidad.
public class Parallax : MonoBehaviour
{
    // 0 = va pegada a la cámara (muy lejos, como el cielo).
    // 1 = se mueve como el escenario (muy cerca).
    [Range(0f, 1f)]
    [SerializeField] private float factor = 0.2f;

    private Transform cam;        // la cámara principal
    private Vector3 camInicio;    // dónde estaba la cámara al empezar
    private Vector3 posInicio;    // dónde estaba esta capa al empezar

    void Start()
    {
        cam = Camera.main.transform;
        camInicio = cam.position;
        posInicio = transform.position;
    }

    // LateUpdate se ejecuta después de que la cámara se haya movido en ese frame
    void LateUpdate()
    {
        // Cuánto se ha movido la cámara desde el principio
        Vector3 desplazamiento = cam.position - camInicio;

        // La capa acompaña a la cámara en la parte (1 - factor):
        // con factor 0 parece quieta al fondo,
        // con factor 1 se mueve como el escenario.
        transform.position = new Vector3(
            posInicio.x + desplazamiento.x * (1f - factor),
            posInicio.y + desplazamiento.y * (1f - factor),
            posInicio.z);
    }
}
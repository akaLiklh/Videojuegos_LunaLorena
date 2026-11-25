using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMueve : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float velocidadMovimiento;
    private int sigientePlataforma = 1;
    private bool ordenPlataformas = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ordenPlataformas && sigientePlataforma +1 >= puntosMovimiento.Length)
        {
            ordenPlataformas = false;
        }
        if (ordenPlataformas && sigientePlataforma <= 0)
        {
            ordenPlataformas = true;
        }

        if(Vector2.Distance(transform.position, puntosMovimiento[sigientePlataforma].position)< 0.1f)
        {
            if (ordenPlataformas)
            {
                sigientePlataforma += 1;
            }
            else
            {
                sigientePlataforma -= 1;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[sigientePlataforma].position, velocidadMovimiento + Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

}

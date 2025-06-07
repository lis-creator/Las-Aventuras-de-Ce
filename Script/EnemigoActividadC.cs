using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el comportamiento de un enemigo que activa una actividad educativa al colisionar con el jugador.
/// </summary>

public class EnemigoActividadC : MonoBehaviour
{
    /// <summary>
    /// Lista de preguntas que se enviarán a la actividad cuando se active.
    /// </summary>
    
    public List<string> preguntas = new List<string>();

    /// <summary>
    /// Lista de respuestas correspondientes a las preguntas.
    /// </summary>
    
    public List<string> respuestas = new List<string>();

    /// <summary>
    /// Detecta colisiones con otros objetos. 
    /// Si colisiona con el jugador, inicia la actividad asociada a este enemigo.
    /// </summary>
    /// <param name="collision">Información de la colisión detectada.</param>
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto que colisionó tiene la etiqueta "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Busca el componente ActividadLetraC en la escena
            ActividadLetraC actividad = FindFirstObjectByType<ActividadLetraC>();

            if (actividad != null)
            {
                actividad.IniciarActividad(gameObject, preguntas, respuestas);
            }
            else
            {
                Debug.LogWarning("No se encontr� el script ActividadLetraC en la escena.");
            }
        }
    }
}


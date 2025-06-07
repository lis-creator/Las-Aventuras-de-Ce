using UnityEngine;

/// <summary>
/// Controla el comportamiento de un enemigo que al colisionar con el jugador
/// activa una pregunta, ya sea de forma manual o aleatoria, mediante un controlador asignado.
/// </summary>
public class Enemigo : MonoBehaviour
{
     /// <summary>
    /// Referencia a un componente que puede ser de tipo <see cref="ControladorPreguntaManual"/> o <see cref="ControladorPreguntaRandom"/>. 
    /// Se encarga de mostrar la pregunta cuando el jugador colisiona con este enemigo.
    /// </summary>
    
    public MonoBehaviour controladorPregunta; 

    /// <summary>
    /// Indica si ya se ha activado una pregunta para evitar activaciones múltiples.
    /// </summary>
    /// 
    private bool haActivadoPregunta = false;

    /// <summary>
    /// Detecta colisiones con otros objetos.
    /// Si colisiona con el jugador, activa la pregunta usando el controlador asignado.
    /// </summary>
    /// <param name="collision">Información de la colisión detectada.</param>

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (haActivadoPregunta) return;
        
      // Detecta qué tipo de controlador de pregunta se ha asignado
        if (collision.gameObject.CompareTag("Player"))
        {
            haActivadoPregunta = true;

            if (controladorPregunta != null)
            {
                // Detecta qu� tipo de controlador es
                if (controladorPregunta is ControladorPreguntaRandom random)
                {
                    random.MostrarPregunta(gameObject);
                }
                else if (controladorPregunta is ControladorPreguntaManual manual)
                {
                    manual.MostrarPanel(gameObject);
                }
                else
                {
                    Debug.LogWarning("El script asignado no es un controlador de pregunta v�lido.");
                }

                Time.timeScale = 0f;
            }
            else
            {
                Debug.LogWarning("No se ha asignado ning�n controlador de pregunta.");
            }
        }
    }
}



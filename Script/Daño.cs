using UnityEngine;

/// <summary>
/// Controla el comportamiento de objetos que causan daño al jugador.
/// Cuando el jugador colisiona con este objeto, se activa un respawn y se muestra un mensaje en consola.
/// </summary>

public class Daño : MonoBehaviour
{
    /// <summary>
    /// Detecta colisiones con otros objetos.
    /// Si colisiona con el jugador, reinicia su posición y muestra un mensaje en la consola.
    /// </summary>
     /// <param name="collision">Información de la colisión detectada.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto colisionado tiene la etiqueta "Player"
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Player Damaged");
             // Obtiene el componente Movimiento del jugador.
            Movimiento player = collision.gameObject.GetComponent<Movimiento>();
            if (player != null)
            {
                player.Respawn();
            }
        }
    }
}

using UnityEngine;

/// <summary>
/// Script que controla el comportamiento de los objetos coleccionables en el juego.
/// Cuando el jugador entra en contacto con este objeto, se suma un punto y se destruye el coleccionable.
/// </summary>

public class Collectible : MonoBehaviour
{
    /// <summary>
    /// Método que se ejecuta automáticamente cuando otro collider con la opción 'IsTrigger' activa entra en contacto.
    /// </summary>
    /// <param name="other">El collider que colisionó con este objeto.</param>
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddPoint();
            Destroy(gameObject);
        }
    }
}

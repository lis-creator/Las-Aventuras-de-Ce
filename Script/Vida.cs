using UnityEngine;

/// <summary>
/// Clase que gestiona la vida del jugador.
/// Permite recibir daño, controlar la vida actual y manejar la muerte del jugador.
/// </summary>
public class Vida : MonoBehaviour
{
    /// <summary>
    /// Vida máxima del jugador.
    /// </summary>
    public int maxHealth = 3;

    /// <summary>
    /// Vida actual del jugador.
    /// </summary>
    private int currentHealth;

    /// <summary>
    /// Inicializa la vida del jugador al valor máximo al iniciar el juego.
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Aplica daño al jugador y verifica si su vida ha llegado a cero.
    /// </summary>
    /// <param name="amount">Cantidad de daño recibido.</param>
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Vida actual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Maneja la muerte del jugador, desactivando su objeto en la escena.
    /// 
    /// Nota: Si el personaje reaparece o se reinicia al morir,
    /// es porque se debe a que otro script recarga la escena o vuelve a activar
    /// el objeto del jugador. Verificar otros scripts relacionados con
    /// la gestión de escenas o el reinicio de niveles.
    /// 
    /// Opcionalmente se puede pausar el juego usando Time.timeScale = 0f
    /// o mostrar una pantalla de Game Over.
    /// </summary>
    void Die()
    {
        Debug.Log("¡El jugador ha muerto!");

        // Desactiva el objeto del jugador.
        gameObject.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Administra el puntaje del jugador y actualiza la interfaz de usuario con el valor actual.
/// </summary>

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// Instancia única del ScoreManager (patrón Singleton).
    /// </summary>
    
    public static ScoreManager instance;

    /// <summary>
    /// Referencia al componente de texto en la UI donde se mostrará el puntaje.
    /// </summary>
    
    public Text scoreText;

    /// <summary>
    /// Almacena el puntaje actual del jugador.
    /// </summary>
    
    private int score = 0;

     /// <summary>
    /// Se ejecuta antes del método Start. Se asegura de que solo exista una instancia de ScoreManager.
    /// </summary>

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Incrementa el puntaje en 1 y actualiza el texto en pantalla.
    /// </summary>
    
    public void AddPoint()
    {
        score++;
        scoreText.text = "Puntaje: " + score.ToString();
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Este script permite cambiar de escena cuando el jugador colisiona con un objeto que tiene este script (por ejemplo, una bandera).
/// </summary>

public class CambioDeEscenaPorBandera : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string nombreEscena;

    /// <summary>
    /// Método que se ejecuta automáticamente cuando otro collider 2D con IsTrigger activo entra en contacto.
    /// </summary>
    /// <param name="collision">El collider que colisionó con este objeto.</param>
    
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        // Verifica si el objeto que colisionó tiene la etiqueta 'Player'
        if (collision.CompareTag("Player"))
        {
            Debug.Log("�Jugador toc� la bandera!");
            SceneManager.LoadScene(nombreEscena);
        }
    }
}

using UnityEngine;

/// <summary>
/// Gestiona una instancia única de un objeto de música que persiste entre escenas.
/// </summary>

public class MusicaPersistente : MonoBehaviour
{
     /// <summary>
    /// Instancia estática de MusicaPersistente (patrón Singleton).
    /// </summary>
    
    private static MusicaPersistente instancia;
     /// <summary>
    /// Se ejecuta antes del método Start. 
    /// Se asegura de que solo exista una instancia de este objeto y que no se destruya al cambiar de escena.
    /// </summary>
    void Awake()
    {
          // Si no existe una instancia, se asigna esta y se mantiene entre escenas
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // evita duplicados
        }
    }
}
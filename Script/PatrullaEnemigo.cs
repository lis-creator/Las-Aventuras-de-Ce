using UnityEngine;

/// <summary>
/// Controla el patrullaje de un enemigo entre dos puntos en la escena.
/// </summary>
public class PatrullaEnemigo : MonoBehaviour
{
    /// <summary>
    /// Primer punto de patrullaje.
    /// </summary>
    
    public Transform puntoA;

     /// <summary>
    /// Segundo punto de patrullaje.
    /// </summary>
    
    public Transform puntoB;

    /// <summary>
    /// Punto objetivo actual hacia donde se mueve el enemigo.
    /// </summary>
    private Transform objetivoActual;

     /// <summary>
    /// Velocidad de movimiento del enemigo.
    /// </summary>
    public float velocidad = 2f;

    /// <summary>
    /// Referencia al componente SpriteRenderer para voltear el sprite.
    /// </summary>

    private SpriteRenderer spriteRenderer;

     /// <summary>
    /// Inicializa el objetivo de patrullaje y obtiene el componente SpriteRenderer.
    /// </summary>

    void Start()
    {
        objetivoActual = puntoB;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Actualiza la posición del enemigo cada frame, moviéndolo entre los dos puntos
    /// y volteando su sprite al cambiar de dirección.
    /// </summary>
    
    void Update()
    {
         // Mueve al enemigo hacia el objetivo actual
        transform.position = Vector2.MoveTowards(transform.position, objetivoActual.position, velocidad * Time.deltaTime);
        
           // Verifica si ha llegado al objetivo
        if (Vector2.Distance(transform.position, objetivoActual.position) < 0.1f)
        {
            // Cambiar direcci�n
            objetivoActual = (objetivoActual == puntoA) ? puntoB : puntoA;

            // Voltear el sprite horizontalmente
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}

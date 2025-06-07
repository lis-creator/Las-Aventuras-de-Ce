using UnityEngine;

/// <summary>
/// Controla el movimiento del personaje, incluyendo correr, saltar y reiniciar su posición.
/// </summary>

public class Movimiento : MonoBehaviour
{
     /// <summary>
    /// Velocidad de desplazamiento horizontal del personaje.
    /// </summary>
    public float runSpeed = 5f;

    /// <summary>
    /// Fuerza aplicada al personaje al saltar.
    /// </summary>

    public float jumpForce = 5f;

    /// <summary>
    /// Punto desde donde se verifica si el personaje está tocando el suelo.
    /// </summary>
    
    public Transform groundCheck;

    /// <summary>
    /// Radio del círculo de comprobación de suelo.
    /// </summary>
    
    public float groundCheckRadius = 0.2f;

     /// <summary>
    /// Capa que se considera como suelo.
    /// </summary>
    
    public LayerMask groundLayer;

    /// <summary>
    /// Referencia al componente Rigidbody2D del personaje.
    /// </summary>

    private Rigidbody2D rb;

     /// <summary>
    /// Referencia al componente SpriteRenderer para voltear el sprite.
    /// </summary>
    
    private SpriteRenderer sr;
    /// <summary>
    /// Indica si el personaje está tocando el suelo.
    /// </summary>
    
    private bool isGrounded;

    /// <summary>
    /// Referencia al componente Animator para controlar animaciones.
    /// </summary>
    
    private Animator animator;
    /// <summary>
    /// Guarda la posición inicial del personaje para reiniciar cuando sea necesario.
    /// </summary>
    private Vector3 startPosition; // Posici�n inicial

    /// <summary>
    /// Inicializa referencias a componentes y guarda la posición inicial.
    /// </summary>
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        startPosition = transform.position; // Guardar posici�n inicial
    }
      
    /// <summary>
    /// Se ejecuta cada frame. Verifica si el personaje está en el suelo y permite saltar.
    /// </summary>


    void Update()
    {
        // Verifica si el jugador est� en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

     /// <summary>
    /// Se ejecuta a intervalos fijos. Controla el movimiento horizontal y actualiza las animaciones.
    /// </summary>


    void FixedUpdate()
    {
        float moveX = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX = runSpeed;
            sr.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -runSpeed;
            sr.flipX = true;
        }

        rb.velocity = new Vector2(moveX, rb.velocity.y);

        // Control de animaci�n: correr si realmente se mueve
        bool isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;
        animator.SetBool("Run", isMoving);
    }
     /// <summary>
    /// Reinicia la posición y velocidad del personaje a su estado inicial.
    /// </summary>

    public void Respawn()
    {
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}



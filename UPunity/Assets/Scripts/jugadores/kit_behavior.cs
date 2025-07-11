using UnityEngine;

public class kit_behavior : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 25f; // Velocidad ajustada para un movimiento rápido

    [Header("Salto")]
    public float fuerzaSalto = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;

    private bool enSuelo;
    private Rigidbody2D rb;

    [Header("References")]
    public Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Calcular desplazamiento (sin aceleración ni deceleración)
        float movement = horizontalInput * moveSpeed * Time.deltaTime;

        // Mover el personaje
        transform.position += new Vector3(movement, 0, 0);

        // Animación y flip del sprite
        
        if (horizontalInput != 0)
        {
            // Actualizar la animación con la velocidad (sin suavizado)

            animator.SetFloat("movement", Mathf.Abs(horizontalInput)); // Siempre 1 o 0

            // Voltear el sprite según la dirección
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);
        }
        else
        {
            // Detener la animación si no hay input
            animator.SetFloat("movement", 0);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
        enSuelo = hit.collider != null;

        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }
        animator.SetBool("ensuelo", enSuelo);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    private float movimientoHorizontal = 5f;
    [SerializeField] private float velocidadDeMovimiento;
    [Range(0, 0.3f)][SerializeField] private float suavizadoDeMovimiento;

    private bool mirandoDerecha = true;
    private Vector3 velocidad = Vector3.zero;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;

    public bool recibiendoDanio; // activar y desactivar la animación de daño del animator
    private bool muerto;

    [Header("Animación")]
    private Animator animator;
    private Rigidbody2D rb2D;

    // Referencia al script CombateJugador
    private CombateJugador combateJugador;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        combateJugador = GetComponent<CombateJugador>(); // Obtener la referencia al script CombateJugador
    }

    private void Update()
    {
        if (!muerto)
        {
            movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;
            animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

            if (Input.GetButtonDown("Jump"))
            {
                salto = true;
            }
        }
    }

    public void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("enSuelo", enSuelo);
        animator.SetBool("recibeDanio", recibiendoDanio);
        animator.SetBool("muerto", muerto);

        // Mover
        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.linearVelocity.y);
        rb2D.linearVelocity = Vector3.SmoothDamp(rb2D.linearVelocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if (!recibiendoDanio)
        {
            if (mover > 0 && !mirandoDerecha)
            {
                // Girar
                Girar();
            }
            else if (mover < 0 && mirandoDerecha)
            {
                // Girar
                Girar();
            }
        }

        if (enSuelo && saltar && !recibiendoDanio)
        {
            enSuelo = false;
            rb2D.AddForce(new Vector2(5f, fuerzaSalto));
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    public void RecibeDanio(Vector2 direccion, float cantDanio)
    {
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            combateJugador.TomarDaño(cantDanio); // Llamar al método de daño en CombateJugador
            if (combateJugador.vida <= 0) // Verificar si el jugador está muerto
            {
                muerto = true;
            }
        }
    }

    public void DesactivaDanio()
    {
        recibiendoDanio = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}

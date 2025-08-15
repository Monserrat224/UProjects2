using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jefe : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb2D;
    public Transform jugador;
    public bool mirandoDerecha = true;

    [Header("Vida")]
    [SerializeField] private float vida;
    [SerializeField] private BarraDeVida barraDeVida;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float da�oAtaque;

    [Header("Cambio de Escena")]
    [SerializeField] private string nombreEscenaSiguiente; // Nombre de la escena a cargar

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        barraDeVida.InicializarBarraDeVida(vida);
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        MirarJugador(); // Llama a MirarJugador en cada actualizaci�n
    }

    public void TomarDa�o(float da�o)
    {
        vida -= da�o;
        barraDeVida.CambiarVidaActual(vida);

        if (vida <= 0)
        {
            animator.SetTrigger("Muerte");
            PasarDeNivel();
        }
    }

    public void Muerte()
    {
        Destroy(gameObject);
    }

    private void MirarJugador()
    {
        if ((jugador.position.x > transform.position.x && !mirandoDerecha) || (jugador.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, mirandoDerecha ? 0 : 180, 0); // Cambia la rotaci�n correctamente
        }
    }

    public void Ataque()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Player")) // Aseg�rate de que la etiqueta sea "Player" (con may�scula)
            {
                colision.gameObject.GetComponent<CombateJugador>().TomarDa�o(da�oAtaque);
            }
        }
    }

    private void PasarDeNivel()
    {
        Debug.Log("�Enemigo derrotado! Pasando al siguiente nivel...");
        // Cargar la escena especificada en el Inspector
        SceneManager.LoadScene(nombreEscenaSiguiente);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}

using UnityEngine;

public class JefeCaminarBehaviour : StateMachineBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float tiempoBase;
    private float tiempoSeguir;
    private Transform jugador;
    private SeguirJugador seguirJugador;
    private float posicionYInicial;  // Guardaremos la posición Y inicial

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tiempoSeguir = tiempoBase;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        seguirJugador = animator.gameObject.GetComponent<SeguirJugador>();
        posicionYInicial = animator.transform.position.y;  // Guardamos la posición Y inicial
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Solo moverse horizontalmente manteniendo la misma posición Y
        Vector2 nuevaPosicion = new Vector2(
            Mathf.MoveTowards(animator.transform.position.x, jugador.position.x, velocidadMovimiento * Time.deltaTime),
            posicionYInicial  // Mantenemos la misma posición Y
        );

        animator.transform.position = nuevaPosicion;
        seguirJugador.Girar(jugador.position);
        tiempoSeguir -= Time.deltaTime;

        // Se ha eliminado la línea que establece el trigger "Volver"
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0)
        {
            GolpeC();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }

        if (Input.GetButtonDown("Fire2") && tiempoSiguienteAtaque <= 0)
        {
            GolpeV();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }

        if (Input.GetButtonDown("Fire3") && tiempoSiguienteAtaque <= 0)
        {
            GolpeB();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }

    private void GolpeC()
    {
        animator.SetTrigger("GolpeC");
        RealizarGolpe();
    }

    private void GolpeV()
    {
        animator.SetTrigger("GolpeV");
        RealizarGolpe();
    }

    private void GolpeB()
    {
        animator.SetTrigger("GolpeB");
        RealizarGolpe();
    }

    private void RealizarGolpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<Jefe>().TomarDaño(dañoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
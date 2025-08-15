using UnityEngine;

public class AnimationChanger : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
	     
    }

    public void ActivarSiguienteAnimacion()
    {
		
        animator.SetTrigger("Siguiente");
    }
}

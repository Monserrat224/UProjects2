using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class BloqueDialogo
{
    public string[] lineas;
}

public class DialogueScripts : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public List<BloqueDialogo> bloquesDeDialogo;
    public float textSpeed = 0.01f;

    private int bloqueActual = 0;
    private int index = 0;
    private string[] lines;
    private bool avanceAutomaticoTerminado = false;

    void Start()
    {
        dialogueText.text = string.Empty;
        if (bloquesDeDialogo.Count > 0)
        {
            CargarBloque(bloqueActual);
        }
    }

    void CargarBloque(int bloque)
{
    lines = bloquesDeDialogo[bloque].lineas;
    index = 0;
    dialogueText.text = string.Empty;

    avanceAutomaticoTerminado = false;
    StartCoroutine(AvanzarAutomatico());
}


    IEnumerator AvanzarAutomatico()
{
    while (index < lines.Length)
    {
        yield return StartCoroutine(WriteLine());
        yield return new WaitForSeconds(1f);  // Tiempo antes de la siguiente línea
        index++;
        if (index < lines.Length)
        {
            dialogueText.text = string.Empty;
        }
    }
    avanceAutomaticoTerminado = true;
}


    public void BotonSiguiente()
{
    if (index >= lines.Length)
    {
        // En vez de solo mostrar warning, pasamos al siguiente bloque o terminamos
        bloqueActual++;
        if (bloqueActual < bloquesDeDialogo.Count)
        {
            CargarBloque(bloqueActual);
        }
        else
        {
            gameObject.SetActive(false);
        }
        return; // Salimos de la función para no continuar con el código
    }

    if (!avanceAutomaticoTerminado)
    {
        return;
    }

    if (dialogueText.text != lines[index])
    {
        StopAllCoroutines();
        dialogueText.text = lines[index];
        return;
    }

    if (index < lines.Length - 1)
    {
        index++;
        dialogueText.text = string.Empty;
        StartCoroutine(WriteLine());
    }
    else
    {
        bloqueActual++;
        if (bloqueActual < bloquesDeDialogo.Count)
        {
            CargarBloque(bloqueActual);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}


    IEnumerator WriteLine()
    {
        if (index >= lines.Length)
        {
            yield break;
        }

        dialogueText.text = string.Empty;
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}

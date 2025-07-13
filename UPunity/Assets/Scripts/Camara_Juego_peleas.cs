using UnityEngine;

public class FightingCamera2D : MonoBehaviour
{
    [Header("Targets")]
    public Transform player1;
    public Transform player2;

    [Header("Camera Settings")]
    public float minZoom = 5f;
    public float maxZoom = 10f;
    public float zoomSpeed = 2f;
    public float followSpeed = 10f;
    public float edgePadding = 1f; // Espacio adicional alrededor de los personajes

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null) return;

        // Calcular el punto medio
        Vector3 midpoint = (player1.position + player2.position) * 0.5f;

        // Calcular distancia entre jugadores para el zoom
        float distance = Vector3.Distance(player1.position, player2.position);
        float targetZoom = Mathf.Clamp(distance * 0.7f, minZoom, maxZoom);

        // Suavizar el zoom
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);

        // Calcular nueva posición de cámara
        Vector3 targetPosition = new Vector3(
            midpoint.x,
            midpoint.y,
            transform.position.z
        );

        // Movimiento suavizado pero rápido
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Asegurar que ambos personajes estén visibles
        KeepBothPlayersVisible();
    }

    void KeepBothPlayersVisible()
    {
        // Convertir posiciones de jugadores a viewport
        Vector3 player1Viewport = cam.WorldToViewportPoint(player1.position);
        Vector3 player2Viewport = cam.WorldToViewportPoint(player2.position);

        // Si algún jugador se está saliendo de la pantalla
        bool needAdjustment = player1Viewport.x < edgePadding || 
                            player1Viewport.x > 1 - edgePadding ||
                            player1Viewport.y < edgePadding || 
                            player1Viewport.y > 1 - edgePadding ||
                            player2Viewport.x < edgePadding || 
                            player2Viewport.x > 1 - edgePadding ||
                            player2Viewport.y < edgePadding || 
                            player2Viewport.y > 1 - edgePadding;

        if (needAdjustment)
        {
            // Ajustar rápidamente para mantener a ambos en pantalla
            followSpeed = 15f;
        }
        else
        {
            followSpeed = 8f;
        }
    }
}



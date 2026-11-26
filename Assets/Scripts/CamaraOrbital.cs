using UnityEngine;

public class CamaraOrbital : MonoBehaviour
{
    [SerializeField] private Transform objetivo;
    [SerializeField] private float sensibilidad = 3f;
    [SerializeField] private float minVertical = -20f;
    [SerializeField] private float maxVertical = 60f;
    [SerializeField] private float alturaPivote = 1.5f;
    [SerializeField] private LayerMask capasColision = ~0;

    private float horizontal;
    private float vertical;
    private Vector3 offsetLocal;

    void Start()
    {
        if (objetivo == null)
        {
            Debug.LogError("CamaraOrbital: falta asignar el campo Objetivo.");
            enabled = false;
            return;
        }

        Vector3 euler = transform.eulerAngles;
        horizontal = euler.y;
        vertical = euler.x > 180f ? euler.x - 360f : euler.x;

        // Offset de la cámara respecto al pivote, "des-rotado"
        Vector3 pivote = objetivo.position + Vector3.up * alturaPivote;
        offsetLocal = Quaternion.Inverse(Quaternion.Euler(vertical, horizontal, 0f))
                      * (transform.position - pivote);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // El mouse solo rota si el cursor está bloqueado,
        // pero la cámara SIEMPRE sigue al personaje
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            horizontal += Input.GetAxis("Mouse X") * sensibilidad;
            vertical -= Input.GetAxis("Mouse Y") * sensibilidad;
            vertical = Mathf.Clamp(vertical, minVertical, maxVertical);
        }

        Quaternion rotacion = Quaternion.Euler(vertical, horizontal, 0f);
        Vector3 pivote = objetivo.position + Vector3.up * alturaPivote;
        Vector3 deseada = pivote + rotacion * offsetLocal;

        // Colisión con paredes
        Vector3 dir = deseada - pivote;
        float dist = dir.magnitude;
        if (Physics.SphereCast(pivote, 0.2f, dir.normalized, out RaycastHit hit, dist, capasColision, QueryTriggerInteraction.Ignore))
        {
            deseada = pivote + dir.normalized * hit.distance;
        }

        transform.position = deseada;
        transform.rotation = rotacion;
    }
}
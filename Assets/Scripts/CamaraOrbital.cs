using UnityEngine;

public class CamaraOrbital : MonoBehaviour
{
    [SerializeField] private Transform objetivo;          // el personaje
    [SerializeField] private float distancia = 5f;
    [SerializeField] private float alturaPivote = 1.6f;   // punto al que mira (cabeza/pecho)
    [SerializeField] private float sensibilidad = 3f;
    [SerializeField] private float minVertical = -20f;
    [SerializeField] private float maxVertical = 60f;
    [SerializeField] private LayerMask capasColision = ~0;

    private float horizontal;
    private float vertical = 15f;

    void Start()
    {
        horizontal = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Cursor.lockState != CursorLockMode.Locked) return;

        horizontal += Input.GetAxis("Mouse X") * sensibilidad;
        vertical -= Input.GetAxis("Mouse Y") * sensibilidad;
        vertical = Mathf.Clamp(vertical, minVertical, maxVertical);

        Quaternion rotacion = Quaternion.Euler(vertical, horizontal, 0f);
        Vector3 pivote = objetivo.position + Vector3.up * alturaPivote;
        Vector3 direccion = rotacion * Vector3.back;

        // Si hay una pared atrás, acerca la cámara para no atravesarla
        float d = distancia;
        if (Physics.SphereCast(pivote, 0.2f, direccion, out RaycastHit hit, distancia, capasColision, QueryTriggerInteraction.Ignore))
        {
            d = hit.distance;
        }

        transform.position = pivote + direccion * d;
        transform.rotation = rotacion;
    }
}
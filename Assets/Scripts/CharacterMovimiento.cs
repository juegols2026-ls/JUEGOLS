using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovimiento : MonoBehaviour
{
    [SerializeField] private bool UsarGetAxisRaw;
    [SerializeField] private Transform camara;
    [SerializeField] private Animator animator;
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private float velocidadRotacion = 12f;
    [SerializeField] private float gravedad = -20f;

    private CharacterController controlador;
    private float velocidadVertical;

    void Awake()
    {
        controlador = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (camara == null && Camera.main != null) camara = Camera.main.transform;
    }

    void Update()
    {
        float ejeH = UsarGetAxisRaw ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
        float ejeV = UsarGetAxisRaw ? Input.GetAxisRaw("Vertical") : Input.GetAxis("Vertical");

        Vector3 adelante = camara.forward;
        Vector3 derecha = camara.right;
        adelante.y = 0f;
        derecha.y = 0f;
        adelante.Normalize();
        derecha.Normalize();

        Vector3 direccion = derecha * ejeH + adelante * ejeV;
        float intensidad = Mathf.Clamp01(direccion.magnitude);

        if (direccion.sqrMagnitude > 0.0001f)
        {
            direccion.Normalize();
            // El personaje gira hacia donde camina
            Quaternion destino = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, destino, velocidadRotacion * Time.deltaTime);
        }

        // Gravedad
        if (controlador.isGrounded && velocidadVertical < 0f) velocidadVertical = -2f;
        velocidadVertical += gravedad * Time.deltaTime;

        Vector3 movimiento = direccion * (velocidadMovimiento * intensidad);
        movimiento.y = velocidadVertical;
        controlador.Move(movimiento * Time.deltaTime);

        // Animación
        if (animator != null)
        {
            animator.SetFloat("Velocidad", intensidad, 0.1f, Time.deltaTime);
        }
    }
}
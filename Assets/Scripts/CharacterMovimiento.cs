using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovimiento : MonoBehaviour
{
    [SerializeField] private Transform camara;
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private float gravedad = -20f;

    [SerializeField] private float rotationSpeed = 250;

    public Animator animation;
    float x, z;

    public CharacterController cc;
    private float velY;

    void Awake() => cc = GetComponent<CharacterController>();

    void Update()
    {
         x = Input.GetAxisRaw("Horizontal"); // A/D (strafe)
         z = Input.GetAxisRaw("Vertical");   // W/S

        // El personaje siempre mira hacia donde mira la cámara
        transform.rotation = Quaternion.Euler(0f, camara.eulerAngles.y, 0f);

        // Movimiento relativo al personaje (que ya está alineado con la cámara)
        Vector3 dir = (transform.forward * z + transform.right * x).normalized;

        velY = cc.isGrounded ? -2f : velY + gravedad * Time.deltaTime;

        Vector3 mov = dir * velocidad;
        mov.y = velY;
        cc.Move(mov * Time.deltaTime);

        animation.SetFloat("velX",x);
        animation.SetFloat("velZ",z);
    }
}
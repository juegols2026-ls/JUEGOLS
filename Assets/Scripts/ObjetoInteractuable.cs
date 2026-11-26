using UnityEngine;
using UnityEngine.Events;

public class ObjetoInteractuable : MonoBehaviour
{
    public enum TipoAccion
    {
        Quitar,
        Empujar,
        Personalizada
    }

    [SerializeField] private TipoAccion accion;

    [Tooltip("Nombre exacto del Trigger en el Animator del jugador.")]
    [SerializeField] private string triggerAnimacion = "";

    [Header("Empujar")]
    [SerializeField] private Rigidbody cuerpo;
    [SerializeField] private float impulso = 3f;

    [Header("Acción personalizada")]
    [SerializeField] private UnityEvent alInteractuar;

    public string TriggerAnimacion => triggerAnimacion;

    private void Reset()
    {
        cuerpo = GetComponent<Rigidbody>();
    }

    public bool Interactuar(Transform jugador)
    {
        switch (accion)
        {
            case TipoAccion.Quitar:
                gameObject.SetActive(false);
                return true;

            case TipoAccion.Empujar:
                if (cuerpo == null || cuerpo.isKinematic)
                    return false;

                Vector3 direccion =
                    cuerpo.worldCenterOfMass - jugador.position;

                direccion.y = 0f;

                if (direccion.sqrMagnitude < 0.001f)
                    return false;

                cuerpo.AddForce(
                    direccion.normalized * impulso,
                    ForceMode.Impulse
                );

                return true;

            case TipoAccion.Personalizada:
                alInteractuar.Invoke();
                return true;
        }

        return false;
    }
}

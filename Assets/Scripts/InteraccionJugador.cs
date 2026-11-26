using UnityEngine;
using UnityEngine.InputSystem;

public class InteraccionJugador : MonoBehaviour
{
    [SerializeField] private Key tecla = Key.E;
    [SerializeField] private float alcance = 1.8f;
    [SerializeField] private float pausaEntreAcciones = 0.4f;

    [SerializeField] private LayerMask capaInteractuables;
    [SerializeField] private LayerMask capaObstaculos;

    [SerializeField] private Animator animador;

    private float siguienteInteraccion;

    private Vector3 Origen =>
        transform.position + Vector3.up * 0.9f;

    private void Update()
    {
        Keyboard teclado = Keyboard.current;

        if (teclado == null ||
            !teclado[tecla].wasPressedThisFrame ||
            Time.time < siguienteInteraccion)
        {
            return;
        }

        ObjetoInteractuable objeto = BuscarMasCercano();

        if (objeto == null)
            return;

        
        string trigger = objeto.TriggerAnimacion;

        if (!objeto.Interactuar(transform))
            return;

        siguienteInteraccion = Time.time + pausaEntreAcciones;

        if (animador != null && !string.IsNullOrWhiteSpace(trigger))
            animador.SetTrigger(trigger);
    }

    private ObjetoInteractuable BuscarMasCercano()
    {
        Collider[] encontrados = Physics.OverlapSphere(
            Origen,
            alcance,
            capaInteractuables,
            QueryTriggerInteraction.Ignore
        );

        ObjetoInteractuable mejor = null;
        float menorDistancia = float.PositiveInfinity;

        foreach (Collider col in encontrados)
        {
            ObjetoInteractuable candidato =
                col.GetComponentInParent<ObjetoInteractuable>();

            if (candidato == null || !candidato.isActiveAndEnabled)
                continue;

            Vector3 punto = col.ClosestPoint(Origen);
            float distancia = (punto - Origen).sqrMagnitude;

            if (distancia >= menorDistancia)
                continue;

            // Impide interactuar a través de paredes.
            bool bloqueado = Physics.Linecast(
                Origen,
                punto,
                capaObstaculos,
                QueryTriggerInteraction.Ignore
            );

            if (bloqueado)
                continue;

            mejor = candidato;
            menorDistancia = distancia;
        }

        return mejor;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Origen, alcance);
    }
}
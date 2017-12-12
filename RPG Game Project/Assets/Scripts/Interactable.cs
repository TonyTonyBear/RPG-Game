using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

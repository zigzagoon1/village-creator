using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform interactionTransform;
    public float radius = 3f;

    private bool isFocus = false;
    private Transform player;
    private bool hasInteracted = false;
    public virtual void Interact()
    {
        Debug.Log("Interacting with" + transform.name);
    }
    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            //check if player is close enough to object to interact
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    public void OnDefocus()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
        if (interactionTransform == null)
        {
            interactionTransform = this.transform;
        }
    }
}

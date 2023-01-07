using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{

    public float radius = 3f;
    public Transform interactionTransfor = null;

    bool isFocus;
    Transform player;
    bool hasInteracted = false;

    private void OnDrawGizmosSelected()
    {
        if (interactionTransfor == null)
            interactionTransfor = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransfor.position, radius);
    }    

    public void Start()
    {
        OnDefocused();
    }
    
    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransfor.position);

            if (distance <= radius)
            {
                Interact();                
                hasInteracted = true;
            }
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    public void OnFocused(Transform playerFocused)
    {
        isFocus = true;
        player = playerFocused;
        hasInteracted = false;
    }
}

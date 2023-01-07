using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseController : MouseController
{
    public override void MouseButtonLeftAction()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, movementMask))
        {
            motor.MoveToPoint(hit.point);

            RemoveFocus();

            InteractableController interactable = hit.collider.GetComponent<InteractableController>();

            if (interactable != null)
            {
                SetFocus(interactable);
            }
        }
    }

    public override void MouseButtonRightAction()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            InteractableController interactable = hit.collider.GetComponent<InteractableController>();

            if (interactable != null)
            {
                SetFocus(interactable);
            }
        }
    }
}

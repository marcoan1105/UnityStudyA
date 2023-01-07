using Assets.Assets.Script.Configuration;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MouseMotor))]
public abstract class MouseController : MonoBehaviour
{

    protected Camera cam;
    protected MouseMotor motor;

    [SerializeField]
    protected InteractableController focus;

    [SerializeField]
    protected LayerMask movementMask;

    [SerializeField]
    protected float offset = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<MouseMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())        
            return;        

        if (Input.GetMouseButton(ButtonConfiguration.MouseButtonLeft))
        {
            MouseButtonLeftAction();            
        }

        if (Input.GetMouseButton(ButtonConfiguration.MouseButtonRight))
        {
            MouseButtonRightAction();            
        }
    }

    public abstract void MouseButtonLeftAction();
    public abstract void MouseButtonRightAction();

    protected void SetFocus(InteractableController newFocus)
    {
        if (newFocus != focus)
        {
            if(focus != null)
                focus.OnDefocused();

            focus = newFocus;            
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }


    protected void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }
}

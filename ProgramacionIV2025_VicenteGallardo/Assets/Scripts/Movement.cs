using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    public InputActionAsset inputActions;

    private Player player;
    private InputAction m_moveAction;

    private Vector2 movePos;
    private Vector2 moveRotate;

    private Rigidbody2D m_rigidbody2D;

    public float spd;
    public float rotateSpd;



    private void OnEnable()
    {
      inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
       player = GetComponent<Player>();
       m_moveAction = InputSystem.actions.FindAction("Move");    
       m_rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        movePos = m_moveAction.ReadValue<Vector2>();  
       
    }

    private void FixedUpdate()
    {
        Moving();
        Rotating();
    }

    private void Moving()
    {
        m_rigidbody2D.MovePosition(m_rigidbody2D.position + (Vector2)transform.up * movePos.y * spd * Time.deltaTime);
    }

    private void Rotating()
    {
        if(movePos.x != 0)
        {
            float rotationAmount = -movePos.x * rotateSpd * Time.deltaTime;
            transform.Rotate(0,0,rotationAmount);   
        }
    }
}

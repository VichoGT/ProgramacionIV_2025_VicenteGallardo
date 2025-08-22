using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
   public InputActionAsset inputActions;


    private InputAction m_moveAction;

    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;

    private Rigidbody2D m_rigidbody;

    public float spd = 5f;
    public float rotateSpd = 5f;



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
       m_moveAction = InputSystem.actions.FindAction("Move");    
       m_rigidbody = GetComponent<Rigidbody2D>();



    }


    private void Update()
    {
     
        m_moveAmt = m_moveAction.ReadValue<Vector2>();  
       

    }

    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }

    private void Walking()
    {
        m_rigidbody.MovePosition(m_rigidbody.position + (Vector2)transform.up * m_moveAmt.y * spd * Time.deltaTime);
    }

    private void Rotating()
    {
        if(m_moveAmt.x != 0)
        {
            float rotationAmount = -m_moveAmt.x * rotateSpd * Time.deltaTime;
            transform.Rotate(0,0,rotationAmount);
            
           
        }
    }












}

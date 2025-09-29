using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    //public GroundSensor groundSensor;

    private InputAction _moveAction;
    private Vector2 _moveInput;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _interactAction;
    [SerializeField] private float _playerVelocity = 5;
    [SerializeField] private float _jumpForce = 4;
    private bool _alreadyLanded = true;

    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private Vector2 _sensorSize = new Vector2(0.5f, 0.5f);
    [SerializeField] private Vector2 _interactionZone = new Vector2(1, 1);

    [SerializeField] private int _currentHealth = 10;
    [SerializeField] private int _maxHealth = 10;



  



    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _attackAction = InputSystem.actions["Attack"];
        _interactAction = InputSystem.actions["Interact"];
        //groundSensor = GetComponentInChildren<GroundSensor>();
        // //_attackAction = InputSystem.actions.FindAction("Attack");
    }

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {



        //Tarea if(isAttaking) return;


        _moveInput = _moveAction.ReadValue<Vector2>();
        //Debug.Log(_moveInput);

        //transform.position = transform.position + new Vector3(_moveInput.x, 0, 0) * _playerVelocity * Time.deltaTime;

        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }

        if (_interactAction.WasPerformedThisFrame())
        {
            Interact();
        }

        Movement();

        _animator.SetBool("IsJumping", !IsGrounded());
    }

    void FixedUpdate()
    {

        Move();

    }

    void Movement()
    {
        if (_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _animator.SetBool("IsRunning", true);
        }
        else if (_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }

    void Jump()
    {

        _rigidBody2D.AddForce(transform.up * Mathf.Sqrt(_jumpForce * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);

        //Debug.Log("Salto");
    }

    void Interact()
    {
        //Debug.Log("haciendo cosas");
        Collider2D[] interactables = Physics2D.OverlapBoxAll(transform.position, _interactionZone, 0);
        foreach (Collider2D item in interactables)
        {
            if (item.gameObject.tag == "Star")
            {
                Star starScript = item.gameObject.GetComponent<Star>();
                if (starScript != null)
                {
                    starScript.Interection();
                }
            }
        }
    }

    void Move()
    {
        _rigidBody2D.linearVelocity = new Vector2(_moveInput.x * _playerVelocity, _rigidBody2D.linearVelocityY);
    }

    bool IsGrounded()
    {
        Collider2D[] ground = Physics2D.OverlapBoxAll(_sensorPosition.position, _sensorSize, 0);
        foreach (Collider2D item in ground)
        {
            if (item.gameObject.layer == 3)
            {

                return true;
            }
        }

        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_sensorPosition.position, _sensorSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, _interactionZone);
    }

    public void TakeDamage(int damage)
    {

        Debug.Log("He recibido daño");
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Estas Muerto");
    }
}

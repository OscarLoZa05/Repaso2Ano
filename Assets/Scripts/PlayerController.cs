using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody2D _rigidBody2D;
    //public GroundSensor groundSensor;

    private InputAction _moveAction;
    private Vector2 _moveInput;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    [SerializeField] private float _playerVelocity = 5;
    private float _jumpForce = 4;

    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private Vector2 _sensorSize = new Vector2(0.5f, 0.5f);



    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        //_attackAction = InputSystem.actions.FindAction("Attack");
        _attackAction = InputSystem.actions["Attack"];
        //groundSensor = GetComponentInChildren<GroundSensor>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        _moveInput = _moveAction.ReadValue<Vector2>();
        //Debug.Log(_moveInput);

        //transform.position = transform.position + new Vector3(_moveInput.x, 0, 0) * _playerVelocity * Time.deltaTime;

        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {

        Move();

    }

    void Jump()
    {

        _rigidBody2D.AddForce(transform.up * Mathf.Sqrt(_jumpForce * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);

        //Debug.Log("Salto");
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
    }



}

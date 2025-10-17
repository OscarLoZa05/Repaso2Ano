using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private AudioSource _audioSource;
    public AudioClip jumpSFX;
    public AudioClip attackSFX;
    public AudioClip runAttackSFX;
    public AudioClip damageSFX;
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

    [SerializeField] private float _currentHealth = 10;
    [SerializeField] private float _maxHealth = 10;

    [SerializeField] private Transform _attackHitbox;
    [SerializeField] private float _attackZone = 0.5f;

    [SerializeField] private bool _isAttacking = false;

    [SerializeField] private int normalAttackDamage = 2;
    [SerializeField] private int runAttackDamage = 1;







    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

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


        if (_isAttacking == true)
        {
            return;
        }



        //Tarea if(isAttaking) return;


            _moveInput = _moveAction.ReadValue<Vector2>();
        //Debug.Log(_moveInput);

        //transform.position = transform.position + new Vector3(_moveInput.x, 0, 0) * _playerVelocity * Time.deltaTime;

        if (_attackAction.WasPressedThisFrame() && _moveInput.x == 0 && IsGrounded())
        {
            _isAttacking = true;
            _animator.SetTrigger("IsAttacking");
        }
        if(_attackAction.WasPressedThisFrame() && _moveInput.x != 0 && IsGrounded())
        {          
            _animator.SetTrigger("IsRunAttack");
        }

        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
            {
                Jump();
            }

        if (_interactAction.WasPressedThisFrame())
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
        _audioSource.PlayOneShot(jumpSFX);

        //Debug.Log("Salto");
    }

    void Interact()
    {
        //Debug.Log("haciendo cosas");
        Collider2D[] interactables = Physics2D.OverlapBoxAll(transform.position, _interactionZone, 0);
        foreach (Collider2D item in interactables)
        {
            if (item.gameObject.layer == 9)
            {
                IInteratable interactable = item.gameObject.GetComponent<IInteratable>();
                if (interactable != null)
                {
                    interactable.Interact();
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

    public void TakeDamage(int damage)
    {
        _audioSource.PlayOneShot(damageSFX);
        _currentHealth -= damage;
        GUIManager.Instance.UpdateHealthBar(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        _animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public void NormalAttack()
    {
        _audioSource.PlayOneShot(attackSFX);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackHitbox.position, _attackZone);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject.layer == 8)
            {
                IDamageable damageable = enemy.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.DamageEnemy(normalAttackDamage);
                }

                
            }
        }
    }

    public void MovementAttack()
    {
        _audioSource.PlayOneShot(runAttackSFX);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackHitbox.position, _attackZone);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject.layer == 8)
            {
                IDamageable damageable = enemy.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.DamageEnemy(runAttackDamage);
                }
            }
        }
    }

    public void FinishAttack()
    {
        _isAttacking = false;
    }

    public void Heal(float heal)
    {
        _currentHealth += heal;
        if(_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        GUIManager.Instance.UpdateHealthBar(_currentHealth, _maxHealth);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_sensorPosition.position, _sensorSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, _interactionZone);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_attackHitbox.position, _attackZone);
    }
}

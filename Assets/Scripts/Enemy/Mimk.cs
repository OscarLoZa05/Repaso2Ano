using UnityEngine;
using System.Collections;

public class Mimk : MonoBehaviour, IDamageable
{

    [SerializeField] private int mimikSpeed = 11;
    [SerializeField] private int mimikDamage = 2;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    public Vector2 _attackZone = new Vector2(0.5f, 0.5f);

    public float cooldawnAttack = 2;
    public float timerAttack = 2;
    public int mimikDirection = 1;
    public int mimikLife = 6;

    public Transform mimikSensor;
    public Vector2 groundSize = new Vector2(0.1f, 0.1f);

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private AudioSource _audioSource;
    public AudioClip _deathMimikSFX;


    //private PlayerController _playerController;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        //_playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    void Update()
    {
        _rigidBody2D.linearVelocity = new Vector2(mimikSpeed * mimikDirection, _rigidBody2D.linearVelocityY);

        cooldawnAttack += 1 * Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            mimikDirection *= -1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mimikDirection *= -1;
            if (cooldawnAttack >= timerAttack)
            {
                _animator.SetTrigger("IsAttacking");
                PlayerController _playerController = collision.gameObject.GetComponent<PlayerController>();
                _playerController.TakeDamage(mimikDamage);
                cooldawnAttack = 0;

            }
        }
    }

    public void DamageEnemy(int damage)
    {
        mimikLife -= damage;
        Debug.Log(mimikLife);
        if(mimikLife <= 0)
        {
            StartCoroutine(DeathMimik());
        }

    }

    public IEnumerator DeathMimik()
    {
        _spriteRenderer.enabled = false;
        _rigidBody2D.gravityScale = 0;
        _boxCollider.enabled = false;
        mimikSpeed = 0;
        _audioSource.PlayOneShot(_deathMimikSFX);
        //_audioSource.PlayOneShot(_deathMimikSFX);

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }






}

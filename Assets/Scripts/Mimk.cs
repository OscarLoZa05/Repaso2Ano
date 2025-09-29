using UnityEngine;
using UnityEngine.AI;

public class Mimk : MonoBehaviour
{

    [SerializeField] private int mimikSpeed = 11;
    [SerializeField] private int mimikDamage = 2;
    private Rigidbody2D _rigidBody2D;
    public Vector2 _attackZone = new Vector2(0.5f, 0.5f);

    public float cooldawnAttack = 2;
    public float timerAttack = 2;
    public int mimikDirection = 1;

    public Transform mimikSensor;
    public Vector2 groundSize = new Vector2(0.1f, 0.1f);


    //private PlayerController _playerController;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
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

    void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && cooldawnAttack >= timerAttack)
        {
            PlayerController _playerController = collision.gameObject.GetComponent<PlayerController>();
            _playerController.TakeDamage(mimikDamage);
            cooldawnAttack = 0;
        }
        
    }

    





}

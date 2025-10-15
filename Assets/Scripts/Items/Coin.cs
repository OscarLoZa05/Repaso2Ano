using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider;
    private AudioSource _audioSource;
    public AudioClip _coinSFX;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ClaimCoin());
        }
    }
    
    public IEnumerator ClaimCoin()
    {
        GameManager.instance.AddCoin();
        _spriteRenderer.enabled = false;
        _circleCollider.enabled = false;
        _audioSource.PlayOneShot(_coinSFX);
        GUIManager.Instance.CoinText();

        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}

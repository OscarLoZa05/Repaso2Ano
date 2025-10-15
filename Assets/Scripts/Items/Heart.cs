using UnityEngine;

public class Heart : MonoBehaviour, IInteratable
{

    [SerializeField] AudioClip _heartSFX;
    private PlayerController _playerController;
    private float _heal = 1;

    void Awake()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    public void Interact()
    {
        AudioManager.instance.ReproduceSound(_heartSFX);
        _playerController.Heal(_heal);
        Destroy(gameObject);
    }

}

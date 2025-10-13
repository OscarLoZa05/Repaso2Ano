using UnityEngine;

public class Star : MonoBehaviour, IInteratable
{
    //GameManager _gameManager;

    [SerializeField] AudioClip _starSFX;

    void Awake()
    {
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Interact()
    {
        GameManager.instance.AddStar();
        AudioManager.instance.ReproduceSound(_starSFX);
        Destroy(gameObject);
    }


}

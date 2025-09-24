using UnityEngine;

public class Star : MonoBehaviour
{
    //GameManager _gameManager;

    [SerializeField] AudioClip _starSFX;

    void Awake()
    {
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Interection()
    {
        //_gameManager.AddStar();

        GameManager.instance.AddStar();
        AudioManager.instance.ReproduceSound(_starSFX);
        Destroy(gameObject);
    }


}

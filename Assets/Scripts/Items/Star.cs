using UnityEngine;

public class Star : MonoBehaviour, IInteratable
{

    [SerializeField] AudioClip _starSFX;
    private BoxCollider2D _boxCollider;

    void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Interact()
    {
        _boxCollider.enabled = false;
        GameManager.instance.AddStar();
        GameManager.instance.Victory();
        AudioManager.instance.ReproduceSound(_starSFX);
        GUIManager.Instance.StarText();
        //GameManager.instance.Victory();
        Destroy(gameObject);
    }

    //find ibjecto with tag


}

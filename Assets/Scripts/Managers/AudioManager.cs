using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }

    [SerializeField] private AudioSource _bmgSource;
    [SerializeField] private AudioSource _sfxSource;

    public AudioClip menuBGM;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {

    }

    public void ReproduceSound(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    public void ChangeBGM(AudioClip bgmClip)
    {
        _bmgSource.clip = bgmClip;
        _bmgSource.Play();
    }
}

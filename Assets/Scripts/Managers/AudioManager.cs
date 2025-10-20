using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }

    [SerializeField] private AudioSource _bmgSource;
    [SerializeField] private AudioSource _sfxSource;

    public AudioClip menuBGM;
    public AudioClip nivel1BGM;
    public AudioClip gameoverBGM;

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

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {

    }

    public void ReproduceSound(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    public void StopBGM()
    {
        _bmgSource.Stop();
    }
    public void PlayBGM()
    {
        _bmgSource.Play();
    }

    public void ChangeBGM(AudioClip bgmClip)
    {
        _bmgSource.clip = bgmClip;
        _bmgSource.Play();
    }
}

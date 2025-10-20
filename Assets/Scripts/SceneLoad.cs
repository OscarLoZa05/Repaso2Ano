using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{

    public static SceneLoad Instance;
    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Image _loadingBar;

    [SerializeField] private Text _loadingText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string sceneName)
    {
        GameManager.instance.stars = 0;
        GameManager.instance.coin = 0;
        StartCoroutine(LoadNewScene(sceneName));
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        yield return null;

        _loadingCanvas.SetActive(true);

        SceneManager.LoadSceneAsync(sceneName);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        asyncLoad.allowSceneActivation = false;

        float fakeLoadPercentage = 0;

        while (!asyncLoad.isDone)
        {
            //_loadingBar.fillAmount = asyncLoad.progress;

            fakeLoadPercentage += 0.01f;
            Mathf.Clamp01(fakeLoadPercentage);
            _loadingBar.fillAmount = fakeLoadPercentage;
            _loadingText.text = (fakeLoadPercentage * 100).ToString("F0") + "%";

            if (asyncLoad.progress >= 0.9f && fakeLoadPercentage >= 0.99f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return new WaitForSecondsRealtime(0.1f);
        }

        Time.timeScale = 1;
        _loadingCanvas.SetActive(false);
        GameManager.instance.playerInputs.FindActionMap("Player").Enable();
        GameManager.instance._isPaused = false;
        IsInMenu();

    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    public void IsInMenu()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            AudioManager.instance.StopBGM();
            AudioManager.instance.ChangeBGM(AudioManager.instance.menuBGM);
        }
        if (SceneManager.GetActiveScene().name == "Nivel 1")
        {
            AudioManager.instance.StopBGM();
            AudioManager.instance.ChangeBGM(AudioManager.instance.nivel1BGM);
        }
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            AudioManager.instance.StopBGM();
            AudioManager.instance.ChangeBGM(AudioManager.instance.gameoverBGM);
        }
    }
}

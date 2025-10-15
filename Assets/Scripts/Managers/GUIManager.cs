using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;
    public GameObject _pauseCanvas;
    public Image _healthBar;
    public Text starCount;
    public Text coinCount;

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

    }

    public void ChangeCanvasStatus(GameObject canvas, bool status)
    {
        canvas.SetActive(status);
    }

    public void Resume()
    {
        GameManager.instance.Pause();
    }

    public void UpdateHealthBar(float _currentHealth, float _maxHealth)
    {
        Debug.Log(_currentHealth / _maxHealth);
        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }

    public void ChangeScene(string sceneName)
    {
        SceneLoad.Instance.ChangeScene(sceneName);
    }

    public void StarText()
    {
        starCount.text = ": " + GameManager.instance.stars.ToString();
    }

    public void CoinText()
    {
        coinCount.text = ": " + GameManager.instance.coin.ToString();
    }


}

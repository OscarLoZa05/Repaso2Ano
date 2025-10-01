using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{

    //Singelton /*Sirve para ser facil de acceder a cialqioer cosa, y evitar que haya duplicados de ese objeto*/ 

    public static GameManager instance { get; private set; } //Solo prodra cambiar le valor desde dentro del script para modificat el valor

    private bool _isPaused = false;
    [SerializeField] private InputActionAsset playerInputs;
    private InputAction _pauseInput;

    private int _stars = 0;

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

        _pauseInput = InputSystem.actions["Pause"];
    }

    public void AddStar()
    {
        _stars++;
        Debug.Log("Estrella recogidas: " + _stars);
    }

    void Update()
    {
        if (_pauseInput.WasPressedThisFrame())
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (_isPaused)
        {
            _isPaused = false;
            Time.timeScale = 1;
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance._pauseCanvas, false);
            playerInputs.FindActionMap("Player").Enable();
        }
        else
        {
            _isPaused = true;
            Time.timeScale = 0;
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance._pauseCanvas, true);
            playerInputs.FindActionMap("Player").Disable();
        }

    }

    
}

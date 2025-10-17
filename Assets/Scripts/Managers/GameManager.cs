using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{

    //Singelton /*Sirve para ser facil de acceder a cialqioer cosa, y evitar que haya duplicados de ese objeto*/ 

    public static GameManager instance { get; private set; } //Solo prodra cambiar le valor desde dentro del script para modificat el valor

    public bool _isPaused = false;
    public InputActionAsset playerInputs;
    private InputAction pauseInput;

    public int stars;
    public int coin;
    public int maxStars;
    
    public int _starVictory = 0;

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

        pauseInput = InputSystem.actions["Pause"];
    }

    void Start()
    {
        GameObject[] estrellas = GameObject.FindGameObjectsWithTag("Star");
        maxStars = estrellas.Length;
    }

    public void AddStar()
    {
        stars++;
        Debug.Log("Estrella recogidas: " + stars);
    }

    public void AddCoin()
    {
        coin++;
        Debug.Log("Moneda recogidas: " + coin);
    }

    void Update()
    {
        if (pauseInput.WasPressedThisFrame())
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
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance.pauseCanvas, false);
            playerInputs.FindActionMap("Player").Enable();
        }
        else
        {
            _isPaused = true;
            Time.timeScale = 0;
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance.pauseCanvas, true);
            playerInputs.FindActionMap("Player").Disable();
        }

    }

    public void Victory()
    {
        if(stars == maxStars)
        {
            GUIManager.Instance.ChangeCanvasStatus(GUIManager.Instance.victoryCanvas, true);
            playerInputs.FindActionMap("Player").Disable();
            Time.timeScale = 0;
            
        }
        
    }
   
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject ui;
    public GameObject HUD;
    public GameObject BuildUI;

    public Button Resume;
    public Button Restart;
    public Button ExitButton;

    // Start is called before the first frame update
    void Start()
    {
        ui.SetActive(false);
        Resume.onClick.AddListener(Toggle);
        Restart.onClick.AddListener(Retry);
        ExitButton.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if(ui.activeSelf)
        {
            Time.timeScale = 0f;
            HUD.SetActive(false);
            BuildUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Debug.Log(Cursor.lockState);
        }
        else
        {
            Time.timeScale = 1f;
            HUD.SetActive(true);
            BuildUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log(Cursor.lockState);
        }

    }

    public void Retry()
    {
        Time.timeScale = 1f;
        GameManager.Instance.paused = false;
        SceneManager.LoadScene(1);

    }

    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }    
}

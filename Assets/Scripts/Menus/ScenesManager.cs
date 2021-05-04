using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    private Animator button;
    private bool highlighted;
    private GameManager gm;

    public void Start()
    {
        gm = GameManager.Instance;
        button = GetComponent<Animator>();
        button.SetBool("Default", true);
        highlighted = false;
    }

    public void onButtonHighlighted()
    {
        highlighted = !highlighted;
        button.SetBool("Highlighted", highlighted);
    }

    public void onPlayGameButtonClicked()
    {
        gm.ChangeScene("GameScene");
    }

    public void onMainMenuButtonClicked()
    {
        gm.ChangeScene("MainMenu");
    }

    public void onHowToPlayButtonClicked()
    {
        gm.ChangeScene("HowToPlay");
    }
    public void onCreditsButtonClicked()
    {
        gm.ChangeScene("Credits");
    }
    public void onQuitClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
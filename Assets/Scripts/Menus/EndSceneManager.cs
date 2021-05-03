using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if (GameManager.Instance.win)
            Debug.Log("Show Win Camera Location");
        else
            Debug.Log("Show Lose Camera Location");
    }
    public void ToCredits()
    {
        GameManager.Instance.ChangeScene("Credits");
    }
}

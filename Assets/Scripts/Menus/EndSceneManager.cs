using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    public Camera cam;
    public Animator anim;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if (GameManager.Instance.win)
        {
            anim.SetBool("FullSpin", true);

        }    
        else
        {
            anim.SetBool("Dead", true);
            cam.transform.position = cam.transform.position + new Vector3(0, 180, 0);
        }
            
    }
    public void ToCredits()
    {
        GameManager.Instance.ChangeScene("Credits");
    }
}

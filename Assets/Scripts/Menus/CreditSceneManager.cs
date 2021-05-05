using System.Collections.Generic;
using UnityEngine;

public class CreditSceneManager : MonoBehaviour
{
    public GameObject panel1, panel2, panel3;
    private List<GameObject> panelList;
    private int currPanel;
    // Start is called before the first frame update
    void Start()
    {
        panelList = new List<GameObject>() { panel1, panel2, panel3 };
        ResetPanels();
    }

    private void ResetPanels()
    {
        foreach (GameObject panel in panelList)
            panel.SetActive(false);
        currPanel = 0;
        panelList[0].SetActive(true);
    }

    public void Next()
    {
        Debug.Log("Hi");
        panelList[currPanel++].SetActive(false);
        if (currPanel < panelList.Count)
            panelList[currPanel].SetActive(true);
        else
            GameManager.Instance.ChangeScene("MainMenu");
    }
}

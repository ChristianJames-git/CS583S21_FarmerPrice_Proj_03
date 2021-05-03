using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Camera topDownCam;
    public Camera firstPersonCam;
    public bool inMap;

    private void Start()
    {
        topDownCam.enabled = false;
        firstPersonCam.enabled = true;
        inMap = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.M))
        {
            topDownCam.enabled = !topDownCam.enabled;
            firstPersonCam.enabled = !firstPersonCam.enabled;
            inMap = !inMap;
            GameManager.Instance.paused = inMap;

            if (inMap)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Debug.Log("Cursor should be unlocked");

                //set timescale to 0
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;

                //set time scale to 1
                Time.timeScale = 1;
            }
                
        }
    }
}

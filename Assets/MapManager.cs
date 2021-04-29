using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Camera topDownCam;
    public Camera firstPersonCam;
    bool inMap;

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
        }
    }
}

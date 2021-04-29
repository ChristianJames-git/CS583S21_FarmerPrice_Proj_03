using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Camera topDownCam;
    public Camera firstPersonCam;
    public bool inMap;
    //public List<GameObject> enemies;
    public GameObject player;

    private void Start()
    {
        topDownCam.enabled = false;
        firstPersonCam.enabled = true;
        inMap = false;

        //enemies = new List<GameObject>();
        //foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        //{
        //    enemies.Add(enemy);
        //}
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.M))
        {
            topDownCam.enabled = !topDownCam.enabled;
            firstPersonCam.enabled = !firstPersonCam.enabled;
            inMap = !inMap;

            if (inMap)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Debug.Log("Cursor should be unlocked");
            }
            else
            {
                
                Cursor.lockState = CursorLockMode.Locked;
            }

        }
    }
}

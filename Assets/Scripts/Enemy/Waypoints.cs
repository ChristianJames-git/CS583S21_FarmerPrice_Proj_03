using UnityEngine;

public class Waypoints : MonoBehaviour
{
    //static so objects can access it without a direct reference
    public static Transform[] waypoints;

    private void Awake()
    {
        waypoints = new Transform[transform.childCount];

        //go through all the children of this object and add them to the array in order
        for (int i = 0; i < waypoints.Length; i++)
            waypoints[i] = transform.GetChild(i);
    }
}

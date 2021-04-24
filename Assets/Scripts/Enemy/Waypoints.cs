using UnityEngine;

public class Waypoints : MonoBehaviour
{
    //static so objects can access it without a direct reference
    public static Transform[] points;
    public Transform Waypoint;

    private void Awake()
    {
        for (int i = 1, j = 0; i < GameManager.mapZ; i += 2, j++)
        {
            if (j % 2 == 0)
                Instantiate(Waypoint, gameObject.transform).position = new Vector3(0, 0, i);
            Instantiate(Waypoint, gameObject.transform).position = new Vector3(GameManager.mapX - 1, 0, i);
            if (j % 2 == 1)
                Instantiate(Waypoint, gameObject.transform).position = new Vector3(0, 0, i);
        }
        points = new Transform[transform.childCount];

        //go through all the children of this object and add them to the array in order
        for (int i = 0; i < points.Length; i++)
            points[i] = transform.GetChild(i);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    Vector3 level1, level2, level3, level4;
    [SerializeField]
    float timeToTarget = .15f;

    Vector3 nextLevelPosition;
    int xValue;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

/*        cameraTimer -= Time.deltaTime;
        if (cameraTimer >= 0.1f)
        {

            smoothCamera(transform.position, nextLevelTransition.currentLevelLocation);

        }*/

    }


    public static void nextLevel(int currentLevel)
    {

        xValue = 25 * currentLevel;
        nextLevelPosition = new Vector3(xValue, 0, -10);

        smoothCamera(transform.position, nextLevelPosition);




    }

    public void smoothCamera(Vector3 currentPos, Vector3 newPos)
    {

        transform.position = Vector3.SmoothDamp(currentPos, newPos,ref velocity, timeToTarget);

        if (currentPos != newPos)
        {
            timeToTarget -= .01f;
            smoothCamera(transform.position, newPos);
        }
        else
            timeToTarget = .15f;
    }
}
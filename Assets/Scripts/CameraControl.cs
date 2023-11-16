using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

        cameraTimer -= Time.deltaTime;
        if (cameraTimer >= 0.1f)
        {

            smoothCamera(transform.position, nextLevelTransition.currentLevelLocation);

        }

    }






    public void smoothCamera(Vector3 currentPos, Vector3 newPos)
    {

        transform.position = Vector3.SmoothDamp(currentPos, newPos, ref velocity, 0.15f);





    }
}
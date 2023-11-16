using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int currentLevel = 1;

    public CameraControl cameraControl;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
            cameraControl.nextLevel(currentLevel);
        currentLevel++;

    }


}

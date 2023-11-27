using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    int currentLevel = 1;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    CameraControl cameraControl;
    //[SerializeField] GameObject camera;


    void Awake()
    {
        //cameraControl = camera.GetComponent<CameraControl>();


    }

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
        if (other.gameObject.tag == "Player") {
            cameraControl.NextLevel(currentLevel);
            gameManager.MovePlayerToLevel(currentLevel);
        }
        currentLevel++;

    }


}

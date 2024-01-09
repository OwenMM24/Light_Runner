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
    [SerializeField] AudioSource doorAudio;


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
            doorAudio.Play();
            cameraControl.NextLevel(currentLevel);
            gameManager.MovePlayerToLevel(currentLevel);
        }
        Debug.Log("before add" + currentLevel);
        currentLevel+= 2;
        Debug.Log("after add" + currentLevel);
    }


}

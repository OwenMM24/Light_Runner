using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDoor : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    float finalTime = 0f;
    [SerializeField] GameObject finalScreen;
    [SerializeField] AudioSource doorAudio;
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
        if (other.gameObject.tag == "Player")
        {
            doorAudio.Play();
            finalTime = gameManager.levelTimer;
            Time.timeScale = 0f;
            finalScreen.SetActive(true);


        }
    }
}

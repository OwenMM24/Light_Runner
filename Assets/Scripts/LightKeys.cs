using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightKeys : MonoBehaviour
{

    [SerializeField]
    GameObject ConnectedLight1, ConnectedLight2, ConnectedLight3;


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

        if (other.gameObject.CompareTag("BlueLight"))
        {
            //change other object material
            DoorCheck();
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BlueLight"))
        {
            //change other object material back to original color
        }

    }

    void DoorCheck()
    {
        //check 3 lights, if all have material then open door
    }

}

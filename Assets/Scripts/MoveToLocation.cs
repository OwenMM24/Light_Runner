using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLocation : MonoBehaviour
{

    [SerializeField]
    Vector3[] locations;
    [SerializeField]
    int numOfLocations;
    [SerializeField]
    float speed;
    int x = 0;

    bool increasing = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!PauseMenu.paused)
        {

            transform.position = Vector3.MoveTowards(transform.position, locations[x], speed * Time.deltaTime);

            if (x == numOfLocations - 1 || x == 0)
                increasing = !increasing;

            if (transform.position == locations[x] && increasing == true && x != numOfLocations - 1)
            {
                x++;
            }

            else if (transform.position == locations[x] && increasing == false && x != 0)
            {
                x--;
            }
        }


    }

}

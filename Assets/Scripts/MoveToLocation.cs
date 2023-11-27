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

    bool swap = true;


    void FixedUpdate()
    {
        if (!PauseMenu.paused)
        {

            transform.position = Vector3.MoveTowards(transform.position, locations[x], speed * Time.deltaTime);

            if (transform.position == locations[x] && x != numOfLocations - 1)
            {
                x++;
            }

            else if (transform.position == locations[x] && x != 0)
            {
                x = 0;
            }

        }

    }

}

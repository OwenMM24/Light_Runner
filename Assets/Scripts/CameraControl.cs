using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    Vector3 level1, level2, level3, level4;
    [SerializeField]
    float timeToTarget = 1f;

    Vector3 nextLevelPosition;
    int xValue;
    Vector3 velocity = Vector3.zero;

    float tickCoolDown = .1f;
    float transitionAmount = 0f;
    bool levelTransitioning = false;


    Vector3 startPos;
    Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (levelTransitioning == true)
        {
            Debug.Log("hit");
            //Debug.Log(transitionAmount);
            transform.position = new Vector3(Mathf.Lerp(startPos.x, xValue, transitionAmount), 0, -10);
            transitionAmount += Time.deltaTime;

            if (transform.position.x >= xValue)
            {
                //Debug.Log("hit");
                levelTransitioning = false;
                transitionAmount = 0f;
            }
        }






/*        cameraTimer -= Time.deltaTime;
        if (cameraTimer >= 0.1f)
        {

            smoothCamera(transform.position, nextLevelTransition.currentLevelLocation);

        }*/

    }


    public void NextLevel(int currentLevel)
    {

        xValue = 25 * currentLevel;
        nextLevelPosition = new Vector3(xValue, 0, -10);
        startPos = transform.position;
        levelTransitioning = true;
        //SmoothCamera(startPos, nextLevelPosition);




    }

/*    public void SmoothCamera(Vector3 currentPos, Vector3 newPos)
    {
*//*
        transform.position = new Vector3(Mathf.Lerp(startPos, newPos, amount), 0, -10);

        if (currentPos != newPos)
        {
            timeToTarget -= .01f;
            SmoothCamera(transform.position, newPos);
            Debug.Log("hit");
        }
        else
            timeToTarget = 1f;*//*
    }*/
}
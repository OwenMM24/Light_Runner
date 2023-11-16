using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float levelTimer = 0f;

    [SerializeField]
    TextMeshProUGUI timer;

    [SerializeField]
    int currentLevel = 0;

   // bool transitioningLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer += Time.deltaTime;
        timer.text = levelTimer.ToString("0.00");

/*        if (transitioningLevel)
        {

        }*/


    }

/*    public static void levelTransition()
    {
        currentLevel++;
        transitioningLevel = true;  
    }
*/

}

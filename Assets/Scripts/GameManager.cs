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

    [SerializeField]
    Vector3 level_2, level_3, level_4;

    [SerializeField]
    GameObject player;

    Vector3 levelRespawnPoint = new Vector3(-10, 3, -1);

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



        if (player.transform.position.y <= -8 || player.transform.position.y >= 8)
            PlayerHit();


    }

    public void PlayerHit()
    {
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        player.transform.position = levelRespawnPoint;


    }

    public void MovePlayerToLevel(int level)
    {
        if (level == 1)
        {
            player.transform.position = level_2;
            levelRespawnPoint = level_2;
        }
        else if (level == 2)
        {
            player.transform.position = level_3;
            levelRespawnPoint = level_3;
        }
        else
        {
            player.transform.position = level_4;
            levelRespawnPoint = level_4;
        }

    }


}

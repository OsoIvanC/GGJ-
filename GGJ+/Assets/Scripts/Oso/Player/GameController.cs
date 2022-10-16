using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Obstacle> obstacles;

    public int staticCount;

 

    public static GameController instance;

    private void Awake()
    {
        instance = this;


        RandomizeObstacles();
    }

    public void RandomizeObstacles()
    {
       
        for (int i = 0; i < staticCount; i++)
        {
            int random = Random.Range(i, obstacles.Count - 1);

            int r = Random.Range(0, 2);

            obstacles[random].isMovable = (r==0) ? true : false;

            obstacles[random].UpdateObs();
        }
    }


}

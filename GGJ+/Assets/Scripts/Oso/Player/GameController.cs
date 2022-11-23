using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Obstacle> obstacles;
    public List<Obstacle> staticObs; 
    public List<Obstacle> movableObs;
    public int staticCount;

    int count;

    public static GameController instance;

    private void Awake()
    {
        instance = this;

        InitObs();
        //RandomizeObstacles();
    }

    void InitObs()
    {
        movableObs.Clear();
        staticObs.Clear();

        foreach (Obstacle obs in obstacles)
        {
            obs.isMovable = true;

            movableObs.Add(obs);
        }

        foreach (Obstacle obs in obstacles)
        {
            obs.UpdateObs();
        }
    }
    public void RandomizeObstacles()
    {
        InitObs();

        for (int i = 0; i < staticCount; i++)
        {
            int r = Random.Range(0, movableObs.Count);

            staticObs.Add(movableObs[r]);

            movableObs.Remove(movableObs[r]);
        }
        
        foreach (Obstacle obs in movableObs)
        {
            obs.isMovable = true;
        }

        foreach (Obstacle obs in staticObs)
        {
            obs.isMovable = false;
        }

        foreach (Obstacle obs in obstacles)
        {
            obs.UpdateObs();
        }
    }


}

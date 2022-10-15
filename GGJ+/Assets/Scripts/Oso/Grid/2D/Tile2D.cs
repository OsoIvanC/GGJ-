using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct Neighbors2D
{
    public Tile2D North;
    public Tile2D South;
    public Tile2D East;
    public Tile2D West;

}

public class Tile2D : MonoBehaviour
{
    public Neighbors2D _Neighbors;
}

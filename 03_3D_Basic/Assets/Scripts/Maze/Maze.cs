using UnityEngine;

public class Maze : MonoBehaviour
{
    public int length = 10;

    CellBase[,] grid;

    public Maze(int length)
    {
        this.length = length;
        grid = new CellBase[length, length];

        for(int i = 0; i < length; i++)
        {
            for(int j = 0; j < length; j++)
            {
                grid[i, j] = new CellBase(i, j);
            }
        }
    }

    public void Init()
    {

    }
}

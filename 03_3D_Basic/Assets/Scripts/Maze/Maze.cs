

public class Maze
{
    public int length = 10;

    CellBase[,] cells;

    public Maze(int length)
    {
        this.length = length;
        cells = new CellBase[length, length];

        for(int i = 0; i < length; i++)
        {
            for(int j = 0; j < length; j++)
            {
                cells[i, j] = new CellBase(i, j);
            }
        }
    }
}

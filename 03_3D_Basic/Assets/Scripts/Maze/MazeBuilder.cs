using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(MazeVisualize))]
public class MazeBuilder : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public int seed = -1;

    MazeVisualize visualize;

    MazeBase maze;

    private void Awake()
    {
        visualize = GetComponent<MazeVisualize>();
    }

    public void Build()
    {
        maze = new WilsonMaze(width, height, seed);
        visualize.Clear();
        visualize.Draw(maze);
    }
}

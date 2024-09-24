using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Test11_SlimePath : TestBase
{
    public Tilemap background;
    public Tilemap obstacle;
    public Slime slime;

    TileGridMap map;

    private void Start()
    {
        map = new TileGridMap(background, obstacle);
        slime.Initialized(map, slime.transform.position);
    }

    protected override void LClick_performed(InputAction.CallbackContext context)
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(screen);

        slime.ShowPath(true);
        slime.SetDestination(world);
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Vector2Int destination = map.GetRandomMovablePosition();

        slime.SetDestination(destination);
    }
}

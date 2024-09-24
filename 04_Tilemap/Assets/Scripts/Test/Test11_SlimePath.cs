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
    public Slime blockSlime;

    TileGridMap map;

    private void Start()
    {
        map = new TileGridMap(background, obstacle);
        slime.Initialized(map, slime.transform.position);
        blockSlime.Initialized(map, blockSlime.transform.position);
        slime.ShowPath(true);
        blockSlime.ShowPath(false);
    }

    protected override void LClick_performed(InputAction.CallbackContext context)
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(screen);

        
        slime.SetDestination(world);
    }

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        Vector2Int destination = map.GetRandomMovablePosition();

        slime.SetDestination(destination);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        blockSlime.moveSpeed = 2.0f;
    }
}

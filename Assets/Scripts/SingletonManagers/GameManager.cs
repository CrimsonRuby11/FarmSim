using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool[,] isOccupied;
    private Dictionary<(int, int), (TileObject t, GameObject g)> gridobjects;
    private PicksController controller;

    public TileObject[] tileHotbar;

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(this);
        }

        gridobjects = new Dictionary<(int, int), (TileObject t, GameObject g)>();
    }

    void Start() {
        controller = PicksController.instance;

        tileHotbar = new TileObject[4];

        setHotbarTile(0, controller.floor);
        setHotbarTile(1, controller.tree);
        setHotbarTile(2, controller.wall);
        setHotbarTile(3, controller.bush);
    }

    public void setGrid(int x, int y, TileObject t, GameObject g) {
        gridobjects.Add((x, y), (t, g));
    }

    public void resetGrid(int x, int y) {
        if(gridobjects.ContainsKey((x, y))) {
            Destroy(gridobjects[(x, y)].g);
            gridobjects.Remove((x, y));
        }
    }

    public (TileObject t, GameObject g) getGrid(int x, int y) {
        if(gridobjects.ContainsKey((x, y))) {
            return gridobjects[(x, y)];
        } else {
            return (null, null);
        }
    }

    public GameObject getObjectInGrid(int x, int y) {
        return gridobjects[(x, y)].g;
    }

    public int getNextState() {
        for(int i = 0; i < 4; i++) {
            if(tileHotbar[i] == controller.nullTile) {
                return i+1;
            }
        }

        return 0;
    }

    public void setHotbarTile(int n, TileObject tile) {
        tileHotbar[n] = tile;
        controller.hotbarObjects[n].GetComponent<HotbarElement>().setImage(n);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool[,] isOccupied;

    private Dictionary<(int, int), GameObject> gridobjects;

    public TileObject[] tileHotbar;

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(this);
        }

        gridobjects = new Dictionary<(int, int), GameObject>();
    }

    public void setGrid(int x, int y, GameObject g) {
        gridobjects.Add((x, y), g);
    }

    public void resetGrid(int x, int y) {
        if(gridobjects.ContainsKey((x, y))) {
            Destroy(gridobjects[(x, y)]);
            gridobjects.Remove((x, y));
        }
    }

    public bool getGrid(int x, int y) {
        if(gridobjects.ContainsKey((x, y))) {
            return true;
        } else {
            return false;
        }
    }

    public GameObject getObjectInGrid(int x, int y) {
        return gridobjects[(x, y)];
    }
}

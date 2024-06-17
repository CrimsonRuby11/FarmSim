using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static bool[,] isOccupied {
        get;
        set;
    }

    static GameManager() {
        isOccupied = new bool[30,30];
        for(int i = 0; i < 30; i++) {
            for(int j = 0; j < 30; j++) {
                isOccupied[i,j] = false;
            }
        }
    }
}

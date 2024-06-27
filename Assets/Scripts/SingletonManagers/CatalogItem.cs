using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatalogItem : MonoBehaviour
{
    [SerializeField]
    private GameObject border;
    [SerializeField]
    private TextMeshProUGUI borderText;
    [SerializeField]
    private Image tileImage;

    private CatalogManager manager;
    private GameManager gameManager;
    private PicksController picksController;

    private TileObject tile;

    public int state = 0; // can only be 0 (not selected) / 1 / 2 / 3 / 4

    void Start() {
        manager = CatalogManager.instance;
        gameManager = GameManager.instance;
        picksController = PicksController.instance;
    }

    public void setState(int toState) {
        if(toState == 0) {
            // deselect
            // turn off border
            border.SetActive(false);
            // set hotbar at position state to nullTile
            Debug.Log(GameManager.instance);
            GameManager.instance.setHotbarTile(state-1, PicksController.instance.nullTile);
            // set state = 0
            state = 0;
            Debug.Log(state);
        } else {
            // state must be zero
            // select
            // turn on border and set border text
            border.SetActive(true);
            borderText.text = "" + toState;
            // set hotbar at position n-1 to this tile
            Debug.Log(GameManager.instance);
            GameManager.instance.setHotbarTile(toState - 1, tile);
            // set state = n
            state = toState;
            Debug.Log(state);
        }
    }

    public void setTile(TileObject tile) {
        this.tile = tile;
        tileImage.sprite = tile.UIImage;
    }

    public void click() {
        if(state == 0) {
            if(manager.nextState == 0) {
                Debug.Log("Hotbar full");
            } else {
                setState((int)manager.nextState);
                manager.nextState = gameManager.getNextState();
            }
        } else {
            setState(0);
            manager.nextState = gameManager.getNextState();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogManager : MonoBehaviour
{
    public static CatalogManager instance;

    private GameManager gameManager;

    [SerializeField]
    private CatalogItem itemPf;

    [SerializeField]
    private RectTransform parent;

    [SerializeField]
    private List<TileObject> tiles;

    private List<CatalogItem> itemsList;

    public float nextState; // 0 / 1 / 2 / 3 / 4

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        gameManager = GameManager.instance;

        itemsList = new List<CatalogItem>();

        initializeUI();

        nextState = gameManager.getNextState();
    }

    public void initializeUI() {
        for(int i = 0; i < tiles.Count; i++) {
            CatalogItem item = Instantiate(itemPf, Vector3.zero, Quaternion.identity, parent);
            item.setTile(tiles[i]);
            if(i < 4) {
                item.setState(i+1);
            }
            itemsList.Add(item);
        }
    }

}

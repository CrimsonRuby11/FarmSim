using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarElement : MonoBehaviour
{
    [SerializeField]
    private Image imageComponent;

    private GameManager gameManager;

    void Start() {
        gameManager = GameManager.instance;
    }

    public void setImage(int t) {
        imageComponent.sprite = gameManager.tileHotbar[t].UIImage;
    }
}

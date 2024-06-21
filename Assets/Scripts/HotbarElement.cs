using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarElement : MonoBehaviour
{
    [SerializeField]
    private Image imageComponent;

    public void setImage(int t) {
        imageComponent.sprite = GameManager.instance.tileHotbar[t].UIImage;
    }
}

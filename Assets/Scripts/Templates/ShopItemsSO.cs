using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable objects/Item", order = 1)]
public class ShopItemsSO : ScriptableObject
{
    public string itemTitle;
    public int buyPrice;
    public int sellPrice;
    public Sprite icon;
    // when player has no money deselcts the item
    public bool isVisible = true;
    public int level;
    public bool isLocked = true;
}

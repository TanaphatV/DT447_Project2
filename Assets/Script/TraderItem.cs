using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderItem : MonoBehaviour
{
    public string itemName;
    public int cost;
    public virtual void ItemEffect(PlayerInteract playerInteract) { }

}

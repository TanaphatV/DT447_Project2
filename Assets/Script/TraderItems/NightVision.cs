using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightVision : TraderItem
{
    void Start()
    {
        cost = 3;
        itemName = "Night Vision";
    }
    public override void ItemEffect(PlayerInteract playerInteract)
    {
        playerInteract.NightVision.SetActive(true);
    }
}

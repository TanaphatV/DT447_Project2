using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : TraderItem
{
    void Start()
    {
        cost = 2;
        itemName = "Flash Light";
    }
    public override void ItemEffect(PlayerInteract playerInteract)
    {
        playerInteract.FlashLight.SetActive(true);
    }
}

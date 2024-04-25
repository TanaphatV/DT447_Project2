using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : TraderItem
{
    void Start()
    {
        cost = 3;
        itemName = "Speed";
    }
    public override void ItemEffect(PlayerInteract playerInteract)
    {
        playerInteract.fpsController.speed += 3;
    }


}

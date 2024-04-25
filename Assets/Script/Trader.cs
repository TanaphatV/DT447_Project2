using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class Trader : MonoBehaviour
{
    List<TraderItem> traderItems;
    public List<TraderItem> itemCatalogue; 
    public GameObject itemContainer;
    void Start()
    {
        traderItems = new List<TraderItem>(itemContainer.GetComponents<TraderItem>());
        for (int i = 0; i < 3; i++)
        {

            var tempItem = traderItems[i/*Random.Range(0, traderItems.Count)*/];
            //if (traderItems.Find((x) => { return x.itemName == tempItem.itemName; }))
            //{
            //    i--;
            //    continue;
            //}
            itemCatalogue.Add(tempItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

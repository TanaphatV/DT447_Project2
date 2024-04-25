using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct PickUpItem
{
    public string name;
    public Color color;
}

public class Pickable : MonoBehaviour
{
    public PickUpItem item;
    public Light aura;
    // Start is called before the first frame update
    private void Start()
    {
       //item.color = aura.color;
    }

    public void PickItem()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}

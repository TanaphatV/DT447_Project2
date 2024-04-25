using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInteract : MonoBehaviour
{
    public List<PickUpItem> items;
    public TextMeshProUGUI text;
    [SerializeField] public FPSController fpsController;//asdad
    int totalCollectible;
    int currentCollectibleCount = 0;

    bool talkingToTrader = false;
    bool inTraderRange = false;
    public GameObject NightVision;
    public GameObject FlashLight;

    // Start is called before the first frame update
    void Start()
    {
        totalCollectible = FindObjectsOfType(typeof(Collectible)).Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 3.0f);
        foreach (var col in cols)
        {
            if (!col.gameObject.CompareTag("Interactable"))
                continue;
            Debug.Log("iteractable");
            CheckPickUp(col);
        }
        bool checkGate = false;
        bool checkTrader = false;
        foreach (var col in cols)
        {

            if (CheckGate(col))
            {
                checkGate = true;
                break;
            }
            else if(CheckTrader(col))
            {
                checkTrader = true;
                break;
            }
        }
        trading = checkTrader;
        inGate = checkGate;
    }
    Coroutine traderTalk;
    void CheckPickUp(Collider col)
    {
        Debug.Log("check col");
        if (col.TryGetComponent(out Pickable item))
        {
            Debug.Log("item script got");
            if (item is Collectible)
                currentCollectibleCount++;
            else
                items.Add(item.item);
            Debug.Log("add to list");
            StartCoroutine(ItemPopUpTextIE(item.item.name));
            item.PickItem();
        }

    }

    Coroutine GatePopUp = null;
    bool CheckGate(Collider col)
    {
        if (col.TryGetComponent(out Gate gate))
        {
            inGate = true;
            if (GatePopUp == null)
                GatePopUp = StartCoroutine(GatePopUpIE(gate));
            return true;
        }
        return false;
    }
    bool inGate = false;

    bool trading = false;
    bool CheckTrader(Collider col)
    {
        if(col.gameObject.tag == "Interactable" && col.gameObject.transform.parent != null)
        {
            if (col.gameObject.transform.parent.TryGetComponent(out Trader trader))
            {
                trading = true;
                if (traderTalk == null)
                    traderTalk = StartCoroutine(TraderTextIE(trader));
                return true;
            }
        }
        return false;
    }

    IEnumerator GatePopUpIE(Gate gate)
    {
        text.enabled = true;
        text.color = Color.red;
        text.color = new Color(text.color.r, text.color.g, text.color.g, 0);
        text.text = "Give me a " + gate.item + " or you may not pass...\n(Press F to give the thing a " + gate.item + ")";
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.g, text.color.a + (Time.deltaTime / 1.0f));
            yield return null;
        }
        while(inGate)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                for (int i = 0; i < items.Count;i++)
                {
                    if(items[i].name == gate.item)
                    {
                        items.RemoveAt(i);
                        gate.OpenGate();
                    }
                }
            }
            yield return null;
        }
        GatePopUp = null;
        text.text = "";
    }

    IEnumerator TraderTextIE(Trader trader)
    {
        Debug.Log("EnterTradeEnum");
        text.enabled = true;
        text.color = Color.white;
        text.text = "You meet a mysterious trader...\nWant to see his wares?\nPress F to trade";
        bool viewingCatalogue = false;
        string catalogue = "";
        bool quit = false;
        while(trading)
        {
            Debug.Log("trading");
            if(Input.GetKeyDown(KeyCode.F) && !viewingCatalogue)
            {
                catalogue = "Press the item number to buy (Press ESC to exit)\nItem Catalogue:\n";
                Debug.Log("ViewCat");
                for(int i = 0; i < trader.itemCatalogue.Count; i++)
                {
                    var item = trader.itemCatalogue[i];
                    catalogue += (i+1) + ". " + item.itemName + " Cost: " + item.cost + "\n";
                    viewingCatalogue = true;
                    yield return null;
                }
            }
            while(viewingCatalogue && trading)
            {
                Debug.Log("VIEWING");
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    break;
                }

                text.text = catalogue;
                for(int i = 0; i < trader.itemCatalogue.Count; i++)
                {
                    if(Input.GetKeyDown(KeyCode.Alpha1 + i))
                    {
                        var item = trader.itemCatalogue[i];
                        if (currentCollectibleCount < item.cost)
                            continue;
                        item.ItemEffect(this);
                        Debug.Log("Buy item");
                        currentCollectibleCount -= item.cost;
                        trader.itemCatalogue.RemoveAt(i);
                        catalogue = "Press the item number to buy (Press ESC to exit)\nItem Catalogue:\n";
                        for (int j = 0; j < trader.itemCatalogue.Count; j++)
                        {
                            var item2 = trader.itemCatalogue[j];
                            catalogue += (j + 1) + ". " + item2.itemName + " Cost: " + item2.cost + "\n";
                            yield return null;
                        }
                        break;

                    }
                    yield return null;
                }
                yield return null;

            }
            yield return null;
        }
        for (float i = 0; i < 2; i += Time.deltaTime)
        {
            text.text = currentCollectibleCount + " Mysterious Cube Left";
            text.color = new Color(text.color.r, text.color.g, text.color.g, text.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        traderTalk = null;
        yield return new WaitUntil(() => { return trading == false; });
    }

    IEnumerator ItemPopUpTextIE(string item)
    {
        text.enabled = true;
        text.color = Color.white;

        if (item == "MysteriousCube")
        {
            text.text = "Picked up a " + item + " " + currentCollectibleCount + "/" + totalCollectible;
            text.color = Color.yellow;
        }
        else
            text.text = "Picked up a " + item + "!";
        
        for (float i = 0; i < 2; i += Time.deltaTime)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.g, text.color.a - (Time.deltaTime/2.0f));
            yield return null;
        }
        text.text = "";
        text.color = new Color(text.color.r, text.color.g, text.color.g, 1);

    }
}

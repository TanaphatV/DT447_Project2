using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public string item;
    public MeshRenderer wall;
    public SpriteRenderer eye;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGate()
    {
        StartCoroutine(OpenGateIE());
    }

    IEnumerator OpenGateIE()
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            wall.material.color = new Color(wall.material.color.r, wall.material.color.g, wall.material.color.g, wall.material.color.a - (Time.deltaTime));
            yield return null;
        }
        yield return new WaitForSeconds(0.6f);
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            eye.color = new Color(eye.color.r, eye.color.g, eye.color.g, eye.color.a - (Time.deltaTime));
            yield return null;
        }
        Destroy(gameObject.transform.parent.gameObject);
    }
}

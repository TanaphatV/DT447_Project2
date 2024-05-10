using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class EntryPageController : MonoBehaviour
{
    [SerializeField] TMP_InputField ipField;
    [SerializeField] TMP_InputField nameField;

    public void Proceed()
    {
        PlayerPrefs.SetString("ip", ipField.text);
        PlayerPrefs.SetString("name", nameField.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

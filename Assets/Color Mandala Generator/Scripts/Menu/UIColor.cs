using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColor : MonoBehaviour {

    public InputField Red;
    public InputField Blue;
    public InputField Green;

    private Image img;

    // Use this for initialization
    void Start () {
        img = GetComponent<Image>();
    }
    
    // Update is called once per frame
    void Update () {
        img.color = new Color32(byte.Parse(Red.text),byte.Parse(Blue.text),byte.Parse(Green.text),255);
    }
}

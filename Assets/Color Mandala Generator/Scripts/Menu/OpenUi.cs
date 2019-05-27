using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenUi : MonoBehaviour {
    public GameObject UI;

    public void OpenSettings() {
        SceneManager.LoadScene("Settings");
    }
    
    public void Baack() {
         GameObject.FindGameObjectWithTag("Plane").GetComponent <Collider>().enabled = true;
        UI.SetActive(false);
        
    }
    
    void OnMouseDown() {
        UI.SetActive(true);
         GameObject.FindGameObjectWithTag("Plane").GetComponent <Collider>().enabled = false;
    }



    
}

using UnityEngine;
using System.Collections;

public class ScreenResizer : MonoBehaviour {
  

    private readonly float DEFAULT_WIDTH = 1;
    private readonly float DEFAULT_HEIGHT = 1;

   
 

    void Start() {
/*
        float texWidth = PlayerPrefs.GetInt("Width");
        float texHight = PlayerPrefs.GetInt("Height");

         Debug.Log(texHight+" "+texWidth);

        float _worldScreenHeight = Camera.main.orthographicSize * 2f;
        float  _worldScreenWidth = _worldScreenHeight / Screen.height * Screen.width;

     
        float scaleX = _worldScreenWidth / 10;
        float scaleY = _worldScreenHeight / 10;

       transform.localScale = new Vector3(scaleX, 1, scaleY);*/
        
        
       //float height = Camera.main.orthographicSize * 2.0f;
       //float width = height * Screen.width / Screen.height;
       // float lasTscale = height > width ?width:height;   
     //transform.localScale = new Vector3(lasTscale/10, lasTscale,lasTscale/10);
        
        

    }

}
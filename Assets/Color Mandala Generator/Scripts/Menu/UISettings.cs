using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettings : MonoBehaviour {

    public InputField txtSeed;
    public InputField txtAngleCount;
    public InputField txtWidth;
    public InputField txtHeight;
    public InputField txtColorSlowness;
    public InputField txtReflectionCount;
    public InputField[] txtColor1;
    public InputField[] txtColor2;
    public InputField[] txtColor3;
    public InputField[] txtColor4;
    public InputField[] txtColor5;
    

    public InputField txtBrightness;
    public InputField txtContrast;
    public InputField[] txtStepTime;
    public InputField[] txtStepWidth;
    public Toggle tglTextureFilter;
    public Toggle tglMirror;



    void Start () {
        txtSeed.text = PlayerPrefs.GetInt("Seed").ToString();
        txtAngleCount.text = PlayerPrefs.GetInt("AngleCount").ToString();
        txtReflectionCount.text= PlayerPrefs.GetInt("ReflectionCount").ToString();
        txtWidth.text = PlayerPrefs.GetInt("Width").ToString();
        //txtHeight.text = PlayerPrefs.GetInt("Height").ToString();
        txtColorSlowness.text = PlayerPrefs.GetInt("ColorSlowness").ToString();
        txtColor1[0].text = PlayerPrefs.GetInt("R1").ToString();
        txtColor1[1].text = PlayerPrefs.GetInt("G1").ToString();
        txtColor1[2].text = PlayerPrefs.GetInt("B1").ToString();

        txtColor2[0].text = PlayerPrefs.GetInt("R2").ToString();
        txtColor2[1].text = PlayerPrefs.GetInt("G2").ToString();
        txtColor2[2].text = PlayerPrefs.GetInt("B2").ToString();

        txtColor3[0].text = PlayerPrefs.GetInt("R3").ToString();
        txtColor3[1].text = PlayerPrefs.GetInt("G3").ToString();
        txtColor3[2].text = PlayerPrefs.GetInt("B3").ToString();

        txtColor4[0].text = PlayerPrefs.GetInt("R4").ToString();
        txtColor4[1].text = PlayerPrefs.GetInt("G4").ToString();
        txtColor4[2].text = PlayerPrefs.GetInt("B4").ToString();

        txtColor5[0].text = PlayerPrefs.GetInt("R5").ToString();
        txtColor5[1].text = PlayerPrefs.GetInt("G5").ToString();
        txtColor5[2].text = PlayerPrefs.GetInt("B5").ToString();


        txtBrightness.text = PlayerPrefs.GetFloat("Brightness").ToString();
        txtContrast.text = PlayerPrefs.GetFloat("Conrast").ToString();


       txtStepTime[0].text = PlayerPrefs.GetFloat("stMin").ToString();
       txtStepTime[1].text = PlayerPrefs.GetFloat("stMax").ToString();


       txtStepWidth[0].text = PlayerPrefs.GetFloat("swMin").ToString();
       txtStepWidth[1].text = PlayerPrefs.GetFloat("swMax").ToString();

        tglTextureFilter.isOn = PlayerPrefs.GetInt("isTFEnable") == 1;

        tglMirror.isOn = PlayerPrefs.GetInt("isMirror") == 1;

    }


    public void PressButtonStart() {
        PlayerPrefs.SetInt("Seed", int.Parse(txtSeed.text));
        PlayerPrefs.SetInt("AngleCount", int.Parse(txtAngleCount.text));

        PlayerPrefs.GetInt("ReflectionCount", int.Parse(txtReflectionCount.text));


        PlayerPrefs.SetInt("Width", int.Parse(txtWidth.text));
        //PlayerPrefs.SetInt("Height", int.Parse(txtHeight.text));
        PlayerPrefs.SetInt("ColorSlowness", int.Parse(txtColorSlowness.text));



        PlayerPrefs.SetInt("R1", int.Parse(txtColor1[0].text));
        PlayerPrefs.SetInt("G1", int.Parse(txtColor1[1].text));
        PlayerPrefs.SetInt("B1", int.Parse(txtColor1[2].text));

        PlayerPrefs.SetInt("R2", int.Parse(txtColor2[0].text));
        PlayerPrefs.SetInt("G2", int.Parse(txtColor2[1].text));
        PlayerPrefs.SetInt("B2", int.Parse(txtColor2[2].text));

        PlayerPrefs.SetInt("R3", int.Parse(txtColor3[0].text));
        PlayerPrefs.SetInt("G3", int.Parse(txtColor3[1].text));
        PlayerPrefs.SetInt("B3", int.Parse(txtColor3[2].text));

        PlayerPrefs.SetInt("R4", int.Parse(txtColor4[0].text));
        PlayerPrefs.SetInt("G4", int.Parse(txtColor4[1].text));
        PlayerPrefs.SetInt("B4", int.Parse(txtColor4[2].text));

        PlayerPrefs.SetInt("R5", int.Parse(txtColor5[0].text));
        PlayerPrefs.SetInt("G5", int.Parse(txtColor5[1].text));
        PlayerPrefs.SetInt("B5", int.Parse(txtColor5[2].text));
        
        PlayerPrefs.SetFloat("Brightness", float.Parse(txtBrightness.text));
        PlayerPrefs.SetFloat("Conrast", float.Parse(txtContrast.text));


        PlayerPrefs.SetFloat("stMin", float.Parse(txtStepTime[0].text));
        PlayerPrefs.SetFloat("stMax", float.Parse(txtStepTime[1].text));

        PlayerPrefs.SetFloat("swMin", float.Parse(txtStepWidth[0].text));
        PlayerPrefs.SetFloat("swMax", float.Parse(txtStepWidth[1].text));

        PlayerPrefs.SetInt("isTFEnable", tglTextureFilter.isOn ? 1 : 0);
        PlayerPrefs.SetInt("isMirror", tglMirror.isOn ? 1 : 0);

        SceneManager.LoadScene("MainDemo");

    }




    
    

}

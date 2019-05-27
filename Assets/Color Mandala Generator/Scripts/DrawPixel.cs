using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DrawPixel : MonoBehaviour {

    public Material mat;
    public Text txtDebug;

    //Fixed seed if we want generate same values with random
    public int fixedSeed = 33;

    public List <Color32> MyColors = new List <Color32>();

    //"Slothness" of color change
    public float phasorInc = 1.0f / 40000.0f;

    //On how many parts of circle point will be reflected
    public int reflections = 8;

    //How manu points set per frame
    public int pointsPerFrame = 5;

    //How many angles avalible for every poin to make one step
    public int anglesCount = 3;

    public int texWidth = 1080;
    public int texHight;

    public FilterMode filterMode;
    
    //generated Texture
    private Texture2D tex;


    //Then larger step, then greater dispersion
    private float stepWide = 1.0f;

    public float stepWidthMin;
    public float stepWidthMax;

    public float stepTimeMax;
    public float stepTimeMin;

    //Additional Reflection by Y
    public bool isMirror;


    private Phasor p;
    private Palette palette;
    private float[] angles;
    private static System.Random MyRandom;
    private Vector2 v;

    // Workflow
    // Awake->SetPrefs->InstallPrefs->InitPrefs->SetStepCoo->Start->ClearTexture->Setup
    // FixedUpdate -> MakeStep


    public static float RngRange(System.Random rng, float min, float max) {
        return min + ((float) rng.NextDouble() * (max - min));
    }
    


    void Awake() {
        Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;

        SetPrefs();
        MyRandom = new System.Random(fixedSeed);
        StartCoroutine(SetStepCoo());
    }



    void SetPrefs() {
        //  PlayerPrefs.DeleteAll();

        if (!PlayerPrefs.HasKey("First")) {
            //if app launced firs time
            InstallPrefs();
        }
        InitPrefs();

    }



    void InstallPrefs() {

        /*
        new Color32(11, 17, 13, 255);
        new Color32(44, 77, 86, 255);
        new Color32(195, 170, 114, 255);
        new Color32(220, 118, 18, 255);
        new Color32(189, 50, 0, 255);
        */

        PlayerPrefs.SetInt("First", 1);

        PlayerPrefs.SetInt("Seed", 33);
        PlayerPrefs.SetInt("AngleCount", 5);
        PlayerPrefs.SetInt("ReflectionCount", 8);

        PlayerPrefs.SetInt("Width", 1080);


        // PlayerPrefs.SetInt("Height", 700);
        PlayerPrefs.SetInt("ColorSlowness", 40000);


        PlayerPrefs.SetInt("R1", 11);
        PlayerPrefs.SetInt("G1", 17);
        PlayerPrefs.SetInt("B1", 13);

        PlayerPrefs.SetInt("R2", 44);
        PlayerPrefs.SetInt("G2", 77);
        PlayerPrefs.SetInt("B2", 86);

        PlayerPrefs.SetInt("R3", 195);
        PlayerPrefs.SetInt("G3", 170);
        PlayerPrefs.SetInt("B3", 114);

        PlayerPrefs.SetInt("R4", 220);
        PlayerPrefs.SetInt("G4", 118);
        PlayerPrefs.SetInt("B4", 18);

        PlayerPrefs.SetInt("R5", 189);
        PlayerPrefs.SetInt("G5", 50);
        PlayerPrefs.SetInt("B5", 0);

        PlayerPrefs.SetFloat("Brightness", 2.0f);
        PlayerPrefs.SetFloat("Conrast", 1.0f);


        PlayerPrefs.SetFloat("stMin", 10.0f);
        PlayerPrefs.SetFloat("stMax", 33.0f);

        PlayerPrefs.SetFloat("swMin", 0.1f);
        PlayerPrefs.SetFloat("swMax", 1.0f);

        PlayerPrefs.SetInt("isTFEnable", 1);
        PlayerPrefs.SetInt("isMirror", 1);

    }



    void InitPrefs() {
        fixedSeed = PlayerPrefs.GetInt("Seed");
        anglesCount = PlayerPrefs.GetInt("AngleCount");
        reflections = PlayerPrefs.GetInt("ReflectionCount");
        texWidth = PlayerPrefs.GetInt("Width");
        // texture squared to looks same on different resolutions..

        texHight = texWidth;

        // ...however you can change this logic here
        // texHight = PlayerPrefs.GetInt("Height");

        phasorInc = 1.0f / PlayerPrefs.GetInt("ColorSlowness");

        //Adding colors
        MyColors.Clear();
        for (int i = 1; i < 6; ++i) {
            MyColors.Add(new Color32((byte) PlayerPrefs.GetInt("R" + i),
                (byte) PlayerPrefs.GetInt("G" + i),
                (byte) PlayerPrefs.GetInt("B" + i), 255));
        }



        stepTimeMax = PlayerPrefs.GetFloat("stMax");
        stepTimeMin = PlayerPrefs.GetFloat("stMin");

        stepWidthMin = PlayerPrefs.GetFloat("swMin");
        stepWidthMax = PlayerPrefs.GetFloat("swMax");

        filterMode = PlayerPrefs.GetInt("isTFEnable") == 1
            ? FilterMode.Bilinear
            : FilterMode.Point;

        isMirror = PlayerPrefs.GetInt("isMirror") == 1;

        //Shader settings
        mat.SetFloat("_Brightness", PlayerPrefs.GetFloat("Brightness"));
        mat.SetFloat("_Contrast", PlayerPrefs.GetFloat("Conrast"));



    }



    private IEnumerator SetStepCoo() {
        //We need additional random with same seed to synhronize random values
        System.Random RCo = new System.Random(fixedSeed);

        while (true) {
            //After 1.75 step looks really different
            if (stepWidthMax > 1.75) {
                if (RCo.NextDouble() > 0.5f) {
                    stepWide = RngRange(RCo, stepWidthMin, 1.75f);
                } else {
                    stepWide = RngRange(RCo, 1.75f, stepWidthMax);
                }
            } else {
                stepWide = RngRange(RCo, stepWidthMin, stepWidthMax);

            }


            if (GroupWatcher.ProfileCount > 0)
            {
                stepTimeMax = GroupWatcher.ProfileCount;
            }

           
            float timeToNext = RngRange(RCo, stepTimeMin, stepTimeMax);

            //Some Debug.Log to compare random info
            string str = "Step = " + stepWide + " Time next = " + timeToNext;
            txtDebug.text = txtDebug.text + Environment.NewLine + str;


            yield return new WaitForSeconds(timeToNext);

        }


    }



    void Start() {
        tex = new Texture2D(texWidth, texHight);
        tex.filterMode = filterMode;
        mat.mainTexture = tex;

        ClearTexture();
        Setup();
    }



    void ClearTexture() {

        for (int i = 0; i < texHight; i++) {
            for (int j = 0; j < texHight; j++) {
                tex.SetPixel(i, j, Color.black);
            }
        }
    }



    void Setup() {
        angles = new float[anglesCount];
        for (int i = 0; i < angles.Length; i++) {
            angles[i] = ((float) i / (float) anglesCount) * (2 * Mathf.PI);
        }

        palette = new Palette();

        //Phasor for color smooth changing
        p = new Phasor(phasorInc);

        //Center of texture
        v = new Vector2(texWidth / 2.0f, texHight / 2.0f);



        for (int i = 0; i < MyColors.Count; ++i) {
            palette.Add(MyColors[i]);
        }

    }
    


    //Fixed update with 0.1 TimeStep (change in Prpject Preferences)
    void FixedUpdate() {
        for (int i = 0; i < pointsPerFrame; i++) {
            p.PhaseStep();
            MakeStep();
        }

    }



    public void MakeStep() {

        int index = MyRandom.Next(0, angles.Length);

        v = Mth.getVCoordinates(v, stepWide, angles[index]);

        float offset = texWidth / 4.0f;

        if (v.x >= texWidth + offset) {
            v.x -= texWidth + offset;
        }

        if (v.x < -offset) {
            v.x += texWidth + offset;
        }

        if (v.y >= texHight + offset) {
            v.y -= texHight + offset;
        }

        if (v.y < 0) {
            v.y += texHight + offset;
        }

        float a = Mth.getAngleFromCenter(v, new Vector2(texWidth, texHight));

        float d = Vector2.Distance(new Vector2(v.x, v.y), new Vector2(texWidth / 2, texHight / 2));
        Vector2 center = new Vector2(texWidth / 2, texHight / 2);


        Color32 col = palette.GetNorm(p.phase);

        for (int i = 0; i < reflections; i++) {
            float thisAngle = a + ((2 * Mathf.PI) / (float) reflections) * i;
            Vector2 thisV = Mth.getVCoordinates(center, d, thisAngle);
            tex.SetPixel((int) thisV.x, (int) thisV.y, col);
            if (isMirror) {
                thisV = Mth.getVCoordinates(center, d, Mathf.PI - thisAngle);
                tex.SetPixel((int) thisV.x, (int) thisV.y, col);
            }
        }
        tex.Apply();
    }


}

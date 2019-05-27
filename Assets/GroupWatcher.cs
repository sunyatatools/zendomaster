using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using SimpleFirebaseUnity;
using System.Collections;

public class GroupWatcher : MonoBehaviour
{
    //private const String profileUrl = "https://zendo-v1.firebaseio.com/players.json?print=pretty";

    private const String profileUrl = "https://firebasestorage.googleapis.com/v0/b/zendo-v1.appspot.com/o/{0}.jpg?alt=media";
    internal static int ProfileCount;
    internal static int SampleCount;
    private UnityWebRequest request = null;
    private FirebaseObserver observer = null;
    private Dictionary<string, string> currentProfiles = new Dictionary<string, string>();
    private List<Texture> currentTextures = new List<Texture>();
    private string currentKey;
    private Dictionary<string, Dictionary<string, string>> currentSamples = new Dictionary<string, Dictionary<string, string>>();

    public RawImage CenterImage;
    private int lastIdx = 0;

    float timer = 0.0f;
    int lastMin = 0;

    void ChangeHandler(Firebase sender, DataSnapshot snapshot)
    {
        foreach (string key in snapshot.Keys)
        {
            var profile = String.Format(profileUrl, key);

            currentProfiles[key] = profile;

            currentKey = key;

            print(profile);

            object values = snapshot.RawJson;

            print(values);

        }

        GroupWatcher.ProfileCount = currentProfiles.Count;
        GroupWatcher.SampleCount = currentSamples.Count;

        StartCoroutine(this.GetTextures());

    }

    // Start is called before the first frame update
    void Start()
    {
        var firebase = Firebase.CreateNew("zendo-v1.firebaseio.com");

        observer = new FirebaseObserver(firebase.Child("players"), 5f);
        observer.OnChange += ChangeHandler;
        observer.Start(); // Starts the observer coroutine


        print("Start");

    }


    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        int mins = (int)timer / 60;

        if(mins > lastMin)
        {
            lastMin = mins;

            if (currentTextures.Count > 0)
            {
                var value = UnityEngine.Random.Range(0, currentTextures.Count - 1);

                print("compassion index" + value);

                CenterImage.texture = currentTextures[value];

                lastIdx = value;
            }
        }

    }

    IEnumerator GetTextures()
    {
        if(currentTextures.Count == currentProfiles.Count)
        { yield break; }
        foreach (string url in currentProfiles.Values)
        {  
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    var tex = DownloadHandlerTexture.GetContent(uwr);

                    //var newTex = CalculateTexture(tex.height, tex.width, tex.width / 2, tex.width / 2, tex.height / 2, tex);

                    currentTextures.Add(tex);
                }
            }
        }
    }

    IEnumerator GetTexture()
    {
        if (currentProfiles.ContainsKey(currentKey))
        {
            var url = currentProfiles[currentKey];

            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.Log(uwr.error);
                }
                else
                {

                    var tex = DownloadHandlerTexture.GetContent(uwr);

                    //var newTex = CalculateTexture(tex.height, tex.width, tex.width / 2, tex.width / 2, tex.height / 2, tex);

                    currentTextures.Add(tex);
                }
            }
        }
    }

    Texture2D CalculateTexture(int h, int w, float r, float cx, float cy, Texture2D sourceTex)
    {
        Color[] c = sourceTex.GetPixels(0, 0, sourceTex.width, sourceTex.height);
        Texture2D b = new Texture2D(h, w);
        for (int i = 0; i < (h * w); i++)
        {
            int y = Mathf.FloorToInt(((float)i) / ((float)w));
            int x = Mathf.FloorToInt(((float)i - ((float)(y * w))));
            if (r * r >= (x - cx) * (x - cx) + (y - cy) * (y - cy))
            {
                b.SetPixel(x, y, c[i]);
            }
            else
            {
                b.SetPixel(x, y, Color.clear);
            }
        }
        b.Apply();
        return b;
    }
}



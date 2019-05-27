using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Palette {
    public List <Color32> colors;

    public Palette() {
        colors = new List <Color32>();
    }

    public void Add(Color32 c) {
        colors.Add(c);
    }

    public Color32 GetNorm(float p) {
        int index = (int) (p * colors.Count);
        Color32 c1 = colors[index];
        Color32 c2 = colors[(index + 1) % colors.Count];

        return Color32.Lerp(c1, c2, p * colors.Count - index);
    }
}
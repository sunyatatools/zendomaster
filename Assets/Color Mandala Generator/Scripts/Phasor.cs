using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phasor {
    float inc;
    public float phase;

    public Phasor(float inc) {
        if (inc < 1.0f && inc > 0.0f) {
            this.inc = inc;
        } else {
            this.inc = 1.0f / 40000.0f;
        }
        
    }

    public void PhaseStep() {
        phase += inc;
        
        if (phase >= 1.0) {
            phase = 0.0f;
        }
        
    }
}

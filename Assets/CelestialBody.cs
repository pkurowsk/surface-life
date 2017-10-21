using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour {
    public float gravity = 9.8f;

    public float radius = 0.0f;

    public const float TERMINAL_VELOCITY = 200f;

    void OnTriggerEnter2D(Collider2D col)
    {
        RotationWalker rw = col.GetComponent<RotationWalker>();
        if (rw)
        {
            rw.SetCelestialBody(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public bool shockwaveActive = false;
    public Material shockwaveMaterial;

    public void SetShockwaveActive()
    {
        shockwaveActive = true;
    }
}

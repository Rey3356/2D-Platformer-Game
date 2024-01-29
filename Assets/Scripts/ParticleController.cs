using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem particleSystemA;
    public ParticleSystem particleSystemB;

    public void PlayPlayerWinEffect()
    { 
        particleSystemA.Play();
    }
}

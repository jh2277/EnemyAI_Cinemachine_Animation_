using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private AudioSource targetSound;

    // Start is called before the first frame update
    void Start()
    {
        targetSound = GetComponent<AudioSource>();
    }

    public void PlayWeaponSound()
    {
        targetSound.Play();
    }
}

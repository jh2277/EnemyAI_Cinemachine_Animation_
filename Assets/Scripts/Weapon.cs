using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private AudioSource fireSound;

    // Start is called before the first frame update
    void Start()
    {
        fireSound = GetComponent<AudioSource>();  
    }

    public void PlayWeaponSound()
    {
        fireSound.Play();
    }
}

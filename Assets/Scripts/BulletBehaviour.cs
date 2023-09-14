using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletSpeed;
    // Update is called once per frame
    private Rigidbody bulletRigid;
    private void Awake()
    {
        bulletRigid = GetComponent<Rigidbody>();
        bulletRigid.AddForce(transform.forward, ForceMode.Impulse); //Imp
        Destroy(this.gameObject, 3f);
    }
}

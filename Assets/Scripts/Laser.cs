using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField] public float damage = 100f;

    public float Damage
    {
        get
        {
            return damage;
        }
    }

    public void Hit(){
        Destroy(gameObject);
    }

}

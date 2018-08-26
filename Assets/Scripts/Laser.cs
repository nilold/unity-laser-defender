using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] public float damage = 100f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();

        if (laser)
        {
            Hit();
            laser.Hit();
        }
    }

    public float Damage
    {
        get
        {
            return damage;
        }
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

}

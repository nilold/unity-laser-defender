using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 200f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();

        if (laser)
            Hit(collision, laser);

    }

    private void Hit(Collider2D collision, Laser laser)
    {
        float damage = laser.Damage;
        Debug.Log("Hit! Damage: " + damage.ToString());
        health -= damage;

        if (health <= 0)
            Die();

        laser.Hit();

    }

    private void Die()
    {
        Debug.Log("Enemy destroyed");
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

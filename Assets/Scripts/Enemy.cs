using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 200f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float laserSpeed;
    [SerializeField] float shotPeriodRandomTweak = 5f;
    float shotPeriod = 1f;

    void Start()
    {
        if (enemyLaserPrefab)
            StartCoroutine(ShotCoroutine());
    }


    void Update()
    {

    }

    public void SetShotPeriod(float shotPeriod)
    {
        this.shotPeriod = shotPeriod;
    }

    private IEnumerator ShotCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotPeriod + Random.Range(0, shotPeriodRandomTweak));
            Shot();
        }
    }

    private void Shot()
    {
        GameObject laser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();

        //if (laser && laser.tag != "Enemy") not needed becouse iam using layers
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 200f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float laserSpeed;
    [SerializeField] float shotPeriodRandomTweak = 5f;
    [SerializeField] int hitScore = 50;
    [SerializeField] int destroyScore = 100;
    [SerializeField] AudioClip shotAudioClip;
    [SerializeField] AudioClip deathSound;

    ScoreKeeper playerScore; 

    float shotPeriod = 1f;

    void Start()
    {
        playerScore = GameObject.Find("Score").GetComponent<ScoreKeeper>();
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
        AudioSource.PlayClipAtPoint(shotAudioClip, transform.position);
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
        playerScore.AddScore(hitScore);
        health -= damage;

        laser.Hit();

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        playerScore.AddScore(destroyScore);
        Destroy(gameObject);
    }

}

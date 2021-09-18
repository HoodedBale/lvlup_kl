using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crawler : MonoBehaviour
{
    public Vector3 direction;
    public float elevation;
    public float health;
    public int score;
    public GameObject explosion;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = direction * speed;
        transform.position += new Vector3(0, elevation, 0);
        transform.DOScale(new Vector3(1, 1, 1), 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if(bullet)
        {
            health -= bullet.damage;
            bullet.Explode();

            if(health <= 0)
            {
                WaveGenerator waveGen = GameObject.FindObjectOfType<WaveGenerator>();
                --waveGen.enemyCount;
                GameUI gameUI = GameObject.FindObjectOfType<GameUI>();
                gameUI.finalScore += score;
                GameObject temp = Instantiate(explosion);
                temp.transform.position = transform.position;

                Destroy(gameObject);
            }
        }

        if(other.name == "DeadZone")
        {
            WaveGenerator waveGen = GameObject.FindObjectOfType<WaveGenerator>();
            --waveGen.enemyCount;

            PlayerController player = GameObject.FindObjectOfType<PlayerController>();
            player.Damage(1);
            GameObject temp = Instantiate(explosion);
            temp.transform.position = transform.position;

            Destroy(gameObject);
        }

        if (other.name == "Player")
        {
            WaveGenerator waveGen = GameObject.FindObjectOfType<WaveGenerator>();
            --waveGen.enemyCount;
            other.GetComponent<PlayerController>().Damage(1);
            GameObject temp = Instantiate(explosion);
            temp.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}

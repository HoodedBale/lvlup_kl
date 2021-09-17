using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cutter : MonoBehaviour
{
    public float elevation;
    public float health;
    public int score;

    public float speed;
    public float forwardTime = 1.0f;
    int m_dir;
    bool m_stepForward = false;
    GridMan m_gridMan;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(0, elevation, 0);
        m_dir = Random.Range(0, 2) % 2 == 0 ? 1 : -1;
        m_gridMan = GameObject.FindObjectOfType<GridMan>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (m_stepForward) return;

        transform.position += new Vector3(m_dir, 0, 0) * speed * Time.deltaTime;
        Vector3 pos = transform.position;

        if (pos.x < m_gridMan.transform.position.x - (m_gridMan.tileNumber.x - 1) / 2 * m_gridMan.gapSize)
        {
            pos.x = m_gridMan.transform.position.x - (m_gridMan.tileNumber.x - 1) / 2 * m_gridMan.gapSize;
            transform.position = pos;
            m_dir *= -1;

            m_stepForward = true;
            Vector3 newPos = pos - new Vector3(0, 0, m_gridMan.gapSize);
            transform.DOMove(newPos, forwardTime).onComplete += () => { m_stepForward = false; };
        }
        else if(pos.x > m_gridMan.transform.position.x + (m_gridMan.tileNumber.x - 1) / 2 * m_gridMan.gapSize)
        {
            pos.x = m_gridMan.transform.position.x + (m_gridMan.tileNumber.x - 1) / 2 * m_gridMan.gapSize;
            transform.position = pos;
            m_dir *= -1;

            m_stepForward = true;
            Vector3 newPos = pos - new Vector3(0, 0, m_gridMan.gapSize);
            transform.DOMove(newPos, forwardTime).onComplete += () => { m_stepForward = false; };
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet)
        {
            health -= bullet.damage;
            bullet.Explode();

            if (health <= 0)
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

        if (other.name == "DeadZone")
        {
            WaveGenerator waveGen = GameObject.FindObjectOfType<WaveGenerator>();
            --waveGen.enemyCount;
            PlayerController player = GameObject.FindObjectOfType<PlayerController>();
            player.Damage(1);

            GameObject temp = Instantiate(explosion);
            temp.transform.position = transform.position;
            Destroy(gameObject);
        }

        if(other.name == "Player")
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

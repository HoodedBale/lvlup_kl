using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPattern : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    public float lifeTime;
    public float damage;
    public float speed;
    public float turnDelay;

    List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LifeTimeRoutine");
        StartCoroutine("TurnBulletRoutine");
        SpawnBullet(new Vector3(1, 0, 0));
        SpawnBullet(new Vector3(-1, 0, 0));
    }
    IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    IEnumerator TurnBulletRoutine()
    {
        yield return new WaitForSeconds(turnDelay);
        foreach(GameObject temp in bullets)
        {
            if(temp)
                temp.GetComponent<Bullet>().SetDirection(new Vector3(0, 0, 1), speed);
        }
    }

    void SpawnBullet(Vector3 _dir)
    {
        GameObject temp = Instantiate(bullet);
        temp.transform.position = transform.position;
        temp.GetComponent<Bullet>().lifeTime = lifeTime;
        temp.GetComponent<Bullet>().damage = damage;
        temp.GetComponent<Bullet>().SetDirection(_dir, speed);
        bullets.Add(temp);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenPattern : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    public float lifeTime;
    public float damage;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LifeTimeRoutine");
        SpawnBullet(new Vector3(1, 0, 1));
        SpawnBullet(new Vector3(-1, 0, -1));
        SpawnBullet(new Vector3(-1, 0, 1));
        SpawnBullet(new Vector3(1, 0, -1));
        SpawnBullet(new Vector3(1, 0, 0));
        SpawnBullet(new Vector3(-1, 0, 0));
        SpawnBullet(new Vector3(0, 0, 1));
        SpawnBullet(new Vector3(0, 0, -1));
    }
    IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void SpawnBullet(Vector3 _dir)
    {
        GameObject temp = Instantiate(bullet);
        temp.transform.position = transform.position;
        temp.GetComponent<Bullet>().lifeTime = lifeTime;
        temp.GetComponent<Bullet>().damage = damage;
        temp.GetComponent<Bullet>().SetDirection(_dir, speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

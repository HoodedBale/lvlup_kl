using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPattern : MonoBehaviour
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
        GameObject temp = Instantiate(bullet);
        temp.transform.position = transform.position;
        temp.GetComponent<Bullet>().lifeTime = lifeTime;
        temp.GetComponent<Bullet>().damage = damage;
        temp.GetComponent<Bullet>().SetDirection(new Vector3(0, 0, 1), speed);
    }

    IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LifeTimeRoutine");
    }

    public void SetDirection(Vector3 direction, float speed)
    {
        GetComponent<Rigidbody>().velocity = direction.normalized * speed;
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

    public void Explode()
    {
        Destroy(gameObject);
    }
}

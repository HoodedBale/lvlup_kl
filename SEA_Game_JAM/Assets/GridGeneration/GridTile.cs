using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    MeshRenderer mesh;
    public Color spawnColor = new Color(1, 1, 1, 1);
    public Color defaultColor = new Color(0.8f, 0.8f, 0.8f, 1);
    public Color bulletColor = new Color(1.0f, 0.25f, 0.0f, 1);

    public float colorSpeed = 10.0f;
    bool m_spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        StartCoroutine(SpawnTile());
    }

    // Update is called once per frame
    void Update()
    {
        if(m_spawned)
            mesh.material.color += (defaultColor - mesh.material.color) * colorSpeed * Time.deltaTime;
    }

    IEnumerator SpawnTile()
    {
        float delay = Random.Range(1.0f, 2.0f);
        yield return new WaitForSeconds(delay);

        mesh.material.color = spawnColor;
        m_spawned = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet && m_spawned)
            mesh.material.color = bulletColor;
    }
}

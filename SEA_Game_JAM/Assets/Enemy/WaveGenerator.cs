using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    [SerializeField]
    GridMan gridMan;

    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public List<int> enemyWaves = new List<int>();
    public int baseWaves = 5;
    public int waveStep = 3;
    public float maxCooldown = 4.0f;
    public float minCooldown = 2.0f;
    public float cooldownStep = 0.05f;
    public int spawnStep = 5;
    public int maxSpawn = 5;
    public float waveBreak = 5;
    public float maxMoveSpeed = 2.0f;
    public float moveSpeedStep = 0.05f;

    public int wave = 0;
    public bool spawning = false;
    public int enemyCount = 0;

    public GameObject queenPiece;
    public float queenOffset = 2.0f;
    public float queenSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnQueenPiece();
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawning)
        {
            ++wave;
            spawning = true;
            StartCoroutine(WaveRoutine());
        }
    }

    IEnumerator WaveRoutine()
    {
        yield return new WaitForSeconds(waveBreak);
        float maxCD = maxCooldown - wave * cooldownStep;
        if (maxCD < minCooldown) maxCD = minCooldown;
        float cooldown = Random.Range(maxCD - 1, maxCD);

        int waves = baseWaves + (wave / waveStep);
        int maxSpn = 1 + wave / spawnStep;
        int spawn = Random.Range(1, maxSpn);

        for(int i = 0; i < waves; ++i)
        {
            int enemyType = Random.Range(0, enemyPrefabs.Count);
            while (wave < enemyWaves[enemyType])
            {
                enemyType = Random.Range(0, enemyPrefabs.Count);
            }

            float speed = Random.Range(1.0f, 1.0f + wave * moveSpeedStep);
            if(speed > maxMoveSpeed) speed = maxMoveSpeed;

            Dictionary<int, bool> takenID = new Dictionary<int, bool>();
            for(int j = 0; j < spawn; ++j)
            {
                int tileID = (int)Random.Range(gridMan.tileNumber.x * (gridMan.tileNumber.y - 1),
                    gridMan.tileNumber.x * gridMan.tileNumber.y - 1);

                while(takenID.ContainsKey(tileID))
                {
                    tileID = (int)Random.Range(gridMan.tileNumber.x * (gridMan.tileNumber.y - 1),
                    gridMan.tileNumber.x * gridMan.tileNumber.y - 1);
                }
                takenID.Add(tileID, true);

                GameObject temp = Instantiate(enemyPrefabs[enemyType]);
                temp.transform.position = gridMan.tiles[tileID].transform.position;
                ++enemyCount;

                if (temp.GetComponent<Cutter>()) temp.GetComponent<Cutter>().speed *= speed;
                if (temp.GetComponent<Crawler>()) temp.GetComponent<Crawler>().speed *= speed;
            }

            yield return new WaitForSeconds(cooldown);
            cooldown = Random.Range(maxCD - 1, maxCD);
            spawn = Random.Range(1, maxSpn);

        }

        yield return new WaitUntil(() => { return enemyCount <= 0; });

        spawning = false;
    }

    void SpawnQueenPiece()
    {
        //int y = Random.Range(0, 5);
        //int id = y * (int)gridMan.tileNumber.y;
        //Vector3 spawnPos = gridMan.tiles[id].transform.position;
        //spawnPos.x -= queenOffset;

        //GameObject temp = Instantiate(queenPiece);
        //temp.transform.position = spawnPos;
        //temp.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * queenSpeed;

        for(int i = 0; i < (int)gridMan.tileNumber.x; ++i)
        {
            if (i % 2 == 1) continue;
            Vector3 spawnPos = gridMan.tiles[i].transform.position;
            GameObject temp = Instantiate(queenPiece);
            //spawnPos.y = temp.GetComponent<QueenPiece>().startHeight;
            temp.transform.position = spawnPos;
        }
    }
}

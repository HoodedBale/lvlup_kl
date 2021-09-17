using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMan : MonoBehaviour
{
    [SerializeField]
    GameObject gridPrefab;

    public Vector2 tileNumber;
    public float gapSize;

    public List<GameObject> tiles = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        GenerateTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateTiles()
    {
        float width = gapSize * tileNumber.x;
        float height = gapSize * tileNumber.y;

        float yStart = -(tileNumber.y - 1) * gapSize / 2;
        for(int i = 0; i < tileNumber.y; ++i)
        {
            float xStart = -(tileNumber.x - 1) * gapSize / 2;
            for(int j = 0; j < tileNumber.x; ++j)
            {
                GameObject temp = Instantiate(gridPrefab);
                temp.transform.SetParent(transform);
                temp.transform.localPosition = new Vector3(xStart, 0, yStart);
                tiles.Add(temp);
                xStart += gapSize;
            }
            yStart += gapSize;
        }

    }
}

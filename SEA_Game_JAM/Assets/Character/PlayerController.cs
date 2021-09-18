using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public enum HeadMode
    {
        PAWN,
        ROOK,
        BISHOP,
        KNIGHT,
        SIZE
    }

    [SerializeField]
    GridMan gridMan;

    [SerializeField]
    GameObject head;

    public int startGrid;
    public float startHeight;
    public float rotateTime = 0.5f;
    public float moveTime = 0.25f;

    public List<GameObject> projectilePatterns = new List<GameObject>();
    public List<float> projectileCD = new List<float>();

    public GameUI gameUI;

    bool m_rotating = false;
    bool m_moving = false;
    int[] m_currentGrid = { 0, 0 };
    float m_cooldown = 0;

    public int health = 3;

    public GameObject queenPattern;
    public float queenTime = 5.0f;
    public float queenCD = 0.1f;
    bool m_queenMode = false;

    public Animator animBody;

    public GameObject explosion;

    public GameObject healthBar;
    float m_maxhealth;

    [SerializeField]
    HeadMode m_currentHead;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = gridMan.tiles[startGrid].transform.position + new Vector3(0, startHeight, 0);
        m_currentGrid[1] = startGrid / (int)gridMan.tileNumber.y;
        m_currentGrid[0] = startGrid - m_currentGrid[1] * (int)gridMan.tileNumber.y;

        m_maxhealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !m_rotating)
            RotateHead(1);
        if (Input.GetKeyDown(KeyCode.E) && !m_rotating)
            RotateHead(-1);

        Movement();
        Shoot();
    }

    void Movement()
    {
        if (m_moving) return;

        int[] direction = { 0, 0 };

        if (Input.GetKey(KeyCode.W))
            direction[1] = 1;
        if (Input.GetKey(KeyCode.S))
            direction[1] = -1;
        if (Input.GetKey(KeyCode.A))
            direction[0] = -1;
        if (Input.GetKey(KeyCode.D))
            direction[0] = 1;

        if (direction[0] == 0 && direction[1] == 0) return;

        m_moving = true;
        m_currentGrid[0] += direction[0];
        m_currentGrid[1] += direction[1];

        if (m_currentGrid[0] < 0) m_currentGrid[0] = 0;
        if (m_currentGrid[1] < 0) m_currentGrid[1] = 0;
        if (m_currentGrid[0] >= (int)gridMan.tileNumber.x) m_currentGrid[0] = (int)gridMan.tileNumber.x - 1;
        if (m_currentGrid[1] >= (int)gridMan.tileNumber.y) m_currentGrid[1] = (int)gridMan.tileNumber.y - 1;

        Vector3 dest = gridMan.tiles[m_currentGrid[1] * (int)gridMan.tileNumber.x + m_currentGrid[0]].transform.position;
        dest.y = startHeight;

        transform.DOMove(dest, moveTime).SetEase(Ease.Linear).onComplete += () => { m_moving = false; };
        animBody.SetTrigger("walk");
    }

    void Shoot()
    {
        if(m_cooldown > 0)
        {
            m_cooldown -= Time.deltaTime;
            return;
        }

        if (!Input.GetKey(KeyCode.Mouse0))
            return;

        if(m_queenMode)
        {
            GameObject q = Instantiate(queenPattern);
            q.transform.position = transform.position;
            m_cooldown = queenCD;
            return;
        }

        GameObject temp = Instantiate(projectilePatterns[(int)m_currentHead]);
        temp.transform.position = transform.position;
        m_cooldown = projectileCD[(int)m_currentHead];
    }

    void RotateHead(int _dir)
    {
        Vector3 newRot = head.transform.eulerAngles;
        newRot.y += _dir * 90;
        if (newRot.y >= 360) newRot.y -= 360;
        if (newRot.y <= 0) newRot.y += 360;

        m_rotating = true;
        head.transform.DOLocalRotateQuaternion(Quaternion.Euler(newRot), rotateTime).onComplete += ()=> { m_rotating = false; };
        gameUI.RotateMode(_dir);

        int newMode = (int)m_currentHead;
        newMode += _dir;
        if (newMode < 0) newMode = (int)HeadMode.SIZE - 1;
        if (newMode >= (int)HeadMode.SIZE) newMode = 0;
        m_currentHead = (HeadMode)newMode;

        GetComponent<AudioSource>().Play();
    }

    public void Damage(int _dmg)
    {
        health -= _dmg;

        healthBar.transform.DOScale(new Vector3(health / m_maxhealth, 1, 1), 0.1f);

        if (health <= 0)
        {
            gameUI.ShowLoseScreen();
            GameObject temp = Instantiate(explosion);
            temp.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.name == "QueenPiece(Clone)")
        {
            StartCoroutine(QueenRoutine());
            Destroy(other.gameObject);
        }
    }

    public void SetQueen()
    {
        StartCoroutine(QueenRoutine());
    }

    IEnumerator QueenRoutine()
    {
        m_queenMode = true;
        yield return new WaitForSeconds(queenTime);
        m_queenMode = false;
    }
}

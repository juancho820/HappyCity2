using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool SHOW_COLLIDER = true; //$$

    public static LevelManager Instance { set; get; }

    //Level Spawning
    private const float DISTANCE_BEFORE_SPAWN = 100.0f;
    private const int INITIAL_SEGMENTS = 10;
    private const int INITIAL_TRANSITION_SEGMENTS = 2;
    private const int MAX_SEGMENTS_ON_SCREEN = 15;
    private const int MAX_SEGMENTS_ON_SCREEN2 = 15;
    private const int MAX_SEGMENTS_ON_SCREEN3 = 15;
    private const int MAX_SEGMENTS_ON_SCREEN4 = 15;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int amountOfActiveSegments2;
    private int amountOfActiveSegments3;
    private int amountOfActiveSegments4;
    private int continiousSegments;
    private int currentSpawnZ;
    private int currentLevel;
    private int y1, y2, y3;

    public int NumeroDeSpawns = 5;
    public int Contador = 0;
    public int zona;


    //List of pieces
    public List<Piece> ramps = new List<Piece>();
    public List<Piece> longblocks = new List<Piece>();
    public List<Piece> jumps = new List<Piece>();
    public List<Piece> slides = new List<Piece>();
    [HideInInspector]
    public List<Piece> pieces = new List<Piece>(); //All the pieces in the pool

    //List of segments
    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableSegments2 = new List<Segment>();
    public List<Segment> availableSegments3 = new List<Segment>();
    public List<Segment> availableSegments4 = new List<Segment>();
    public List<Segment> availableTransitions = new List<Segment>();
    [HideInInspector]
    public List<Segment> segments = new List<Segment>();
    [HideInInspector]
    public List<Segment> segments2 = new List<Segment>();
    [HideInInspector]
    public List<Segment> segments3 = new List<Segment>();
    [HideInInspector]
    public List<Segment> segments4 = new List<Segment>();

    //Gameplay
    private bool isMoving = false;

    private void Awake()
    {
        Instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnZ = 0;
        currentLevel = 0;
        zona = 0;
    }
    private void Start()
    {
        Contador = 0;
        if(GameManager.Once == false)
        {
            for (int i = 0; i < INITIAL_SEGMENTS; i++)
            {
                if (i < INITIAL_TRANSITION_SEGMENTS)
                {
                    SpawnTransition();
                }
                else
                {
                    GenerateSegment();
                }
            }
        }
        
    }

    private void Update()
    {
        if(currentSpawnZ - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN)
        {
            GenerateSegment();
            Contador++;
            if (Contador >= NumeroDeSpawns)
            {
                zona++;
                Contador = 0;
                return;
            }
            if(zona > 3)
            {
                zona = 0;
            }
            
        }

        if(amountOfActiveSegments >= MAX_SEGMENTS_ON_SCREEN)
        {
            segments[amountOfActiveSegments - 1].DeSpawn();
            amountOfActiveSegments--;
        }
        if (amountOfActiveSegments2 >= MAX_SEGMENTS_ON_SCREEN2)
        {
            segments2[amountOfActiveSegments2 - 1].DeSpawn();
            amountOfActiveSegments2--;
        }
        if (amountOfActiveSegments3 >= MAX_SEGMENTS_ON_SCREEN3)
        {
            segments3[amountOfActiveSegments3 - 1].DeSpawn();
            amountOfActiveSegments3--;
        }
        if (amountOfActiveSegments4 >= MAX_SEGMENTS_ON_SCREEN4)
        {
            segments4[amountOfActiveSegments4 - 1].DeSpawn();
            amountOfActiveSegments4--;
        }
    }

    private void GenerateSegment()
    {
        switch (zona)
        {
            case 0:
                SpawnSegment();
                Debug.Log("zona1");
                break;
            case 1:
                SpawnSegment2();
                break;
            case 2:
                SpawnSegment3();
                break;
            case 3:
                SpawnSegment4();
                break;
        }
        

        if(Random.Range(0f,1f) < (continiousSegments * 0.25f))
        {
            // Spawn transition seg
            continiousSegments = 0;
            SpawnTransition();
        }
        else
        {
            continiousSegments++;
        }
    }

    private void SpawnSegment()
    {
        List<Segment> possibleSeg = availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleSeg.Count);

        Segment s = GetSegment(id, false);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.lenght;
        amountOfActiveSegments++;
        s.Spawn();

    }
    private void SpawnSegment2()
    {
        List<Segment> possibleSeg = availableSegments2.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleSeg.Count);

        Segment s2 = GetSegment2(id, false);

        y1 = s2.endY1;
        y2 = s2.endY2;
        y3 = s2.endY3;

        s2.transform.SetParent(transform);
        s2.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s2.lenght;
        amountOfActiveSegments2++;
        s2.Spawn();

    }
    private void SpawnSegment3()
    {
        List<Segment> possibleSeg = availableSegments3.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleSeg.Count);

        Segment s3 = GetSegment3(id, false);

        y1 = s3.endY1;
        y2 = s3.endY2;
        y3 = s3.endY3;

        s3.transform.SetParent(transform);
        s3.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s3.lenght;
        amountOfActiveSegments3++;
        s3.Spawn();

    }
    private void SpawnSegment4()
    {
        List<Segment> possibleSeg = availableSegments4.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleSeg.Count);

        Segment s4 = GetSegment4(id, false);

        y1 = s4.endY1;
        y2 = s4.endY2;
        y3 = s4.endY3;

        s4.transform.SetParent(transform);
        s4.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s4.lenght;
        amountOfActiveSegments3++;
        s4.Spawn();

    }
    private void SpawnTransition()
    {
        List<Segment> possibleTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleTransition.Count);

        Segment s = GetSegment(id, true);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.lenght;
        amountOfActiveSegments++;
        s.Spawn();
    }

    public Segment GetSegment(int id, bool transition)
    {
        Segment s = null;
        s = segments.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);

        if(s == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;
            s = go.GetComponent<Segment>();

            s.SegId = id;
            s.transition = transition;

            segments.Insert(0, s);
        }
        else
        {
            segments.Remove(s);
            segments.Insert(0, s);
        }

        return s;
    }
    public Segment GetSegment2(int id, bool transition)
    {
        Segment s2 = null;
        s2 = segments2.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);

        if (s2 == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments2[id].gameObject) as GameObject;
            s2 = go.GetComponent<Segment>();

            s2.SegId = id;
            s2.transition = transition;

            segments2.Insert(0, s2);
        }
        else
        {
            segments2.Remove(s2);
            segments2.Insert(0, s2);
        }

        return s2;
    }
    public Segment GetSegment3(int id, bool transition)
    {
        Segment s3 = null;
        s3 = segments3.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);

        if (s3 == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments3[id].gameObject) as GameObject;
            s3 = go.GetComponent<Segment>();

            s3.SegId = id;
            s3.transition = transition;

            segments3.Insert(0, s3);
        }
        else
        {
            segments3.Remove(s3);
            segments3.Insert(0, s3);
        }

        return s3;
    }
    public Segment GetSegment4(int id, bool transition)
    {
        Segment s4 = null;
        s4 = segments4.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);

        if (s4 == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments4[id].gameObject) as GameObject;
            s4 = go.GetComponent<Segment>();

            s4.SegId = id;
            s4.transition = transition;

            segments4.Insert(0, s4);
        }
        else
        {
            segments4.Remove(s4);
            segments4.Insert(0, s4);
        }

        return s4;
    }

    public Piece GetPiece(PieceType pt, int visualIndex)
    {
        Piece p = pieces.Find(x => x.type == pt && x.visualIndex == visualIndex && !x.gameObject.activeSelf);

        if(p == null)
        {
            GameObject go = null;
            if(pt == PieceType.ramp)
            {
                go = ramps[visualIndex].gameObject;
            }
            else if (pt == PieceType.longblock)
            {
                go = longblocks[visualIndex].gameObject;
            }
            else if (pt == PieceType.jump)
            {
                go = jumps[visualIndex].gameObject;
            }
            else if (pt == PieceType.slide)
            {
                go = slides[visualIndex].gameObject;
            }

            go = Instantiate(go);
            p = go.GetComponent<Piece>();
            pieces.Add(p);
        }

        return p;
    }

}

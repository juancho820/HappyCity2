using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerTutorial : MonoBehaviour
{
    public bool SHOW_COLLIDER = true; //$$

    public static LevelManagerTutorial Instance { set; get; }

    //Level Spawning
    private const float DISTANCE_BEFORE_SPAWN = 100.0f;
    private const int INITIAL_SEGMENTS = 4;
    private const int INITIAL_TRANSITION_SEGMENTS = 2;
    private const int MAX_SEGMENTS_ON_SCREEN = 9;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int continiousSegments;
    private int currentSpawnZ;
    //private int currentLevel;
    private int y1, y2, y3;
    public bool Iniciado = false;

    public GameObject final;

    public int NumeroDeSpawns = 5;
    public int Contador = 0;
    public int zona;
    private int segmento;


    //List of pieces
    public List<Piece> jumpsB = new List<Piece>();
    public List<Piece> jumpsPA = new List<Piece>();
    public List<Piece> jumpsPALiquido = new List<Piece>();
    public List<Piece> jumpsMRC = new List<Piece>();
    public List<Piece> jumpsMRD = new List<Piece>();
    public List<Piece> jumpsMR = new List<Piece>();
    public List<Piece> slidesB = new List<Piece>();
    public List<Piece> slidesPA = new List<Piece>();
    public List<Piece> slidesMR = new List<Piece>();
    public List<Piece> blocksPA = new List<Piece>();
    public List<Piece> blocksPABig = new List<Piece>();
    public List<Piece> blocksPALittle = new List<Piece>();
    public List<Piece> FloorZones1 = new List<Piece>();
    [HideInInspector]
    public List<Piece> pieces = new List<Piece>(); //All the pieces in the pool

    //List of segments
    public List<SegmentTutorial> availableSegments = new List<SegmentTutorial>();
    public List<SegmentTutorial> availableTransitions = new List<SegmentTutorial>();
    [HideInInspector]
    public List<SegmentTutorial> segments = new List<SegmentTutorial>();

    private void Awake()
    {
        Instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnZ = 0;
        zona = 0;
        segmento = 0;
    }
    private void Start()
    {
        Contador = 0;
        if(GameManagerTutorial.Once == false)
        {
            for (int i = 0; i < INITIAL_SEGMENTS; i++)
            {
                if (i < INITIAL_TRANSITION_SEGMENTS)
                {
                    SpawnTransition();
                }
                else
                {
                    Iniciado = true;
                    SpawnSegment();
                }
            }
        }
        
    }

    private void Update()
    {
        if(currentSpawnZ - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN)
        {
            Contador++;
            SpawnSegment();
            if (segmento >= 4)
            {
                final.transform.position = Vector3.forward * 120;
            }
        }

        if(amountOfActiveSegments >= MAX_SEGMENTS_ON_SCREEN)
        {
            segments[amountOfActiveSegments - 1].DeSpawn();
            amountOfActiveSegments--;
        }
        if(segmento >= 4)
        {
            segmento = 4;
        }
    }

    private void SpawnSegment()
    {
        switch (zona)
        {
            case 0:
                List<SegmentTutorial> possibleSeg = availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
                //int id = Random.Range(0, possibleSeg.Count);
                Debug.Log(segmento);

                SegmentTutorial s = GetSegment(segmento, false);

                y1 = s.endY1;
                y2 = s.endY2;
                y3 = s.endY3;

                s.transform.SetParent(transform);
                s.transform.localPosition = Vector3.forward * currentSpawnZ;

                currentSpawnZ += s.lenght;
                amountOfActiveSegments++;
                s.Spawn();
                segmento++;
                break;
        }
    }

    private void SpawnTransition()
    {
        List<SegmentTutorial> possibleTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleTransition.Count);

        SegmentTutorial s = GetSegment(id, true);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.lenght;
        amountOfActiveSegments++;
        s.Spawn();
    }

    public SegmentTutorial GetSegment(int id, bool transition)
    {
        SegmentTutorial s = null;
        s = segments.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);

        if(s == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;
            
            s = go.GetComponent<SegmentTutorial>();

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

    public Piece GetPiece(PieceType pt, int visualIndex)
    {
        Piece p = pieces.Find(x => x.type == pt && x.visualIndex == visualIndex && !x.gameObject.activeSelf);

        if (p == null)
        {
            GameObject go = null;
            if (pt == PieceType.jumpPA)
            {
                go = jumpsPA[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpPALiquido)
            {
                go = jumpsPALiquido[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpMR)
            {
                go = jumpsMR[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpMRD)
            {
                go = jumpsMRD[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpMRC)
            {
                go = jumpsMRC[visualIndex].gameObject;
            }
            else if (pt == PieceType.slideMR)
            {
                go = slidesMR[visualIndex].gameObject;
            }
            else if (pt == PieceType.slidePA)
            {
                go = slidesPA[visualIndex].gameObject;
            }
            else if (pt == PieceType.blockPA)
            {
                go = blocksPA[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpB)
            {
                go = jumpsB[visualIndex].gameObject;
            }
            else if (pt == PieceType.slideB)
            {
                go = slidesB[visualIndex].gameObject;
            }
            else if (pt == PieceType.floorZone1)
            {
                go = FloorZones1[visualIndex].gameObject;
            }
            else if (pt == PieceType.blockPABig)
            {
                go = blocksPABig[visualIndex].gameObject;
            }
            else if (pt == PieceType.blockPALittle)
            {
                go = blocksPALittle[visualIndex].gameObject;
            }

            go = Instantiate(go);
            p = go.GetComponent<Piece>();
            pieces.Add(p);
        }

        return p;
    }

}

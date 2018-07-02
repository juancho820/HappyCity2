using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool SHOW_COLLIDER = true; //$$

    public static LevelManager Instance { set; get; }

    //Level Spawning
    public GameObject entrada, salida;
    private const float DISTANCE_BEFORE_SPAWN = 100.0f;
    private const int INITIAL_SEGMENTS = 5;
    private const int INITIAL_TRANSITION_SEGMENTS = 2;
    private const int MAX_SEGMENTS_ON_SCREEN = 7;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int amountOfActiveSegments2;
    private int amountOfActiveSegments3;
    private int amountOfActiveSegments4;
    private int continiousSegments;
    private int currentSpawnZ;
    //private int currentLevel;
    private int y1, y2, y3;
    public bool Iniciado = false;

    public int NumeroDeSpawns = 5;
    public int Contador = 0;
    public int zona;


    //List of pieces
    public List<Piece> ramps = new List<Piece>();
    public List<Piece> longblocksS = new List<Piece>();
    public List<Piece> jumpsB = new List<Piece>();
    public List<Piece> longblocks = new List<Piece>();
    public List<Piece> longblockscabeza = new List<Piece>();
    public List<Piece> jumpsPA = new List<Piece>();
    public List<Piece> jumpsPALiquido = new List<Piece>();
    public List<Piece> jumpsCC = new List<Piece>();
    public List<Piece> jumpsCCLiquido = new List<Piece>();
    public List<Piece> jumpsT = new List<Piece>();
    public List<Piece> jumpsA = new List<Piece>();
    public List<Piece> jumpsALiquido = new List<Piece>();
    public List<Piece> jumpsABolos = new List<Piece>();
    public List<Piece> jumpsMRC = new List<Piece>();
    public List<Piece> jumpsMRD = new List<Piece>();
    public List<Piece> jumpsMR = new List<Piece>();
    public List<Piece> slidesB = new List<Piece>();
    public List<Piece> slidesA = new List<Piece>();
    public List<Piece> slidesPA = new List<Piece>();
    public List<Piece> slidesCC = new List<Piece>();
    public List<Piece> slidesT = new List<Piece>();
    public List<Piece> slidesTBlocker = new List<Piece>();
    public List<Piece> slidesMR = new List<Piece>();
    public List<Piece> blocksA = new List<Piece>();
    public List<Piece> blocksPA = new List<Piece>();
    public List<Piece> blocksCC = new List<Piece>();
    public List<Piece> blocksCCcarros = new List<Piece>();
    public List<Piece> blocksT = new List<Piece>();
    public List<Piece> FloorZones1 = new List<Piece>();
    public List<Piece> FloorZones2 = new List<Piece>();
    public List<Piece> FloorZones3 = new List<Piece>();
    public List<Piece> FloorZones4 = new List<Piece>();
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

    private void Awake()
    {
        Instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnZ = 0;
        //currentLevel = 0;
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
            if (Contador >= NumeroDeSpawns)
            {
                zona++;
                Contador = 0;
            }
            if(zona >= 4)
            {
                zona = 0;
            }
            
        }

        if(amountOfActiveSegments >= MAX_SEGMENTS_ON_SCREEN)
        {
            segments[amountOfActiveSegments - 1].DeSpawn();
            amountOfActiveSegments--;
        }
        if (amountOfActiveSegments2 >= MAX_SEGMENTS_ON_SCREEN)
        {
            segments2[amountOfActiveSegments2 - 1].DeSpawn();
            amountOfActiveSegments2--;
        }
        if (amountOfActiveSegments3 >= MAX_SEGMENTS_ON_SCREEN)
        {
            segments3[amountOfActiveSegments3 - 1].DeSpawn();
            amountOfActiveSegments3--;
        }
        if (amountOfActiveSegments4 >= MAX_SEGMENTS_ON_SCREEN)
        {
            segments4[amountOfActiveSegments4 - 1].DeSpawn();
            amountOfActiveSegments4--;
        }
    }

    private void SpawnSegment()
    {
        switch (zona)
        {
            case 0:
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
                break;
            case 1:
                
                List<Segment> possibleSeg2 = availableSegments2.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
                int id2 = Random.Range(0, possibleSeg2.Count);

                Segment s2 = GetSegment2(id2, false);

                y1 = s2.endY1;
                y2 = s2.endY2;
                y3 = s2.endY3;

                s2.transform.SetParent(transform);
                s2.transform.localPosition = Vector3.forward * currentSpawnZ;

                currentSpawnZ += s2.lenght;
                amountOfActiveSegments2++;
                s2.Spawn();
                break;
            case 2:
                List<Segment> possibleSeg3 = availableSegments3.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
                int id3 = Random.Range(0, possibleSeg3.Count);

                Segment s3 = GetSegment3(id3, false);

                y1 = s3.endY1;
                y2 = s3.endY2;
                y3 = s3.endY3;

                s3.transform.SetParent(transform);
                s3.transform.localPosition = Vector3.forward * currentSpawnZ;

                currentSpawnZ += s3.lenght;
                amountOfActiveSegments3++;
                s3.Spawn();
                break;
            case 3:

                if (Contador == 1)
                {
                    entrada.SetActive(true);
                    entrada.transform.position = new Vector3(-0.03f, 11.2f, 1 * currentSpawnZ);
                }
                if (Contador == 9)
                {
                    salida.SetActive(true);
                    salida.transform.position = new Vector3(-0.75f, 8.55f, 1 * currentSpawnZ + 24);
                }
                List<Segment> possibleSeg4 = availableSegments4.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
                int id4 = Random.Range(0, possibleSeg4.Count);

                Segment s4 = GetSegment4(id4, false);

                y1 = s4.endY1;
                y2 = s4.endY2;
                y3 = s4.endY3;

                s4.transform.SetParent(transform);
                s4.transform.localPosition = Vector3.forward * currentSpawnZ;

                currentSpawnZ += s4.lenght;
                amountOfActiveSegments4++;
                s4.Spawn();
                break;
        }
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

    //private void SpawnMoveCamara()
    //{
    //    if(MovedorDeCamaraCC == true)
    //    {
    //        BajadorDeCamara.SetActive(true);
    //        BajadorDeCamara.transform.localPosition = Vector3.forward * currentSpawnZ;
    //    }
    //    else
    //    {
    //        SubidorDeCamara.SetActive(true);
    //        SubidorDeCamara.transform.localPosition = Vector3.forward * currentSpawnZ;
    //    }

    //}

    public Segment GetSegment(int id, bool transition)
    {
        Segment s = null;
        s = segments.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);

        if(s == null)
        {
            //GameObject go;
            //if (Iniciado == false)
            //{
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;
            //}
            //else
            //{
            //    go = Instantiate(availableSegments[id].gameObject) as GameObject;
            //}
            
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
    public Segment GetSegment2(int id2, bool transition)
    {
        Segment s2 = null;
        s2 = segments2.Find(x => x.SegId == id2 && x.transition == transition && !x.gameObject.activeSelf);

        if (s2 == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id2].gameObject : availableSegments2[id2].gameObject) as GameObject;
            s2 = go.GetComponent<Segment>();

            s2.SegId = id2;
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
    public Segment GetSegment3(int id3, bool transition)
    {
        Segment s3 = null;
        s3 = segments3.Find(x => x.SegId == id3 && x.transition == transition && !x.gameObject.activeSelf);

        if (s3 == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id3].gameObject : availableSegments3[id3].gameObject) as GameObject;
            s3 = go.GetComponent<Segment>();

            s3.SegId = id3;
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
    public Segment GetSegment4(int id4, bool transition)
    {
        Segment s4 = null;
        s4 = segments4.Find(x => x.SegId == id4 && x.transition == transition && !x.gameObject.activeSelf);

        if (s4 == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id4].gameObject : availableSegments4[id4].gameObject) as GameObject;
            s4 = go.GetComponent<Segment>();

            s4.SegId = id4;
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

        if (p == null)
        {
            GameObject go = null;
            if (pt == PieceType.ramp)
            {
                go = ramps[visualIndex].gameObject;
            }
            else if (pt == PieceType.longblock)
            {
                go = longblocks[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpPA)
            {
                go = jumpsPA[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpPALiquido)
            {
                go = jumpsPALiquido[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpCC)
            {
                go = jumpsCC[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpCCLiquido)
            {
                go = jumpsCCLiquido[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpALiquido)
            {
                go = jumpsALiquido[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpABolos)
            {
                go = jumpsABolos[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpA)
            {
                go = jumpsA[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpT)
            {
                go = jumpsT[visualIndex].gameObject;
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
            else if (pt == PieceType.slideA)
            {
                go = slidesA[visualIndex].gameObject;
            }
            else if (pt == PieceType.slidePA)
            {
                go = slidesPA[visualIndex].gameObject;
            }
            else if (pt == PieceType.slideCC)
            {
                go = slidesCC[visualIndex].gameObject;
            }
            else if (pt == PieceType.slideT)
            {
                go = slidesT[visualIndex].gameObject;
            }
            else if (pt == PieceType.slideTBlocker)
            {
                go = slidesTBlocker[visualIndex].gameObject;
            }
            else if (pt == PieceType.blockPA)
            {
                go = blocksPA[visualIndex].gameObject;
            }
            else if (pt == PieceType.blockCC)
            {
                go = blocksCC[visualIndex].gameObject;
            }
            else if (pt == PieceType.blockA)
            {
                go = blocksA[visualIndex].gameObject;
            }
            else if (pt == PieceType.blockCCcarros)
            {
                go = blocksCCcarros[visualIndex].gameObject;
            }
            else if (pt == PieceType.blockT)
            {
                go = blocksT[visualIndex].gameObject;
            }
            else if (pt == PieceType.jumpB)
            {
                go = jumpsB[visualIndex].gameObject;
            }
            else if (pt == PieceType.slideB)
            {
                go = slidesB[visualIndex].gameObject;
            }
            else if (pt == PieceType.longblockS)
            {
                go = longblocksS[visualIndex].gameObject;
            }
            else if (pt == PieceType.longblockCabeza)
            {
                go = longblockscabeza[visualIndex].gameObject;
            }
            else if (pt == PieceType.floorZone1)
            {
                go = FloorZones1[visualIndex].gameObject;
            }
            else if (pt == PieceType.floorZone2)
            {
                go = FloorZones2[visualIndex].gameObject;
            }
            else if (pt == PieceType.floorZone3)
            {
                go = FloorZones3[visualIndex].gameObject;
            }
            else if (pt == PieceType.floorZone4)
            {
                go = FloorZones4[visualIndex].gameObject;
            }

            go = Instantiate(go);
            p = go.GetComponent<Piece>();
            pieces.Add(p);
        }

        return p;
    }

}

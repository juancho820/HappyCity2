using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public PieceType type;
    private Piece currentPiece;
    int i;

    public void Start()
    {
        i = transform.childCount;
    }

    public void Spawn()
    {
        int amtObj = 0;
        switch (type)
        {
            case PieceType.jumpPA:
                amtObj = LevelManager.Instance.jumpsPA.Count;
                break;
            case PieceType.jumpCC:
                amtObj = LevelManager.Instance.jumpsCC.Count;
                break;
            case PieceType.jumpT:
                amtObj = LevelManager.Instance.jumpsT.Count;
                break;
            case PieceType.jumpA:
                amtObj = LevelManager.Instance.jumpsA.Count;
                break;
            case PieceType.jumpMR:
                amtObj = LevelManager.Instance.jumpsMR.Count;
                break;
            case PieceType.jumpMRC:
                amtObj = LevelManager.Instance.jumpsMRC.Count;
                break;
            case PieceType.jumpMRD:
                amtObj = LevelManager.Instance.jumpsMRD.Count;
                break;
            case PieceType.slideMR:
                amtObj = LevelManager.Instance.slidesMR.Count;
                break;
            case PieceType.slidePA:
                amtObj = LevelManager.Instance.slidesPA.Count;
                break;
            case PieceType.slideCC:
                amtObj = LevelManager.Instance.slidesCC.Count;
                break;
            case PieceType.slideT:
                amtObj = LevelManager.Instance.slidesT.Count;
                break;
            case PieceType.longblock:
                amtObj = LevelManager.Instance.longblocks.Count;
                break;
            case PieceType.ramp:
                amtObj = LevelManager.Instance.ramps.Count;
                break;
            case PieceType.blockPA:
                amtObj = LevelManager.Instance.blocksPA.Count;
                break;
            case PieceType.blockCC:
                amtObj = LevelManager.Instance.blocksCC.Count;
                break;
            case PieceType.blockT:
                amtObj = LevelManager.Instance.blocksT.Count;
                break;
            case PieceType.jumpB:
                amtObj = LevelManager.Instance.jumpsB.Count;
                break;
            case PieceType.slideB:
                amtObj = LevelManager.Instance.slidesB.Count;
                break;
            case PieceType.longblockS:
                amtObj = LevelManager.Instance.longblocksS.Count;
                break;
            case PieceType.floorZone1:
                amtObj = LevelManager.Instance.FloorZones1.Count;
                break;
            case PieceType.floorZone2:
                amtObj = LevelManager.Instance.FloorZones2.Count;
                break;
            case PieceType.floorZone3:
                amtObj = LevelManager.Instance.FloorZones3.Count;
                break;
            case PieceType.floorZone4:
                amtObj = LevelManager.Instance.FloorZones4.Count;
                break;
        }

        currentPiece = LevelManager.Instance.GetPiece(type, Random.Range(0,amtObj));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void Despawn()
    {
        currentPiece.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (!transform.GetChild(i-1).gameObject.activeSelf)
        {
            StartCoroutine(Activar());
        }
    }

    private IEnumerator Activar()
    {
        yield return new WaitForSeconds(2);
        transform.GetChild(i-1).gameObject.SetActive(true);
    }
}

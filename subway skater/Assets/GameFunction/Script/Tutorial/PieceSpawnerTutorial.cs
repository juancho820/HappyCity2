using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawnerTutorial : MonoBehaviour
{
    public PieceType type;
    public bool activo = true;
    private Piece currentPiece;
    private Vector3 posicion;
    int i;

    public void Spawn()
    {
        int amtObj = 0;
        switch (type)
        {
            case PieceType.jumpPA:
                amtObj = LevelManagerTutorial.Instance.jumpsPA.Count;
                break;
            case PieceType.jumpPALiquido:
                amtObj = LevelManagerTutorial.Instance.jumpsPALiquido.Count;
                break;
            case PieceType.jumpMR:
                amtObj = LevelManagerTutorial.Instance.jumpsMR.Count;
                break;
            case PieceType.jumpMRC:
                amtObj = LevelManagerTutorial.Instance.jumpsMRC.Count;
                break;
            case PieceType.jumpMRD:
                amtObj = LevelManagerTutorial.Instance.jumpsMRD.Count;
                break;
            case PieceType.slideMR:
                amtObj = LevelManagerTutorial.Instance.slidesMR.Count;
                break;
            case PieceType.slidePA:
                amtObj = LevelManagerTutorial.Instance.slidesPA.Count;
                break;
            case PieceType.blockPA:
                amtObj = LevelManagerTutorial.Instance.blocksPA.Count;
                break;
            case PieceType.jumpB:
                amtObj = LevelManagerTutorial.Instance.jumpsB.Count;
                break;
            case PieceType.slideB:
                amtObj = LevelManagerTutorial.Instance.slidesB.Count;
                break;
            case PieceType.floorZone1:
                amtObj = LevelManagerTutorial.Instance.FloorZones1.Count;
                break;
            case PieceType.blockPABig:
                amtObj = LevelManagerTutorial.Instance.blocksPABig.Count;
                break;
            case PieceType.blockPALittle:
                amtObj = LevelManagerTutorial.Instance.blocksPALittle.Count;
                break;
        }

        currentPiece = LevelManagerTutorial.Instance.GetPiece(type, Random.Range(0,amtObj));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void Despawn()
    {
        currentPiece.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (activo == false)
        {
            StartCoroutine(Activar());
            activo = true;
        }
    }

    private IEnumerator Activar()
    {
        i = transform.childCount;
        posicion = transform.GetChild(i - 1).gameObject.transform.position;
        transform.GetChild(i - 1).gameObject.transform.position = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1.4f);
        transform.GetChild(i - 1).gameObject.transform.position = posicion;
        //transform.GetChild(i-1).gameObject.SetActive(true);
    }
}

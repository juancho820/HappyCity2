using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public PieceType type;
    private Piece currentPiece;

    public void Spawn()
    {
        int amtObj = 0;
        switch (type)
        {
            case PieceType.jump:
                amtObj = LevelManager.Instance.jumps.Count;
                break;
            case PieceType.slide:
                amtObj = LevelManager.Instance.slides.Count;
                break;
            case PieceType.longblock:
                amtObj = LevelManager.Instance.longblocks.Count;
                break;
            case PieceType.ramp:
                amtObj = LevelManager.Instance.ramps.Count;
                break;
            case PieceType.block:
                amtObj = LevelManager.Instance.blocks.Count;
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
            case PieceType.zone1:
                amtObj = LevelManager.Instance.zones1.Count;
                break;
            case PieceType.zone2:
                amtObj = LevelManager.Instance.zones2.Count;
                break;
            case PieceType.zone3:
                amtObj = LevelManager.Instance.zones3.Count;
                break;
            case PieceType.zone4:
                amtObj = LevelManager.Instance.zones4.Count;
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
}

using UnityEngine;

public enum PieceType
{
    none = -1,
    ramp = 0,
    longblock = 1,
    jumpPA = 2,
    slidePA = 3,
    blockPA = 4,
    jumpB = 5,
    slideB = 6,
    longblockS = 7,
    floorZone1 = 8,
    floorZone2 = 9,
    floorZone3 = 10,
    floorZone4 = 11,
    blockCC = 12,
    jumpCC = 13,
    jumpA = 14,
    jumpT = 15,
    blockT = 16,
    slideCC = 17,
    slideT = 18,
    slideMR = 19,
    jumpMRC = 20,
    jumpMRD = 21,
    jumpMR = 22,
    blockCCcarros = 23,
    longblockCabeza = 24,
    jumpPALiquido = 25,
    jumpCCLiquido = 26,
    blockA = 27,
    slideA = 28,
    jumpALiquido = 29,
    jumpABolos = 30,
    slideTBlocker = 31,
    blockPALittle = 32,
    blockPABig = 33,
}

public class Piece : MonoBehaviour
{
    public PieceType type;
    public int visualIndex;
}

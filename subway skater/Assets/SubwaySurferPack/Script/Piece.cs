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
}

public class Piece : MonoBehaviour
{
    public PieceType type;
    public int visualIndex;

}

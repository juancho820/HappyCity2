using UnityEngine;

public enum PieceType
{
    none = -1,
    ramp = 0,
    longblock = 1,
    jump = 2,
    slide = 3,
    block = 4,
    jumpB = 5,
    slideB = 6,
    longblockS = 7,
    zone1 = 8,
    zone2 = 9,
    zone3 = 10,
    zone4 = 11,
}

public class Piece : MonoBehaviour
{
    public PieceType type;
    public int visualIndex;
}

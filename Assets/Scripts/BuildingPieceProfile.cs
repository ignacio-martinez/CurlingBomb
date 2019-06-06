using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Building Piece Profile")]
public class BuildingPieceProfile : ScriptableObject
{
    public float forceFactor = 40f;
    public bool isTargetPiece = false;
    public float destroySpeed = 1;
    public Color aliveColor = Color.green;
    public Color deadColor = Color.red;
}

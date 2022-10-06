using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int gameLevel;
    public int dwarfMovecount;
    public int giantMovecount;
    public int humanMovecount;

    public bool maskOn;
    public float tileSize;
    public float tileGapLength; // the length between tiles, mainlt used in PlayerMovement.cs
    public bool differentCameraView; // Whether the view size of each player is different

    public Vector3[] cameraViews;
}

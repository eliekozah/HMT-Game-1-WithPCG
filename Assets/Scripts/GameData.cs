using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [System.Serializable]
    public class CharacterConfig {
        [Tooltip("Name for this character type. Usually just Dwarf, Giant, or Human.")]
        public string name;
        [Tooltip("Movement Limit.")]
        [Min(1)]
        public int movement;
        [Tooltip("How many adjacent tiles they can see. Currently for reference only.")]
        [Min(1)]
        public int sightRange;
        [Tooltip("Where to initial place the camera realtive to the character. Currently for reference only.")]
        public Vector3 cameraPosition;
        [Tooltip("The faces on the player die. Currently for reference only.")]
        public int[] dieFaces = new int[6];
        

    }

    public int gameLevel;
    public int dwarfMovecount;
    public int giantMovecount;
    public int humanMovecount;

    public CharacterConfig dwarfSettings;
    public CharacterConfig giantSettings;
    public CharacterConfig humanSettings;

    public bool maskOn;
    public float tileSize;
    public float tileGapLength; // the length between tiles, mainlt used in PlayerMovement.cs
    public bool differentCameraView; // Whether the view size of each player is different

    public Vector3[] cameraViews;
}

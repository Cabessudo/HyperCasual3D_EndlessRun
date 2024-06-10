using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOLevelPiece : ScriptableObject
{
    public ArtManager.ArtType artType;
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> startLevelPiece;
    public List<LevelPieceBase> endLevelPiece;
    public List<LevelPieceBase> posEndLevelPiece;
    public int phaseLvl;

    public int startPiece = 2;
    public int pieces = 10;
    public int endPiece = 11;
    public int posEndPiece;
    public float delayToSpawn = 1;
}

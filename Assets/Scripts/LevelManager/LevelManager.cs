using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [Header("Spawn Level")]
    public SOLevelManager soLevelManager;
    public Transform container;
    public int index;

    [Header("Spawn Level Pieces")]
    public List<SOLevelPiece> currentLevelPieceSetup;
    private List<LevelPieceBase> _spawnedLevelPiece = new List<LevelPieceBase>();
    
    [Header("Animation")]
    private Ease easeBounce = Ease.OutBounce;
    public float durationScale = .2f;
    public float timeBetweenPieces = .3f;

    

    void Awake()
    {
        index = soLevelManager.currLevel;
    }

    void Start()
    {
        SpawnLevelPiece();
    }

    void Update()
    {
        NextPhase();

        
    }

    
    public void SpawnLevelPiece()
    {
        CleanLevelPieces();

        for(int i = 0; i < currentLevelPieceSetup[index].startPiece; i++)
        {
            SpawnNextLevelPiece(currentLevelPieceSetup[index].startLevelPiece);
        }

        for(int i  = 0; i < currentLevelPieceSetup[index].pieces; i++)
        {
            SpawnNextLevelPiece(currentLevelPieceSetup[index].levelPieces);
        }

        for(int i = 0; i < currentLevelPieceSetup[index].endPiece; i++)
        {
            SpawnNextLevelPiece(currentLevelPieceSetup[index].endLevelPiece);
        }

        for(int i = 0; i < currentLevelPieceSetup[index].posEndPiece; i++)
        {
            SpawnNextLevelPiece(currentLevelPieceSetup[index].posEndLevelPiece);
        }

        ColorManager.Instance.ChangeColorByType(currentLevelPieceSetup[index].artType);
        // StartCoroutine(ScalePiecesByTime());
    }

    void SpawnNextLevelPiece(List<LevelPieceBase> list)
    {
        var indexPiece = Random.Range(0, list.Count);
        var spawnedPiece = Instantiate(list[indexPiece], container);

        if(_spawnedLevelPiece.Count > 0)
        {
            var lastpiece = _spawnedLevelPiece[_spawnedLevelPiece.Count -1];
            spawnedPiece.transform.position = lastpiece.endPiece.position;
        }
        else
        {
            spawnedPiece.transform.localPosition = Vector3.zero;
        }

        foreach(var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            if(p != null)
            p.ChangeArtType(ArtManager.Instance.GetSetupByType(currentLevelPieceSetup[index].artType));
        }

        _spawnedLevelPiece.Add(spawnedPiece);
    }

    IEnumerator ScalePiecesByTime()
    {
        var currScale = transform.localScale;

        foreach(var p in _spawnedLevelPiece)
        {
            p.transform.localScale = Vector3.zero;
        }

        yield return null;

        for(int i = 0; i < _spawnedLevelPiece.Count; i++)
        {
            _spawnedLevelPiece[i].transform.DOScale(currScale, durationScale).SetEase(easeBounce);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }

    void CleanLevelPieces()
    {
        for(int i = _spawnedLevelPiece.Count -1; i >= 0; i--)
        {
            Destroy(_spawnedLevelPiece[i].gameObject);
        }

        _spawnedLevelPiece.Clear();
    }

    void NextPhase()
    {
        if(PlayerController.Instance.finish && soLevelManager.value < currentLevelPieceSetup[index].phaseLvl)
        soLevelManager.value = currentLevelPieceSetup[index].phaseLvl;
    }

    
    void ChangeLevel()
    {
        CleanLevelPieces();
        index++;
        if(currentLevelPieceSetup.Count <= index) index = 0;
        SpawnLevelPiece();
    }

    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class ArtManager : Singleton<ArtManager>
{
    public enum ArtType
    {
        Type_01,
        Type_02,
        Type_03
    }
        public List<ArtSetup> currSetup;

    public GameObject GetSetupByType(ArtType currArtType)
    {
        var setup = currSetup.Find(i => i.artType == currArtType);
        return setup.art;
    }

    [System.Serializable]
    public class ArtSetup
    {
        public ArtManager.ArtType artType;
        public GameObject art;
    }
}

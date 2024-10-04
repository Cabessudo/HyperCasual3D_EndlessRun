using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using System.IO;

namespace Save
{

    public class SaveManager : Singleton<SaveManager>
    {
        [HideInInspector] public SaveLayout saveLayout
        {
            get{ return _saveLayout; }
        }

        [SerializeField] private SaveLayout _saveLayout;

        public override void Awake()
        {
            base.Awake();
            Load();
        }

        #region GetSaves

        public PowerupSave GetPowerupSaveByType(PowerupType type)
        {
            var pwup = _saveLayout.powerups.Find(i => i.pwupType == type);
            if(pwup == null)
                return null;
            else
                return pwup;
        }

        public LevelLockSave GetLevelLockByType(LevelType type)
        {
            var lvlLock = _saveLayout.levelButtonsLock.Find(i => i.levelType == type);
            if(lvlLock == null)
                return null;
            else
                return lvlLock;
        }
        #endregion

        
        #region  Save
        [NaughtyAttributes.Button]
        public void CreateNewSave()
        {
            Debug.Log("New Save");
            _saveLayout.coins.Clear();
            _saveLayout.audio.Clear();
            _saveLayout.level.Clear();
            _saveLayout.levelButtonsLock.ForEach(i => i.Clear());
            _saveLayout.powerups.ForEach(i => i.Clear());
            Save();
        }

        [NaughtyAttributes.Button]
        public void SaveButton()
        {
            Save();
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(_saveLayout);
            File.WriteAllText(Application.persistentDataPath + "file.save", json);
            Debug.Log(json);
        }

        public void Load()
        {
            string path = Application.persistentDataPath + "file.save";
            if(File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _saveLayout = JsonUtility.FromJson<SaveLayout>(json);
                Debug.Log(json);
            }
            else
            {
                CreateNewSave();
            }
        }
        #endregion
    }

    [System.Serializable]
    public class SaveLayout
    {
        public CoinsSave coins;
        public AudioSave audio;
        public LevelSave level;
        public List<LevelLockSave> levelButtonsLock;
        public List<PowerupSave> powerups;
    }
}

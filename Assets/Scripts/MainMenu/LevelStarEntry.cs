using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelStarEntry : MonoBehaviour
{
    [Serializable]
    public class Data
    {
        public int Id = 0;
        public bool IsUnlocked = false;
    }    

    [Header("References")]
    [SerializeField] private Image iconImage = null;

    public Data CurrentData { get; private set; }

    public void Init(Data data)
    {
        CurrentData = data;

        RefreshIconImage();
    }

    private void RefreshIconImage()
    {
        iconImage.color = CurrentData.IsUnlocked ? Color.yellow : Color.white;
    }
}

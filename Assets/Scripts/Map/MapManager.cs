using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager Instance { get; set; }
    
        #region Public Functions

        public static MapManager GetInstance()
        {
            return Instance;
        }

        #endregion
        
        #region Private Functions

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        #endregion
    }
}
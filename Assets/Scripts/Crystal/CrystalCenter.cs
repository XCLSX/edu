using UnityEngine;

namespace Crystal
{
    public class CrystalCenter : MonoBehaviour
    {
        private static CrystalCenter Instance { get; set; }

        #region Private Functions

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        #endregion


        #region Public Functions

        public static CrystalCenter GetInstance()
        {
            return Instance;
        }

        #endregion
    }
}
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace Cage
{
    public class MonsterCage : MonoBehaviour
    {
        [SerializeField]
        private GameObject monsterPrefab;
        private List<Monster> _monsterList = new List<Monster>();
        [SerializeField] private float createMonsterCd = 1f;
        private float _deltaTime = 0f;

        #region Public Functions

        public void RemoveMonster(Monster monster)
        {
            _monsterList.Remove(monster);
        }
        
        #endregion
        
        #region Private Functions

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            _deltaTime += Time.deltaTime;
            if (_deltaTime >= createMonsterCd)
            {
                _deltaTime = 0f;
                CreateMonster();
            }
        }

        private void CreateMonster()
        {
            var mapTransform = MapManager.GetInstance().transform;
            var go = Instantiate(monsterPrefab, mapTransform);
            go.transform.position = transform.position;
            var monsterScript = go.transform.GetComponent<Monster>();
            _monsterList.Add(monsterScript);
            monsterScript.SetMonsterCage(this);
        }
        #endregion
    }
}
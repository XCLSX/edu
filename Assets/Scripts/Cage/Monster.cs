using System;
using UnityEngine;
using Crystal;
using Unity.VisualScripting;

namespace Cage
{
    public class Monster : MonoBehaviour
    {
        private MonsterCage _monsterCage;
        private Rigidbody2D _rigidbody;
        [SerializeField] private float speed;

        private GameObject _crystalPrefab;

        #region Public Functions

        public void SetMonsterCage(MonsterCage monsterCage)
        {
            _monsterCage = monsterCage;
        }

        #endregion

        #region Privat Functions

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        void FixedUpdate()
        {
            CheckAction();
        }

        void CheckAction()
        {
            MoveToCrystal();
        }

        void MoveToCrystal()
        {
            var toward = CrystalCenter.GetInstance().transform.position - transform.position;
            _rigidbody.linearVelocity = toward * speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
            }
            else if (collision.gameObject.CompareTag("Crystal"))
            {
                Die();
            }
        }

        private void Die()
        {
            _monsterCage.RemoveMonster(this);
            Destroy(gameObject);
        }

        #endregion
    }
}
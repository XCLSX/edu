using System;
using UnityEngine;

namespace Role
{
    public class RoleCtl : MonoBehaviour
    {
        private static RoleCtl Instance { get; set; }
        [SerializeField] private float speed = 1.0f;

        private Rigidbody2D _rigidbody;

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
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        void FixedUpdate()
        {
            CheckMove();
        }

        void CheckMove()
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");
            var moveMovement = new Vector3(moveHorizontal * speed, moveVertical * speed, 0);
            _rigidbody.linearVelocity = moveMovement;
        }

        #endregion

        #region Public Functions

        public static RoleCtl GetInstance()
        {
            return Instance;
        }

        #endregion
    }
}
using UnityEngine;

namespace Role
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Rigidbody2D _rigidbody2D;
        #region Private Functions

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        #endregion
     
        #region Public Functions

        public void SetDirection(Vector2 direction)
        {
            _rigidbody2D.AddForce(direction * speed, ForceMode2D.Force);
        }
        #endregion
    }
}

using UnityEngine;

namespace Role
{
    public class RoleCtl : MonoBehaviour
    {
        private static RoleCtl Instance { get; set; }
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform fireTransform;
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
            CheckFire();
        }

        void FixedUpdate()
        {
            CheckMove();
            CheckRotation();
        }

        void CheckMove()
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");
            var moveMovement = new Vector2(moveHorizontal * speed, moveVertical * speed);
            _rigidbody.linearVelocity = moveMovement;

            // var moveHorizontal = Input.GetAxis("Horizontal");
            // var moveVertical = Input.GetAxis("Vertical");
            // var moveMovement = new Vector3(moveHorizontal, moveVertical, 0) * Time.deltaTime;
            // transform.position += moveMovement * speed;

            // var moveHorizontal = Input.GetAxis("Horizontal");
            // var moveVertical = Input.GetAxis("Vertical");
            // var moveMovement = new Vector3(moveHorizontal, moveVertical, 0) * Time.deltaTime;
            // transform.position += moveMovement * speed;
        }

        void CheckRotation()
        {
            if (Camera.main != null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // 计算角色与鼠标之间的方向向量
                Vector2 direction = new Vector2(mousePosition.x - transform.position.x,
                    mousePosition.y - transform.position.y);

                // 计算角色的角度，使其朝向鼠标
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                // 旋转角色，设置角色的朝向
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            StopRigidbody();
        }

        void StopRigidbody()
        {
            // 将刚体的线性速度设为 0
            _rigidbody.linearVelocity = Vector2.zero;
        }

        void CheckFire()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateBullet();
            }
        }

        void CreateBullet()
        {
            var bullet = Instantiate(bulletPrefab, fireTransform.position, Quaternion.identity,
                transform.parent);
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDirection(transform.up);
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
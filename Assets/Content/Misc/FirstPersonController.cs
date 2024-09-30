using UnityEngine;

namespace Content.Misc
{
    public class FirstPersonController : MonoBehaviour
    {
        public float MoveSpeed = 5f;
        public float MouseSensitivity = 2f;

        private CharacterController _characterController;
        private Vector3 _moveDirection = Vector3.zero;
        private float _rotationX = 0;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            var current_transform = transform;
            
            var rotationY = Input.GetAxis("Mouse X") * MouseSensitivity;
            transform.Rotate(0, rotationY, 0);

            _rotationX -= Input.GetAxis("Mouse Y") * MouseSensitivity;
            _rotationX = Mathf.Clamp(_rotationX, -90, 90);
            Camera.main.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);

            var moveX = Input.GetAxis("Horizontal") * MoveSpeed;
            var moveZ = Input.GetAxis("Vertical") * MoveSpeed;

            _moveDirection = current_transform.right * moveX + current_transform.forward * moveZ;
            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }
}

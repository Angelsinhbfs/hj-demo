using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HumanJoystick.Scripts
{
    public class DebugControls : MonoBehaviour
    {
        public float lookSpeed;
        public Transform controlOrigin;
        public Transform camTrans;
        public Vector2 inputVector = new Vector2();
        public InputActionReference look;
        public InputActionReference move;

#if !UNITY_EDITOR
        private void Start()
        {
            gameObject.SetActive(false);
        }
#endif
        private void Update()
        {
            //look around, but only horizontally
            camTrans.Rotate(0, look.action.ReadValue<Vector2>().x * lookSpeed, 0);
            controlOrigin.forward = camTrans.forward;
            //easy move relative to look direction
            inputVector = move.action.ReadValue<Vector2>();
            camTrans.position = controlOrigin.TransformPoint(inputVector.x, 1.5f, inputVector.y);
        }
    }
}
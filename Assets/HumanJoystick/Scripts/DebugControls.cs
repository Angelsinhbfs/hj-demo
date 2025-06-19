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

        private void Update()
        {
            camTrans.Rotate(0, look.action.ReadValue<Vector2>().x * lookSpeed, 0);
            controlOrigin.forward = camTrans.forward;
            inputVector = move.action.ReadValue<Vector2>();
            //inputVector.y = Input.GetAxis("Vertical");
            camTrans.position = controlOrigin.TransformPoint(inputVector.x, 1.5f, inputVector.y);
        }
    }
}
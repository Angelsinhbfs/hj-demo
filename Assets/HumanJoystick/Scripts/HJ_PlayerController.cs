using System;
using UnityEngine;

namespace HumanJoystick.Scripts
{
    public class HJ_PlayerController : MonoBehaviour
    {
        public GameObject hmd;
        public GameObject locomotion;
        public GameObject root;
        public float linearSpeed;
        public float rotSpeed;
        public float slowing;
        public float deadZone;
        public float distanceCap;

        private float inputSpeed;
        private Vector2 inputDir;

        private void Update()
        {
            GatherInput();
        }

        private void FixedUpdate()
        {
            UpdateLocomotion();
        }


        private void GatherInput()
        {
            var hmdLocal = locomotion.transform.worldToLocalMatrix * hmd.transform.position;
            var projectedPos = new Vector2(hmdLocal.x, hmdLocal.z);
            var displacement = projectedPos.magnitude;

            inputSpeed = Mathf.Lerp(0, linearSpeed, (displacement - deadZone) / (distanceCap - deadZone));
            print($"input speed: {inputSpeed}");
            inputDir = displacement > deadZone ? projectedPos : Vector2.zero;
            print($"input direction {inputDir.x}, {inputDir.y}");
        }

        private void UpdateLocomotion()
        {
            locomotion.transform.rotation = Quaternion.Slerp(locomotion.transform.rotation,
                Quaternion.Euler(0, hmd.transform.rotation.eulerAngles.y, 0), rotSpeed * Time.deltaTime);
            if (inputSpeed == 0)
            {
                root.GetComponent<Rigidbody>().linearVelocity *= slowing * Time.deltaTime;
            }
            else
            {
                var velocity = root.GetComponent<Rigidbody>().linearVelocity;
                velocity += (Vector3)(locomotion.transform.localToWorldMatrix * new Vector3(inputDir.x, 0, inputDir.y) * inputSpeed);
                if (velocity.magnitude > linearSpeed)
                {
                    velocity = velocity.normalized * linearSpeed;
                }

                root.GetComponent<Rigidbody>().linearVelocity = velocity;
            }
        }

        public void Recenter()
        {
            
        }
    }
}
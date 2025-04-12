﻿using UnityEngine;

namespace AE
{
    public class LightSource : MonoBehaviour
    {
    }

    [RequireComponent(typeof(Interactor))]
    public class Grabber : MonoBehaviour
    {
        [SerializeField]
        private Transform leftHandObjectHolder;
        [SerializeField]
        private Transform rightHandObjectHolder;

        [Header("Candle")]
        [SerializeField]
        private string lightSourceTag;

        [Header("States")]
        [SerializeField]
        private Rigidbody leftHandObject;
        [SerializeField]
        private Rigidbody rightHandObject;

        public void GrabObject(Rigidbody grabbedObject, GrabHand targetHand)
        {
            bool isLeftHand = targetHand == GrabHand.Left;
            if (isLeftHand)
                leftHandObject = grabbedObject;
            else
                rightHandObject = grabbedObject;

            var holder = isLeftHand ? leftHandObjectHolder : rightHandObjectHolder;
            grabbedObject.isKinematic = true;
            grabbedObject.transform.SetParent(holder, false);
            grabbedObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            if (grabbedObject.CompareTag(lightSourceTag) && TryGetComponent<LightSource>(out _) == false)
                gameObject.AddComponent<LightSource>();
        }

        public bool TryGetGrabbedObject(GrabHand hand, out Rigidbody grabbedObject)
        {
            grabbedObject = hand == GrabHand.Left ? leftHandObject : rightHandObject;
            return grabbedObject != null;
        }

        public void DropObject(GrabHand hand)
        {
            bool isLeftHand = hand == GrabHand.Left;
            var grabbedObject = isLeftHand ? leftHandObject : rightHandObject;
            grabbedObject.transform.parent = null;
            grabbedObject.isKinematic = false;

            if (isLeftHand)
                leftHandObject = null;
            else
                rightHandObject = null;
        }
    }
}

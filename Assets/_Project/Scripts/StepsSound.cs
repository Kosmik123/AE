using System;
using UnityEngine;

namespace AE
{
    public class StepsSound : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip[] stepSounds;

        [SerializeField]
        private float stepLength = 1;
        private Vector3 previousPosition;

		private void OnEnable()
		{
			previousPosition = transform.position;
		}

		private void Update()
		{
            var currentPosition = transform.position;
            float sqrDistance = (currentPosition - previousPosition).sqrMagnitude;
            if (sqrDistance > stepLength * stepLength)
			{
				PlayStepSound();
				previousPosition = currentPosition;
			}
		}

		private void PlayStepSound()
		{
			if (stepSounds.Length > 0)
				audioSource.PlayOneShot(stepSounds[UnityEngine.Random.Range(0, stepSounds.Length)]);
		}
	}
}

using System;
using UnityEngine;

namespace AE
{
    public class LightSource : InteractorInteractionHandler
    {
		[SerializeField]
		private bool isLighted;
		public bool IsLighted
		{
			get => isLighted;
			set
			{
				isLighted = value;
				LightObject.SetActive(isLighted);
			}
		}

		[field: SerializeField]
        public Transform Candle { get; private set; }

        [field: SerializeField]
		public bool HasCandle { get; private set; }

		[field:SerializeField]
		public GameObject LightObject { get; private set; }

		private void Awake()
		{
			Candle.gameObject.SetActive(HasCandle);
			LightObject.SetActive(IsLighted);
		}

		public void CollectCandle()
		{
			HasCandle = true;
			Candle.gameObject.SetActive(true);
		}
	}
}

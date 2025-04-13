using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AE
{
	public class ScreenFade : MonoBehaviour
	{
		[SerializeField]
		private Image image;

		private void Start()
		{
			image.enabled = false;	
		}

		public void FadeOut(float duration)
		{
			image.gameObject.SetActive(true);
			image.enabled = true;
			image.color = new Color(0, 0, 0, 0.01f);
			StartCoroutine(FadeCo(1, duration));
		}

		private IEnumerator FadeCo(float targetAlpha, float duration)
		{
			var color = image.color;
			float alpha = color.a;
			while (alpha != targetAlpha)
			{ 
				alpha = Mathf.MoveTowards(alpha, targetAlpha, Time.deltaTime / duration);
				color.a = alpha;
				image.color = color;
				yield return null;
			}

			color.a = targetAlpha;
			image.color = color;
		}

	}
}

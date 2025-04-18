using UnityEngine;

namespace AE
{
	public abstract class Torch : MonoBehaviour
    {
        public static event System.Action OnTorchLighedUp;
        protected static void InvokeEvent() => OnTorchLighedUp?.Invoke();
    }

    public class SingleTorch : Torch
    {
        private const string EmissionKeyword = "_EMISSION";

        [SerializeField]
        private MeshRenderer meshRenderer;
        [SerializeField]
        private int[] glowMaterialsIndices = new[] { 1 }; 
        [SerializeField]
        private Light[] lights;
        [SerializeField]
        private bool shouldToggleMeshVisibility;

        private void Reset()
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            lights = GetComponentsInChildren<Light>();
        }
        
        private void Awake()
        {
            Enable(enabled);
        }

        private void OnEnable()
        {
            Enable(true);
        }

        private void Enable(bool enable)
        {
            foreach (var light in lights)
                light.enabled = enable;

            if (shouldToggleMeshVisibility)
                meshRenderer.enabled = enable;

            if (Application.isPlaying)
            {
                foreach (var index in glowMaterialsIndices)
                {
                    if (index < meshRenderer.materials.Length)
                    {
                        if (enable)
                            meshRenderer.materials[index].EnableKeyword(EmissionKeyword);
                        else
                            meshRenderer.materials[index].DisableKeyword(EmissionKeyword);
                    }
                }
            }

            if (enable)
                InvokeEvent();

		}

        private void OnDisable()
        {
            Enable(false);
        }

        private void OnValidate()
        {
            Enable(enabled);
        }
    }
}

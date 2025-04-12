using UnityEngine;

namespace AE
{
    public class Torch : MonoBehaviour
    {
        private const string EmissionKeyword = "_EMISSION";

        [SerializeField]
        private MeshRenderer meshRenderer;
        [SerializeField]
        private int[] glowMaterialsIndices = new[] { 1 }; 
        [SerializeField]
        private Light[] lights;

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

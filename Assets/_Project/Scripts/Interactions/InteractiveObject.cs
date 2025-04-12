using UnityEngine;

namespace AE
{
    public class InteractiveObject : MonoBehaviour
    {
        [SerializeField]
        private Interaction interaction;

        public bool TryInteract(Interactor interactor) => interaction 
            && interaction.isActiveAndEnabled 
            && interaction.TryInteract(interactor);
    }
}

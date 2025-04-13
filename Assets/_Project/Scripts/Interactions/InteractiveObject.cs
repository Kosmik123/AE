using UnityEngine;

namespace AE
{
    public interface IInteractiveObject
    {
        bool CanInteract(Interactor interactor);
        bool TryInteract(Interactor interactor);
    }

    public sealed class InteractiveObject : MonoBehaviour, IInteractiveObject
    {
        [SerializeField]
        private Interaction interaction;

        public bool CanInteract(Interactor interactor) => isActiveAndEnabled && interaction && interaction.CanInteract(interactor);

        public bool TryInteract(Interactor interactor) => interaction && interaction.TryInteract(interactor);
    }
}

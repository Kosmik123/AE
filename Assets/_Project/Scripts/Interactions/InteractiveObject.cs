using UnityEngine;

namespace AE
{
    public delegate void InteractionEventHandler(InteractiveObject interactiveObject, Interactor interactor, Interaction interaction);

    public interface IInteractiveObject
    {
        bool CanInteract(Interactor interactor);
        bool TryInteract(Interactor interactor);
    }

    public sealed class InteractiveObject : MonoBehaviour, IInteractiveObject
    {
        public event InteractionEventHandler OnInteracted;

        [SerializeField]
        private Interaction interaction;
        public Interaction Interaction
        {
            get => interaction;
            set => interaction = value; 
        }

        public bool CanInteract(Interactor interactor) => isActiveAndEnabled && interaction && interaction.CanInteract(interactor);

		public bool TryInteract(Interactor interactor)
		{
			bool success = interaction && interaction.TryInteract(interactor);
            if (success)
                OnInteracted?.Invoke(this, interactor, interaction);
            return success;
        }
	}
}

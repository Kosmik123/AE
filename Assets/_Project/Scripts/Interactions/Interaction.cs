using UnityEngine;

namespace AE
{
    public abstract class Interaction : MonoBehaviour
    {
        public abstract bool TryInteract(Interactor interactor);

        public virtual bool CanInteract(Interactor interactor) => isActiveAndEnabled;
    }
}

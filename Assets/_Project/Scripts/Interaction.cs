using UnityEngine;

namespace AE
{
    public abstract class Interaction : MonoBehaviour
    {
        public abstract bool TryInteract(Interactor interactor);
    }
}

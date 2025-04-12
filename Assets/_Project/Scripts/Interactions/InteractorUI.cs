using UnityEngine;

namespace AE
{
    public class InteractorUI : MonoBehaviour
    {
        [SerializeField]
        private Interactor interactor;

        [SerializeField]
        private GameObject inactiveIndicator;
        [SerializeField]
        private GameObject activeIndicator;

        private void OnEnable()
        {
            interactor.OnInteractiveObjectChanged += Interactor_OnInteractiveObjectChanged; 
        }

        private void Interactor_OnInteractiveObjectChanged(Interactor interactor, InteractiveObject from, InteractiveObject to)
        {
            inactiveIndicator.SetActive(to == null);
            activeIndicator.SetActive(to != null);
        }

        private void OnDisable()
        {
            interactor.OnInteractiveObjectChanged -= Interactor_OnInteractiveObjectChanged; 
        }
    }
}

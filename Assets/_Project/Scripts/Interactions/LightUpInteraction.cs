using DG.Tweening;
using UnityEngine;

namespace AE
{
    public class LightUpInteraction : Interaction
    {
        [SerializeField]
        private Torch torch;
        [SerializeField]
        private Transform candleLightingPoint;

        [SerializeField]
        private float moveToTorchDuration = 1;
        [SerializeField]
        private float middleWaitingDuration = 0.5f;
        [SerializeField]
        private float moveBackDuration = 1;

        public override bool CanInteract(Interactor interactor)
        {
            if (base.CanInteract(interactor) == false)
                return false;

            if (interactor.TryGetComponent<LightSource>(out var lightSource) == false)
                return false;

            return lightSource.IsLighted != torch.enabled;
        }

        public override bool TryInteract(Interactor interactor)
        {
            if (interactor.TryGetComponent<LightSource>(out var lightSource) == false)
                return false;

            var playerMovement = interactor.GetComponentInParent<SimplePlayerMovement>();
            if (playerMovement == null)
                return false;

            interactor.enabled = false;
            interactor.DisableMovement();
            var candle = lightSource.Candle;

            TweenCallback lightingAction = lightSource.IsLighted ? LightUpTorch : LightUpSource;

            DOTween.Sequence()
                .Append(candle.DOMove(candleLightingPoint.position, moveToTorchDuration))
                    .Join(candle.DORotateQuaternion(candleLightingPoint.rotation, moveToTorchDuration))
                .AppendInterval(middleWaitingDuration)
                .AppendCallback(lightingAction)
                .Append(candle.DOLocalMove(Vector3.zero, moveBackDuration))
                    .Join(candle.DOLocalRotateQuaternion(Quaternion.identity, moveBackDuration))
                .AppendCallback(EnableInteractor)
                .Play();

            return true;

            void LightUpTorch()
            {
                torch.enabled = true;
            }

            void LightUpSource()
            {
                lightSource.IsLighted = true;
            }

            void EnableInteractor()
            {
                interactor.enabled = true;
                interactor.EnableMovement();
            }
        }

    }
}

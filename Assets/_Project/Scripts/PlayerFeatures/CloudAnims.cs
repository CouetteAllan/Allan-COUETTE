using DG.Tweening;
using UnityEngine;

public class CloudAnims : MonoBehaviour
{
    [SerializeField] private Transform _visuals;
    [SerializeField] private float _floatingValue = .5f;

    private Vector3 _baseRotation, _basePosition;

    private void Start()
    {
        _baseRotation = _visuals.localRotation.eulerAngles;
        _basePosition = _visuals.localPosition;
        FloatAnim();
    }

    public void RainAnim()
    {
        _visuals.DOKill();
        _visuals.localRotation = Quaternion.Euler(_baseRotation);
        _visuals.localPosition = _basePosition;
        _visuals.DOPunchScale(Vector3.one * .2f, 1.0f, vibrato: 4, elasticity: .1f).SetLoops(-1).SetRelative();
    }

    public void FloatAnim()
    {
        _visuals.DOKill();
        _visuals.DOBlendableLocalMoveBy(Vector3.down * _floatingValue, 1.0f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        _visuals.DORotate(Vector3.right * -7.0f, 1.0f).SetRelative();
        _visuals.DORotate(Vector3.right * 20.0f, 4.0f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).SetDelay(1.1f).SetRelative();
    }
}

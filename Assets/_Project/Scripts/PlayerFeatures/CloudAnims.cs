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
        _visuals.DOLocalRotate(new Vector3(1,90,0) * 9.0f, 1.0f);
        _visuals.DOLocalRotate(new Vector3(-1, 90, 0) * 9.0f, 2.0f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).SetDelay(1.1f);
    }

    public void VictoryAnim()
    {
        _visuals.DOKill();
        _visuals.DOLocalMove(Vector3.right * -2.0f, .3f);
        _visuals.DOLocalMove(Vector3.right * 2.0f, 1.0f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo).SetDelay(.3f);
        _visuals.DOBlendableLocalRotateBy(Vector3.forward * 360.0f, 1f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo).SetDelay(.3f);
    }
}

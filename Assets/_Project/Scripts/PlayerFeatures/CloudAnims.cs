using DG.Tweening;
using UnityEngine;

public class CloudAnims : MonoBehaviour
{
    [SerializeField] private Transform _visuals;
    [SerializeField] private float _floatingValue = .5f;
    [SerializeField] private float _punchRotationValue = 1.0f;

    private void Start()
    {
        _visuals.DOBlendableLocalMoveBy(Vector3.down * .5f,1.0f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        _visuals.DORotate(Vector3.right * -15.0f, 1.0f).SetRelative();
        _visuals.DORotate(Vector3.right * 30.0f, 6.0f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).SetDelay(1.1f).SetRelative();  
    }
}

using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;

public class BuildingVisuals : MonoBehaviour
{
    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private ParticleSystem _healingEffect;
    [SerializeField] private ParticleSystem[] _fireEffects;

    [Header("All MM Player")]
    [SerializeField] private MMFeedbacks _feedbacksStartFire;
    [SerializeField] private MMFeedbacks _feedbacksDestroy;
    [SerializeField] private MMFeedbacks _feedbacksVictory;

    [Header("Settings for DOTween")]
    [SerializeField] private Transform _buildingTransform;
    [SerializeField] private MeshRenderer _buildingRenderer;

    private Material _buildingMaterial;
    private Color _startColor;

    private void Awake()
    {
        _buildingMaterial = _buildingRenderer.material;
        _startColor = _buildingMaterial.color;
    }

    public void PlayExplosionEffect()
    {
        _feedbacksStartFire.PlayFeedbacks();
        _explosionEffect.Play();
    }

    public void ActivateFire(int index)
    {
        index = Mathf.Clamp(index, 0, _fireEffects.Length - 1);
        for(int i = 0; i <= index; i++)
        {
            _fireEffects[i].Play();
        }
        UpdateBuildingColor(index);
    }

    public void StopAllFires()
    {
        UpdateBuildingColor(-1);
        foreach (var fire in _fireEffects)
        {
            fire.Stop();
        }
    }

    public void StartHealingBuilding()
    {
        _healingEffect.Play();
        _buildingTransform.DOBlendableScaleBy(new Vector3(0.5f, 1, 0.5f) * .2f, .4f).SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void StopHealingEffect()
    {
        _healingEffect.Stop();
        _buildingTransform.DOScale(Vector3.one, .4f).OnComplete(() => _buildingTransform.DOKill());
    }

    public void DestroyBuilding()
    {
        //Destroy feedback;
        StopAllFires();
        _feedbacksDestroy.PlayFeedbacks();
    }

    public void UpdateBuildingColor(int flameIndex)
    {
        Color finalColor = new Color();
        switch (flameIndex)
        {
            case 0:
                finalColor = new Color(1.0f,0.6f,0.6f);
                break;
            case 1:
                finalColor = new Color(1.0f,0.3f,0.3f);
                break;
            case 2:
                finalColor = Color.red;
                break;
            default:
                finalColor = _startColor;
                break;

        }
        _buildingMaterial.DOColor(finalColor, 1.5f);
    }

    public void PlayVictoryFeedback()
    {
        _feedbacksVictory.PlayFeedbacks();
        _buildingTransform.DOBlendableScaleBy(Vector3.up * 1.2f, 0.5f).SetEase(Ease.InOutBack).SetLoops(-1, LoopType.Yoyo);
        _buildingTransform.DOLocalRotate(Vector3.forward * -10.0f, .1f).OnComplete(() =>
        {
            _buildingTransform.DOLocalRotate(Vector3.forward * 20.0f, 1.0f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo).SetRelative();
        });
    }
}

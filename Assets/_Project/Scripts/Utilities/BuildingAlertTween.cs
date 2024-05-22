using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingAlertTween : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Awake()
    {
        BuildingManager.OnUpdateBuildingOnFire += OnBuildingOnFire;
    }

    private void OnBuildingOnFire()
    {
        SetBuildingText(BuildingManager.Instance.BuildingOnFireCount);
    }

    private void SetBuildingText(int buildingNb)
    {
        _text.text = buildingNb.ToString();

        if (buildingNb > 0)
        {
            _canvasGroup.DOFade(1.0f, .2f);
            //Bump;
            this.transform.DOPunchScale(new Vector3(1.2f, .7f), .2f, vibrato: 4, elasticity: .4f);
        }
        else
        {
            this.transform.DOBlendableScaleBy(new Vector3(1, 1, 0), .5f).SetLoops(2, LoopType.Yoyo);
            _canvasGroup.DOFade(0.0f, .2f).SetDelay(1.5f);
        }
        
    }

    private void OnDisable()
    {
        BuildingManager.OnUpdateBuildingOnFire -= OnBuildingOnFire;

    }
}

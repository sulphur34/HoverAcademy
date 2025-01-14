using DG.Tweening;
using UnityEngine;

public class HoverChoosePad : MonoBehaviour
{
    [SerializeField] private GameObject[] _hovers;

    private void Start()
    {
        foreach (var hover in _hovers)
        {
            AnimateHover(hover);
        }
    }

    private void AnimateHover(GameObject hover)
    {
        hover.transform.DOMoveY(hover.transform.position.y + 1,1).SetLoops(-1, LoopType.Yoyo);
        hover.transform.DORotate(new Vector3(0,360,0), 2, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Flash);
    }
}
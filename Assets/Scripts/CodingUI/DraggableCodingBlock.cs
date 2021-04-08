using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableCodingBlock : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;
    private Transform _originalParent;
    private bool _inDropBlock;
    private GameObject _placeHolder = null;
    private Transform _dropBlockParent;
    private int _originalSiblingIndex;
    private PuzzleBlock _answerBlock;

    private void Awake()
    {
        _originalSiblingIndex = transform.GetSiblingIndex();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _originalParent = transform.parent;

    }

    public void SetAnswerBlock(PuzzleBlock answerBlock)
    {
        _answerBlock = answerBlock;
        GetComponentInChildren<Text>().text = answerBlock.Content;
    }

    public PuzzleBlock GetAnswerBlock()
    {
        return _answerBlock;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;

        // Makes sure the dragging block is shown on top of everything
        transform.SetParent(_canvas.transform);
        if (_inDropBlock) return;

        transform.parent.SetAsLastSibling();
        CreatePlaceHolder();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;

        if (_inDropBlock)
        {
            transform.SetParent(_dropBlockParent);
            _rectTransform.anchoredPosition = Vector2.zero;
        } else
        {
            SetOriginalParent();
        }
    }

    private void CreatePlaceHolder()
    {
        _placeHolder = new GameObject();
        _placeHolder.AddComponent<LayoutElement>();
        RectTransform rt = _placeHolder.GetComponent<RectTransform>();
        rt.sizeDelta = _rectTransform.sizeDelta;

        _placeHolder.transform.SetParent(_originalParent);
        _placeHolder.transform.SetSiblingIndex(_originalSiblingIndex);
    }

    public void SetDropBlockOrOriginalParent(DraggableCodingBlock dragBlock)
    {
        if (dragBlock._inDropBlock)
        {
            SetDropBlockAsParent(dragBlock._dropBlockParent);
            OnEndDrag(null);
        }
        else SetOriginalParent();
    }

    public void SetOriginalParent()
    {
        transform.SetParent(_originalParent);
        transform.SetSiblingIndex(_originalSiblingIndex);
        Destroy(_placeHolder);

        _inDropBlock = false;
        _canvasGroup.blocksRaycasts = true;
    }

    public void SetDropBlockAsParent(Transform newParent)
    {
        _dropBlockParent = newParent;
        _inDropBlock = true;

        _rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        _rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        _rectTransform.anchoredPosition = Vector2.zero;
    }
}

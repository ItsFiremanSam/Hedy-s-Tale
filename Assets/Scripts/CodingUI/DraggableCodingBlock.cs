using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableCodingBlock : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
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

    public RectTransform HelpTooltip { get; set; }
    public Text ExplanationText { get; set; }
    public Vector3 TooltipOffset { get; set; }
    public int DelayAmount { get; set; }

    private Coroutine _showTooltip;

    private void Awake()
    {
        _originalSiblingIndex = transform.GetSiblingIndex();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        _originalParent = transform.parent;

    }

    public void SetAnswerBlock(PuzzleBlock answerBlock)
    {
        _answerBlock = answerBlock;
        GetComponentInChildren<Text>().text = answerBlock.Content;
        GetComponentInChildren<Text>().fontSize = 20;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (string.IsNullOrWhiteSpace(_answerBlock.Explanation))
            return;

        _showTooltip = StartCoroutine(ShowTooltip());

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Cancel the showing of the tooltop
        if (_showTooltip != null)
            StopCoroutine(_showTooltip);

        HelpTooltip.gameObject.SetActive(false);
    }

    private IEnumerator ShowTooltip()
    {
        yield return new WaitForSeconds(DelayAmount);

        HelpTooltip.gameObject.SetActive(true);
        ExplanationText.text = _answerBlock.Explanation;

        yield return null;
    }

    private void Update()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {
        if (HelpTooltip == null || !HelpTooltip.gameObject.activeSelf)
            return;

        Vector3 newPos = Input.mousePosition + (TooltipOffset * _canvas.scaleFactor);
        newPos.z = 0f;

        float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + _rectTransform.rect.width * _canvas.scaleFactor / 2);
        if (rightEdgeToScreenEdgeDistance < 0)
        {
            newPos.x += rightEdgeToScreenEdgeDistance;
        }

        float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - _rectTransform.rect.width * _canvas.scaleFactor / 2);
        if (leftEdgeToScreenEdgeDistance > 0)
        {
            newPos.x += leftEdgeToScreenEdgeDistance;
        }

        float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + _rectTransform.rect.height * _canvas.scaleFactor);
        if (topEdgeToScreenEdgeDistance < 0)
        {
            newPos.y += topEdgeToScreenEdgeDistance;
        }

        HelpTooltip.transform.position = newPos;
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class RadialSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private const string PREFS_UI_SAVE_NAME = "Radial";

    [Header("TIMER")]
    [SerializeField, Tooltip("The gap between each timing before the picture changes, timings can only be set according to the interval, so no inbetween.")]
    private int timeInterval = 5;
    [Tooltip("The current time selected by the slider")]
    public float CurrentValue = 10.0f;
    [Tooltip("The maximum time allowed")]
    public float MaxValue = 100.0f;

    [Header("OBJECTS")]
    [SerializeField]
    private Image sliderImage;
    [SerializeField]
    private RectTransform indicatorPivot;
    [SerializeField]
    private TextMeshProUGUI valueText;

    [Header("SETTINGS")]
    [SerializeField]
    private int sliderID;
    [SerializeField, Range(0, 8)]
    private int decimals;
    [SerializeField]
    private bool isPercent;
    [SerializeField]
    private bool rememberValue;
    [SerializeField]
    private bool enableCurrentValue;
    [SerializeField]
    private UnityEvent onValueChanged;

    private GraphicRaycaster graphicRaycaster;
    private RectTransform hitRectTransform;
    private bool isPointerDown;
    private float currentAngle;
    private float currentAngleOnPointerDown;
    private float valueDisplayPrecision;

    /// <summary>
    /// Sets
    /// </summary>
    public float SliderAngle
    {
        get { return currentAngle; }
        set { currentAngle = Mathf.Clamp(value, 30.0f, 360.0f); }
    }

    /// <summary>
    /// Slider value with applied display precision, i.e. the number of decimals to show.
    /// </summary>
    public float SliderValue
    {
        get { return (long)(SliderValueRaw * valueDisplayPrecision) / valueDisplayPrecision; }
        set { SliderValueRaw = value; }
    }

    /// <summary>
    /// Raw slider value, i.e. without any display precision applied to its value.
    /// </summary>
    public float SliderValueRaw
    {
        get { return SliderAngle / 360.0f * MaxValue; }
        set { SliderAngle = value * 360.0f / MaxValue; }
    }

    private void Awake()
    {
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        if (graphicRaycaster == null)
        {
            Debug.LogWarning("Could not find GraphicRaycaster component in parent of this GameObject: " + name);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        valueDisplayPrecision = Mathf.Pow(10, decimals);
        LoadState();

        if (rememberValue == false && enableCurrentValue == true)
        {
            SliderAngle = (CurrentValue / MaxValue) * 360f;
            UpdateUI();

            valueText.text = string.Format("{0}{1}", 10, isPercent ? "%" : "") + ":00";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        hitRectTransform = eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>();
        isPointerDown = true;
        currentAngleOnPointerDown = SliderAngle;
        HandleSliderMouseInput(eventData, true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (HasValueChanged())
        {
            SaveState();
        }
        hitRectTransform = null;
        isPointerDown = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        HandleSliderMouseInput(eventData, false);
    }

    public void LoadState()
    {
        if (!rememberValue)
        {
            return;
        }

        currentAngle = PlayerPrefs.GetFloat(sliderID + PREFS_UI_SAVE_NAME);
    }

    public void SaveState()
    {
        if (!rememberValue)
        {
            return;
        }

        PlayerPrefs.SetFloat(sliderID + PREFS_UI_SAVE_NAME, currentAngle);
    }

    public void UpdateUI()
    {
        float normalizedAngle = SliderAngle / 360.0f;

        // Rotate indicator (handle / knob)
        //indicatorPivot.transform.localEulerAngles = new Vector3(180.0f, 0.0f, SliderAngle);

        if (normalizedAngle >= 0.875f || normalizedAngle < 0.125f)
        {
            float percentage;
            float yPos;

            if (normalizedAngle >= 0.875f)
            {
                percentage = (normalizedAngle - 0.875f) / 0.25f;

            }
            else
            {
                percentage = ((normalizedAngle / 2f) + 0.0625f) / 0.125f;
            }

            yPos = -280f + (percentage * 560f);

            indicatorPivot.anchoredPosition = new Vector3(-15f, yPos, 0f);
        }
        else if (normalizedAngle >= 0.125f && normalizedAngle < 0.375f)
        {
            float percentage = (normalizedAngle - 0.125f) / 0.25f;
            float xPos = -5f + (percentage * 560f);

            indicatorPivot.anchoredPosition = new Vector3(xPos, 280f, 0f);
        }
        else if (normalizedAngle >= 0.375f && normalizedAngle < 0.625f)
        {
            float percentage = (normalizedAngle - 0.375f) / 0.25f;
            float yPos = 280f - (percentage * 560f);

            indicatorPivot.anchoredPosition = new Vector3(540f, yPos, 0f);
        }
        else
        {
            float percentage = (normalizedAngle - 0.625f) / 0.25f;
            float xPos = 555f - (percentage * 560f);

            indicatorPivot.anchoredPosition = new Vector3(xPos, -280f, 0f);
        }

        // Update slider fill amount
        sliderImage.fillAmount = normalizedAngle;

        CurrentValue = Mathf.Round((normalizedAngle * MaxValue) / timeInterval) * timeInterval;
        valueText.text = string.Format("{0}{1}", CurrentValue, isPercent ? "%" : "") + ":00";
    }

    private bool HasValueChanged()
    {
        return SliderAngle != currentAngleOnPointerDown;
    }

    private void HandleSliderMouseInput(PointerEventData eventData, bool allowValueWrap)
    {
        if (!isPointerDown)
        {
            return;
        }

        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(hitRectTransform, eventData.position, eventData.pressEventCamera, out localPos);
        float newAngle = Mathf.Atan2(-localPos.y, localPos.x) * Mathf.Rad2Deg + 180f;
        if (!allowValueWrap)
        {
            float currentAngle = SliderAngle;
            bool needsClamping = Mathf.Abs(newAngle - currentAngle) >= 180;
            if (needsClamping)
            {
                newAngle = currentAngle < newAngle ? 0.0f : 360.0f;
            }
        }

        SliderAngle = newAngle;

        UpdateUI();

        if (HasValueChanged())
        {
            onValueChanged.Invoke();
        }
    }
}
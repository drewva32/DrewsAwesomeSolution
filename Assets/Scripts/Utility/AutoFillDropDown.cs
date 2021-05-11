using TMPro;
using UnityEngine;

public class AutoFillDropDown : MonoBehaviour
{
    [SerializeField][Range(1,100)] private float maxOptions;
    [SerializeField] private int defaultValue= 1;
    
    private TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        if (_dropdown != null)
        {
            _dropdown.options.Clear();
            for (int i = 1; i < maxOptions + 1; i++)
            {
                _dropdown.options.Add(new TMP_Dropdown.OptionData(i.ToString()));
            }
        }
        _dropdown.value = defaultValue - 1;
    }
}

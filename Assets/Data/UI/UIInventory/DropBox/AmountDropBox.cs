using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AmountDropBox : SecondMonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    public TMP_InputField InputField => _inputField;

    private int Limit = 0;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInputField();
    }

    private void LoadInputField()
    {
        if (this._inputField != null) return;
        this._inputField = transform.GetComponentInChildren<TMP_InputField>();
        Debug.LogWarning(transform.name + ": LoadInputField", gameObject);
    }

    protected override void Start()
    {
        if (_inputField == null)
        {
            Debug.LogError("InputField reference is not set in the inspector!");
            return;
        }
        _inputField.onValueChanged.AddListener(OnInputValueChanged);
        this.transform.gameObject.SetActive(false);
    }

    public void ShowBox(int Amount)
    {
        this.transform.gameObject.SetActive(true);

        this.Limit = Amount;
        _inputField.text = this.Limit.ToString();

        EventSystem.current.SetSelectedGameObject(InputField.gameObject);
    }

    public void HideBox()
    {
        this.transform.gameObject.SetActive(false);
    }

    private void OnInputValueChanged(string newValue)
    {
        if (!int.TryParse(newValue, out int number))
        {
            _inputField.text = "";
            return;
        }

        number = Mathf.Clamp(number, 1, Limit);

        this._inputField.text = number.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AmountDropBox : SecondMonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    public TMP_InputField inputField => _inputField;

/*    [SerializeField] private BtnOK _btnOk;
    public BtnOK btnOk => _btnOk;

    [SerializeField] private BtnCancel _btnCancel;*/

    private int Limit = 0;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInputField();
/*        this.LoadButtonOk();
        this.LoadButtonCancel();*/
    }

    private void LoadInputField()
    {
        if (this._inputField != null) return;
        this._inputField = transform.GetComponentInChildren<TMP_InputField>();
        Debug.LogWarning(transform.name + ": LoadInputField", gameObject);
    }

/*    private void LoadButtonOk()
    {
        if (this._btnOk != null) return;
        this._btnOk = transform.GetComponentInChildren<BtnOK>();
        Debug.LogWarning(transform.name + ": LoadButtonOk", gameObject);
    }

    private void LoadButtonCancel()
    {
        if (this._btnCancel != null) return;
        this._btnCancel = transform.GetComponentInChildren<BtnCancel>();
        Debug.LogWarning(transform.name + ": LoadButtonCancel", gameObject);
    }*/

    protected override void Start()
    {
        if (_inputField == null)
        {
            Debug.LogError("InputField reference is not set in the inspector!");
            return;
        }
        _inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    public void ShowBox(int Amount)
    {
        this.transform.gameObject.SetActive(true);

        this.Limit = Amount;
        _inputField.text = this.Limit.ToString();

        EventSystem.current.SetSelectedGameObject(inputField.gameObject);
    }

    public void HideBox()
    {
        this.transform.gameObject.SetActive(false);
    }

    private void OnInputValueChanged(string newValue)
    {
        int number;
        if (!int.TryParse(newValue, out number))
        {
            _inputField.text = "";
            return;
        }

        number = Mathf.Clamp(number, 1, Limit);

        _inputField.text = number.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUseInfo : MonoBehaviour
{
    [SerializeField] private UseInformation _useInformation;
    public UseInformation useInformation => _useInformation;

    public virtual void LinkUseInfo(UseInformation useInformation)
    {
        this._useInformation = useInformation;
    }
}

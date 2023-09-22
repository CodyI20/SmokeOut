using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIControl : MonoBehaviour
{
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _optionsUI;

    public void ToggleMenuOptionsUI()
    {
        if (_menuUI && _optionsUI)
        {
            _menuUI.SetActive(!_menuUI.activeSelf);
            _optionsUI.SetActive(!_optionsUI.activeSelf);
        }
    }
}

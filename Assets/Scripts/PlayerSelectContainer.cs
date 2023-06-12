using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PlayerSelectContainer : MonoBehaviour
    {   
        
        private Image  _image;
        private Button _button;

        private Color _enableColor  = Color.white;
        private Color _disableColor = new Color(1f, 1f, 1f, 0.5f);

        private void OnEnable()
        {
            _image  = GetComponent<Image>();
            _button = GetComponent<Button>();

            _button.onClick.AddListener(PlayerClicked);
            _image.color = _disableColor;

            GameManager.PlayerSelected += OnPlayerSelected;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(PlayerClicked);
            GameManager.PlayerSelected -= OnPlayerSelected;
        }
        
        private void OnPlayerSelected(Sprite obj)
        {
            if (obj != _image.sprite)
            {
                _image.color = _disableColor;
            }
        }

        private void PlayerClicked()
        {
            _image.color = _enableColor;
            GameManager.PlayerSelected?.Invoke(_image.sprite);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Volume : MonoBehaviour
    {
        private Color _activeColor  = Color.white;
        private Color _disableColor = new Color(1f, 1f, 1f, 0.5f);

        public  AudioSource AudioSource;
        private Button      _button;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClicked);
            _button.image.color = AudioSource.mute ? _disableColor : _activeColor;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            AudioSource.mute    = !AudioSource.mute;
            _button.image.color = AudioSource.mute ? _disableColor : _activeColor;
        }
    }
}
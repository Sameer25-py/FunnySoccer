using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SelectedPlayerContainer : MonoBehaviour
    {
        private Image  _image;
        private Sprite _defaultSprite;
        

        private void OnEnable()
        {
            _image = GetComponent<Image>();

            GameManager.PlayerSelected += OnPlayerSelected;
        }

        private void Start()
        {
            _defaultSprite = _image.sprite;
        }

        private void OnDisable()
        {
            GameManager.PlayerSelected -= OnPlayerSelected;
            _image.sprite              =  _defaultSprite;
        }
        
        

        private void OnPlayerSelected(Sprite obj)
        {
            _image.sprite = obj;
        }
    }
}
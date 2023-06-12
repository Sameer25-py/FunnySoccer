using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Loader : MonoBehaviour
    {
        public Image LoaderFill;

        private IEnumerator ProgressLoader()
        {
            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                elapsedTime           += Time.deltaTime / 2f;
                LoaderFill.fillAmount =  elapsedTime;
                yield return null;
            }

            SceneManager.LoadScene("Gameplay");
        }

        private void Start()
        {
            StartCoroutine(ProgressLoader());
        }
    }
}
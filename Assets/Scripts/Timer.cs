using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class Timer : MonoBehaviour
    {
        public float    countDownTime = 60;
        public TMP_Text CountDownText;

        private bool  _isCountDownInProgress;
        private float _elapsedTime;
        private float _remainingTime;

        public static Action TimerEnd;

        private void DecrementTimer()
        {
            _remainingTime -= 1f;
            int seconds = Mathf.FloorToInt(_remainingTime % 60f);
            int minutes = Mathf.FloorToInt(_remainingTime / 60f);
            CountDownText.text = $"{seconds:00}s";

            if (_remainingTime == 0f)
            {
                _isCountDownInProgress = false;
                TimerEnd();
            }
        }

        private void Update()
        {
            if (!_isCountDownInProgress) return;
            _elapsedTime += Time.deltaTime;
            if (!(_elapsedTime >= 1f)) return;
            _elapsedTime = 0f;
            DecrementTimer();
        }

        public void StartTimer(float duration)
        {
            countDownTime          = duration;
            _remainingTime         = countDownTime;
            CountDownText.text     = countDownTime + "s";
            _isCountDownInProgress = true;
        }

        public void PauseTimer()
        {
            _isCountDownInProgress = false;
        }

        public void ResumeTimer()
        {
            _isCountDownInProgress = true;
        }
    }
}
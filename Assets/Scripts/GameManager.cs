using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public GameObject MainMenu;
        public GameObject PlayerSelect;
        public GameObject Bracket;
        public GameObject MatchUp;
        public GameObject MatchUI;
        public GameObject Environment;
        public GameObject Loss,            Victory;
        public TMP_Text   PlayerScoreText, ComputerScoreText;

        public Image          PlayerMatchUpImage, ComputerMatchupImage;
        public BracketManager BracketManager;

        public SpriteRenderer PlayerSprite,    ComputerSprite;
        public Image          LossPlayerImage, VictoryPlayerImage;

        private Sprite _selectedPlayerSprite;

        public static Action<Sprite> PlayerSelected;
        public        Timer          Timer;

        private int matchNo = 1;

        public static Action<int> Goal;

        public Ball       Ball;
        public GameObject GoalImage, EndOfFirstHalfImage;

        private int _playerScore, _computerScore;

        private List<Vector2> _randomDirections = new()
        {
            new Vector2(-0.5f, 1f),
            new Vector2(0.5f, -1f),
            new Vector2(0.5f, 1f),
            new Vector2(-0.5f, -1f)
        };

        private void OnEnable()
        {
            PlayerSelected += OnPlayerSelected;
            Goal           += OnGoalScored;
            Timer.TimerEnd += OnTimerEnd;
        }

        private void TimerEndPostRoutine()
        {
            EndOfFirstHalfImage.SetActive(false);
            Environment.SetActive(false);
            if (matchNo == 3)
            {
                if (_playerScore >= _computerScore)
                {
                    ShowVictory();
                }
                else
                {
                    ShowLoss();
                }
            }
            else
            {
                if (_playerScore >= _computerScore)
                {
                    ShowUpdatedBracket();
                }
                else
                {
                    ShowLoss();
                }
            }
        }

        private void OnTimerEnd()
        {
            Ball.Stop();
            EndOfFirstHalfImage.SetActive(true);
            Invoke(nameof(TimerEndPostRoutine), 2f);
        }

        private void ShowLoss()
        {
            LossPlayerImage.sprite = _selectedPlayerSprite;
            MatchUI.SetActive(false);
            Environment.SetActive(false);
            Loss.SetActive(true);
        }

        private void ShowVictory()
        {
            VictoryPlayerImage.sprite = _selectedPlayerSprite;
            MatchUI.SetActive(false);
            Environment.SetActive(false);
            Victory.SetActive(true);
        }

        private void OnGoalScored(int obj)
        {
            if (obj == 0)
            {
                _computerScore += 1;
            }
            else if (obj == 1)
            {
                _playerScore += 1;
            }

            PlayerScoreText.text   = _playerScore.ToString();
            ComputerScoreText.text = _computerScore.ToString();

            Ball.SetDirection(Vector2.zero);
            Ball.transform.position = Vector2.zero;
            Timer.PauseTimer();
            GoalImage.SetActive(true);
            Invoke(nameof(StartGameWithDelay), 2f);
        }

        private void OnPlayerSelected(Sprite obj)
        {
            _selectedPlayerSprite = obj;
        }

        public void Play()
        {
            PlayerSelect.SetActive(true);
            MainMenu.SetActive(false);
        }

        public void BackToMainMenu()
        {
            Timer.PauseTimer();
            PlayerSelect.SetActive(false);
            MainMenu.SetActive(true);
        }

        public void ShowNewBracket()
        {
            PlayerSelect.SetActive(false);
            BracketManager.ResetBracket(_selectedPlayerSprite);
            Bracket.SetActive(true);
        }

        public void BackToPlayerSelect()
        {
            PlayerSelect.SetActive(true);
            Bracket.SetActive(false);
        }

        public void ShowMatchUp()
        {
            Bracket.SetActive(false);
            int index = BracketManager.GetStartIndex(matchNo);
            PlayerMatchUpImage.sprite = BracketManager.BracketContainers[index]
                .Image.sprite;
            ComputerMatchupImage.sprite = BracketManager.BracketContainers[index + 1]
                .Image.sprite;

            MatchUp.SetActive(true);
        }

        public void BackToBracket()
        {
            Bracket.SetActive(true);
            MatchUp.SetActive(false);
        }

        public void StartMatch()
        {
            _playerScore           = _computerScore = 0;
            PlayerScoreText.text   = "0";
            ComputerScoreText.text = "0";
            int index = BracketManager.GetStartIndex(matchNo);
            PlayerSprite.sprite = BracketManager.BracketContainers[index]
                .Image.sprite;
            ComputerSprite.sprite = BracketManager.BracketContainers[index + 1]
                .Image.sprite;
            MatchUp.SetActive(false);
            MatchUI.SetActive(true);
            Environment.SetActive(true);
            Ball.SetDirection(Vector2.zero);
            Ball.transform.position = Vector2.zero;
            Timer.StartTimer(60f);
            Invoke(nameof(StartGameWithDelay), 1f);
        }

        private void StartGameWithDelay()
        {
            GoalImage.SetActive(false);
            EndOfFirstHalfImage.SetActive(false);
            Timer.ResumeTimer();
            Ball.SetDirection(_randomDirections[UnityEngine.Random.Range(0, _randomDirections.Count)]);
        }

        public void ShowUpdatedBracket()
        {
            BracketManager.UpdateBracket(matchNo, _playerScore, _computerScore);
            matchNo += 1;
            Bracket.SetActive(true);
        }

        public void HomeButton()
        {
            Ball.Stop();
            Environment.SetActive(false);
            MatchUI.SetActive(false);
            Loss.SetActive(false);
            Victory.SetActive(false);
            MainMenu.SetActive(true);
        }
    }
}
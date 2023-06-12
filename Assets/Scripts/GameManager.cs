using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public GameObject     MainMenu;
        public GameObject     PlayerSelect;
        public GameObject     Bracket;
        public GameObject     MatchUp;

        public Image          PlayerMatchUpImage, ComputerMatchupImage;
        public BracketManager BracketManager;

        private Sprite _selectedPlayerSprite;

        public static Action<Sprite> PlayerSelected;

        private int matchNo = 1;

        public static Action<int> Goal;

        public Ball Ball;
        
        private List<Vector2> _randomDirections = new()
        {
            Vector2.up,
            Vector2.down
        };


        private void OnEnable()
        {
            PlayerSelected += OnPlayerSelected;
        }

        private void Start()
        {
            StartMatch();
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
            MatchUp.SetActive(false);
            Ball.SetDirection(Vector2.zero);
            Ball.transform.position = Vector2.zero;
            Invoke(nameof(StartGameWithDelay), 1f);
        }

        private void StartGameWithDelay()
        {
            Ball.SetDirection(_randomDirections[UnityEngine.Random.Range(0, _randomDirections.Count)]);
        }

        public void ShowUpdatedBracket()
        {
            int playerGoals   = UnityEngine.Random.Range(1, 5);
            int computerGoals = UnityEngine.Random.Range(0, playerGoals);
            BracketManager.UpdateBracket(matchNo,playerGoals,computerGoals);
            matchNo += 1;
            Bracket.SetActive(true);
        }
        
    }
}
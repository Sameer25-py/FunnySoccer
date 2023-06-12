using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class BracketManager : MonoBehaviour
    {
        public                   List<BracketContainer> BracketContainers;
        public                   Transform              BracketPlayers;
        [SerializeField] private List<Sprite>           _sprites           = new();
        private                  int                    _matchCount        = 3;
        private                  int                    _participantsCount = 8;

        private Sprite _playerSprite;

        private List<Sprite> GetRandomSprites()
        {
            List<Sprite> randomSprites = new();
            randomSprites.Add(_playerSprite);
            for (int i = 0; i < _participantsCount - 1; i++)
            {
                for (int j = 0; j < _participantsCount; j++)
                {
                    Sprite randomSprite = _sprites[UnityEngine.Random.Range(0, _sprites.Count)];
                    if (!randomSprites.Contains(randomSprite))
                    {
                        randomSprites.Add(randomSprite);
                        break;
                    }
                }
            }

            return randomSprites;
        }

        private Sprite EvaluateRandomWinner(BracketContainer container1, BracketContainer container2)
        {
            int winnerGoals = UnityEngine.Random.Range(1, 5);
            int loserGoals  = UnityEngine.Random.Range(0, winnerGoals);

            container1.Text.text = winnerGoals.ToString();
            container2.Text.text = loserGoals.ToString();
            return container1.Image.sprite;
        }

        public int GetStartIndex(int matchNo)
        {
            if (matchNo == 1)
            {
                return 0;
            }
            else if (matchNo == 2)
            {
                return 8;
            }
            else if (matchNo == 3)
            {
                return 12;
            }

            return 0;
        }

        private List<Sprite> UpdateCurrentSet(int matchNo, int playerScore, int computerScore)
        {
            int          noOfParticipants = _participantsCount / (int)Mathf.Pow(2, matchNo - 1);
            List<Sprite> winners          = new();
            int          startIndex       = GetStartIndex(matchNo);

            for (int i = 0; i < noOfParticipants; i += 2)
            {   
                int participantIndex = startIndex + i;
                if (i == 0)
                {
                    //player
                    BracketContainers[participantIndex]
                        .Text.text = playerScore.ToString();
                    BracketContainers[participantIndex + 1]
                        .Text.text = computerScore.ToString();

                    winners.Add(BracketContainers[participantIndex]
                        .Image.sprite);
                }
                else
                {
                    winners.Add(
                        EvaluateRandomWinner(BracketContainers[participantIndex], BracketContainers[participantIndex + 1]));
                }
            }

            return winners;
        }

        private void SetupNextSet(List<Sprite> winners, int matchNo)
        {
            int startIndex = GetStartIndex(matchNo);
            for (int i = 0; i < winners.Count; i++)
            {
                BracketContainers[startIndex + i]
                    .Image.sprite = winners[i];
            }
        }

        public void ResetBracket(Sprite playerSprite)
        {
            _playerSprite = playerSprite;
            List<Sprite> randomSprites = GetRandomSprites();
            BracketContainers[0]
                .Image.sprite = _playerSprite;
            BracketContainers[0]
                .Text.text = "0";

            for (int i = 1; i < randomSprites.Count; i++)
            {
                BracketContainers[i]
                    .Image.sprite = randomSprites[i];
            }

            foreach (BracketContainer container in BracketContainers)
            {
                container.Text.text = "0";
            }
        }

        public void UpdateBracket(int matchNo, int playerScore, int enemyScore)
        {
            List<Sprite> winners = UpdateCurrentSet(matchNo, playerScore, enemyScore);
            if (matchNo < _matchCount)
            {
                SetupNextSet(winners, matchNo + 1);
            }
        }

        private void Start()
        {
            foreach (Transform bracketPlayer in BracketPlayers)
            {
                _sprites.Add(bracketPlayer.GetComponent<Image>()
                    .sprite);
            }
        }
    }


    [Serializable]
    public class BracketContainer
    {
        public Image    Image;
        public TMP_Text Text;
    }
}
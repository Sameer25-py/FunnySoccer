using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Goal : MonoBehaviour
    {
        public int GoalPostID = 0;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Ball"))
            {
                GameManager.Goal(GoalPostID);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
namespace Andrey04o.Chess {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TimerRepeatMove : UdonSharpBehaviour
    {
        public GameField gameField;
        public float timeToRepeat = 3f;
        float timeToRepeatCurrent = 3f;
        float timer = 0f;
        public void StartTimer() {
            gameObject.SetActive(true);
            timeToRepeatCurrent = timeToRepeat;
        }
        public void StopTimer() {
            timer = 0f;
            gameObject.SetActive(false);
        }
        public void RestartTimer() {
            timeToRepeatCurrent += 1f;
            timer = 0f;
        }
        void Update() {
            PerformTimer();
        }
        void PerformTimer() {
            timer += Time.deltaTime;
            if (timer >= timeToRepeatCurrent) {
                StopTimer();
                gameField.PerformRepeatMove();
            }
        }
    }
}
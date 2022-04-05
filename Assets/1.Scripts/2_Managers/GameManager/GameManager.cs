namespace MainSystem.Managers.GameManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class GameManager : MonoBehaviour//Data
    {
        private int gameRound = 0;
        private int whoseTurn = 0;
        private UnityEvent<int, int> turnProgressEvent = new UnityEvent<int, int>();
        private List<Character> characterList = new List<Character>();
        private Player player;
        private UnityEvent callInventory = new UnityEvent();
    }
    public partial class GameManager : MonoBehaviour//Main
    {
        private void Allocate()
        {
            
        }
        public void Initialize()
        {
            Allocate();

        }
    }
    public partial class GameManager : MonoBehaviour//Signup
    {
        public void SignupCharacter(Character character)
        {
            characterList.Add(character);
            turnProgressEvent.AddListener(character.ReceiveTureOrder);
            character.Initialize();
            character.SignupGameManager(this);
        }
        public void SignupPlayer(Player playerpra)
        {
            player = playerpra;
            callInventory.AddListener(player.Inventory);
        }
    }
    public partial class GameManager : MonoBehaviour//Prop
    {
        public void CallInventory()
        {
            callInventory.Invoke();
        }
        public void RoundProgress()
        {
            Debug.Log("RoundProgress");
            RoundCount();
            CheckTurn();
        }
        private void RoundCount()
        {
            if (whoseTurn == 0)
            {
                gameRound++;
                Debug.Log($"GameManager: Round {gameRound}.");
            }
        }
        private void CheckTurn()
        {
            Debug.Log($"GameManager:Object Number {whoseTurn} turn.");
            if (whoseTurn == 0)
            {
                turnProgressEvent.Invoke(whoseTurn, gameRound);
                whoseTurn = 1;
            }
            else if (whoseTurn == 1)
            {
                turnProgressEvent.Invoke(whoseTurn, gameRound);
                whoseTurn = 0;
            }
        }
        public void ReceiveDeadSignal()
        {
            GameEndNotify();
        }

        private void GameEndNotify()
        {
            if(whoseTurn == 1)
            {
                whoseTurn = 0;
            }
            else if(whoseTurn ==0)
            {
                whoseTurn = 1;
            }
            MainSystem.Instance.UIManager.ActiveEnding(whoseTurn);
            Debug.Log("GameManager: The End");
            Debug.Log($"GameManager: Object Number {whoseTurn} is Winer!");
        }
    }
}
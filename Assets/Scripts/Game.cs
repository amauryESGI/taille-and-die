using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Misc {
    public class Game : MonoBehaviour {
        [SerializeField]
        private List<Button> _buttonsExitGame;

        void Awake() {
            if (Application.isWebPlayer || Application.isEditor)
                _buttonsExitGame.ForEach(buttonExitGame => buttonExitGame.interactable = false);
        }

        public void OnJoinGame(string loadLevel) {
            SceneManager.LoadScene(loadLevel);
        }

        public void OnExitGame() {
            Application.Quit();
        }
    }
}

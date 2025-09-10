using System;
using System.Collections;
using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Utils.MenuManagement
{
    public class PauseMenuController : MonoBehaviour
    {
        
            
        
        private MenuPage _currentMenuPage;

        private MenuPage[] _menuPages;

        [SerializeField] private GameObject PauseMenuUi;

        [SerializeField] private SettingController SettingController;
        
        private bool isPaused = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused) Resume();
                else Pause();
            }
        }
        
        private void OnEnable()
        {
            

        }

        public void ChangeScene(string sceneName)
        {
            Resume();
            GlobalSceneManager.Instance.LoadScene(sceneName);
        }

        public void Resume()
        {
            PauseMenuUi.SetActive(false);
            Time.timeScale = 1f; // запустить игру
            isPaused = false;
            
        }
        
        public void Pause()
        {
            _menuPages = GetComponentsInChildren<MenuPage>(true);
            _currentMenuPage = _menuPages[0];
            foreach (var menuPage in _menuPages)
            {
                menuPage.SetActive(false);
            }
            _currentMenuPage.SetActive(true);
            
            PauseMenuUi.SetActive(true);
            Time.timeScale = 0f; // остановить игру
            isPaused = true;
        }
        
        public void ShowMenuPage(MenuPage menuPage)
        {
            _currentMenuPage.SetActive(false);
            menuPage.SetActive(true);
            _currentMenuPage = menuPage;
        }


        
    }
}
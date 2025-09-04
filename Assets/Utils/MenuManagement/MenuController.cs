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
    public class MenuController : MonoBehaviour
    {
        
        private const string SUPPORT_PAGE = "https://www.kickstarter.com/discover/advanced?term=novel%20visual";
            
        public Image SceneBlocker;
        
        private MenuPage _currentMenuPage;

        private MenuPage[] _menuPages;

        [SerializeField] private SettingController SettingController;
        
        private void Awake()
        {
            SettingController.LoadVolume();
            
            _menuPages = GetComponentsInChildren<MenuPage>(true);
            _currentMenuPage = _menuPages[0];
            foreach (var menuPage in _menuPages)
            {
                menuPage.SetActive(false);
            }
            _currentMenuPage.SetActive(true);
        }

        public void ChangeScene(string sceneName)
        {
            GlobalSceneManager.Instance.LoadScene(sceneName);
        }

        public void Exit()
        {
            Application.Quit();
        }
        

        public void OpenSupportPage()
        {
            Application.OpenURL(SUPPORT_PAGE);
        }
        
        public void ShowMenuPage(MenuPage menuPage)
        {
            _currentMenuPage.SetActive(false);
            menuPage.SetActive(true);
            _currentMenuPage = menuPage;
        }


        
    }
}
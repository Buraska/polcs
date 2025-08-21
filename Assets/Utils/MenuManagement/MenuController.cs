using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utils.MenuManagement
{
    public class MenuController : MonoBehaviour
    {
        
        private const string SUPPORT_PAGE = "https://www.kickstarter.com/discover/advanced?term=novel%20visual";
            
        public Image SceneBlocker;
        
        private MenuPage _currentMenuPage;

        private MenuPage[] _menuPages;

        private void Awake()
        {
            _menuPages = GetComponentsInChildren<MenuPage>(true);
            _currentMenuPage = _menuPages[0];
            foreach (var menuPage in _menuPages)
            {
                Debug.Log("1");
                menuPage.SetActive(false);
            }
            _currentMenuPage.SetActive(true);
        }

        public void Play(int sceneNum)
        {
            StartCoroutine(PlayCoroutine(sceneNum));
        }

        public void Exit()
        {
            Application.Quit();
        }
        
        private IEnumerator PlayCoroutine(int sceneNum)
        {
            SceneBlocker.enabled = true;
            yield return (CustomAnimation.FadeImage(SceneBlocker, false, 1f));
            SceneManager.LoadScene(sceneNum);
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
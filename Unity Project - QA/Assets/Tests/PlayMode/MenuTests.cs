using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Tests
{
    public class MenuTests : InputTestFixture
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("Intro");
        }

        [UnityTest]
        public IEnumerator Initialize()
        {
            var asyncLoad = SceneManager.LoadSceneAsync("Intro");
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            Assert.AreEqual("Intro", SceneManager.GetActiveScene().name);
        }


        [UnityTest]
        public IEnumerator SwitchScene()
        {
            var startButton = GameObject.Find("Start Button");
            startButton.GetComponent<Button>().onClick.Invoke();

            var asyncLoad = SceneManager.LoadSceneAsync("Game");
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
        }

        [UnityTest]
        public IEnumerator AddElement()
        {
            var startButton = GameObject.Find("Start Button");
            startButton.GetComponent<Button>().onClick.Invoke();

            var asyncLoad = SceneManager.LoadSceneAsync("Game");
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            var button = GameObject.Find("Button+100");
            button.GetComponent<Button>().onClick.Invoke();

            var buttonUnity = GameObject.Find("Unity Button(Clone)");
            Assert.IsNotNull(buttonUnity);
        }
    }
}
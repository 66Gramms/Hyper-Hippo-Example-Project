using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Clicker.Money;
using Clicker.UI;

namespace Clicker.Products
{
    public class Product : MonoBehaviour
    {
        [SerializeField] private int productIncome = 1;
        [SerializeField] private float productMultiplier = 1f;
        [Space(5)]
        [SerializeField] private float productionTime = 1f;
        [Space(10)]
        [SerializeField] private Slider productionSlider;

        private static TextOutput textOutpot;
        private Coroutine productionCoroutine;

        private void Start() {
            textOutpot = FindObjectOfType<TextOutput>();
        }

        public void GenerateIncome()
        {
            if (productionCoroutine == null)
                productionCoroutine = StartCoroutine(StartProducing());
        }

        float productionTimeProgress = 0f;
        private IEnumerator StartProducing()
        {
            while (productionTimeProgress < productionTime)
            {
                productionTimeProgress += Time.deltaTime;
                if (productionTimeProgress > productionTime)
                    productionTimeProgress = productionTime;

                productionSlider.value = productionTimeProgress / 10f; // DIVISION BY TEN IS TEMPORARY AND IS ONLY FOR TESTING, FIX THIS IMMEDIATLY!!!
                yield return new WaitForEndOfFrame();
            }

            //Reseting production for next call
            productionSlider.value = 0f;
            productionTimeProgress = 0f;
            productionCoroutine = null;

            int moneyToMake = Mathf.RoundToInt((float)productIncome * productMultiplier);
            MoneyManagger.GenerateProductIncome(moneyToMake);
            textOutpot.UpdateMoneyText();
        }
    }
}

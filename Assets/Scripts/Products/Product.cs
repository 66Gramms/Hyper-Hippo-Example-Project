using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Clicker.Money;
using Clicker.Core;

namespace Clicker.Products
{
    public class Product : MonoBehaviour
    {
        [SerializeField] private int productBaseIncome = 1;
        [SerializeField] private float productMultiplier = 1f;
        [SerializeField] private float productionTime = 1f;
        [Space(10)]
        [SerializeField] private Slider productionSlider;
        [SerializeField] private int productPrice = 2;
        [Space(10)]
        [SerializeField] private Text productAmountText;

        private Coroutine productionCoroutine;
        private int productAmount = 1;

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

                float normalizedProductionTimeProgress = ClickerMath.Map(productionTimeProgress, 0f, productionTime, 0f, 1f);
                productionSlider.value = normalizedProductionTimeProgress;
                yield return new WaitForEndOfFrame();
            }

            //Reseting production for next call
            productionSlider.value = 0f;
            productionTimeProgress = 0f;
            productionCoroutine = null;

            int moneyToMake = Mathf.RoundToInt((float)(productBaseIncome) * (float)(productAmount) * productMultiplier);
            Debug.Log(moneyToMake);
            MoneyManagger.GenerateProductIncome(moneyToMake);
        }

        public void IncreaseAmountOfProduct(int amount)
        {
            int cost = productPrice * amount;
            bool enoughMoney = MoneyManagger.totalMoney >= cost;
            if (!enoughMoney)    return;

            MoneyManagger.SubtractFromMoney(productPrice * amount);
            productAmount += amount;
            productAmountText.text = productAmount.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
        [SerializeField] private BigInteger productPrice = 2;
        [Space(10)]
        [SerializeField] private Text productAmountText;
        [SerializeField] private Text productPurchaseAmountText;
        [SerializeField] private Text timeLeftText;

        private static List<Product> allProducts = new List<Product>();
        private static BigInteger purchaseAmount = 1;

        private Coroutine productionCoroutine;
        private BigInteger productAmount = 1;
        private float productionTimeProgress = 0f;

        private void Start()
        {
            allProducts.Add(this);
        }

        //Called from UI button
        public void GenerateIncome()
        {
            if (productionCoroutine == null)
                productionCoroutine = StartCoroutine(produce());
        }

        private IEnumerator produce()
        {
            while (productionTimeProgress < productionTime)
            {
                productionTimeProgress += Time.deltaTime;
                if (productionTimeProgress > productionTime)
                    productionTimeProgress = productionTime;

                float normalizedProductionTimeProgress = ClickerMath.Map(productionTimeProgress, 0f, productionTime, 0f, 1f);
                productionSlider.value = normalizedProductionTimeProgress;

				float timeLeft = productionTime - productionTimeProgress;
				timeLeftText.text = timeLeft.ToString("F2") + " seconds";
                if (timeLeft <= Mathf.Epsilon)
                timeLeftText.text = "";

                yield return new WaitForEndOfFrame();
            }

            //Reseting production for next call
            productionSlider.value = 0f;
            productionTimeProgress = 0f;
            productionCoroutine = null;

            int moneyToMake = Mathf.RoundToInt((float)(productBaseIncome) * (float)(productAmount) * productMultiplier);
            MoneyManagger.GenerateProductIncome(moneyToMake);
        }

        public void IncreaseAmountOfProduct()
        {
            BigInteger cost = productPrice * purchaseAmount;
            bool enoughMoney = MoneyManagger.totalMoney >= cost;
			BigInteger purchasableAmount = MoneyManagger.totalMoney / productPrice;

            if (!enoughMoney)
            {
                MoneyManagger.SubtractFromMoney(purchasableAmount);
                productAmount += purchasableAmount;
            } else {
                MoneyManagger.SubtractFromMoney(productPrice * purchaseAmount);
                productAmount += purchaseAmount;
			}

            productAmountText.text = productAmount.ToString();
        }

        public static void UpdatePurchaseAmountText(int amount)
		{
			Product.purchaseAmount = amount;
			foreach (Product product in allProducts)
			{
				BigInteger purchasableAmount = MoneyManagger.totalMoney / (BigInteger)product.productPrice;
				string text;

				if (purchasableAmount < amount)
					text = $"Buy {purchasableAmount.ToString()} {product.transform.parent.name} for {product.productPrice * purchasableAmount}$";
				else
					text = $"Buy {amount.ToString()} {product.transform.parent.name} for {product.productPrice * amount}$";

				product.productPurchaseAmountText.text = text;
				purchaseAmount = amount;
			}
		}

		public static void UpdatePurchaseAmountText()
		{
			foreach (Product product in allProducts)
			{
				BigInteger purchasableAmount = MoneyManagger.totalMoney / (BigInteger)product.productPrice;
				string text;

				if (purchasableAmount < purchaseAmount)
					text = $"Buy {purchasableAmount.ToString()} {product.transform.parent.name} for {product.productPrice * purchasableAmount}$";
				else
					text = $"Buy {purchaseAmount.ToString()} {product.transform.parent.name} for {product.productPrice * purchaseAmount}$";

				product.productPurchaseAmountText.text = text;
			}
		}
    }
}

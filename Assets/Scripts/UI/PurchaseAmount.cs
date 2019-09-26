using UnityEngine;
using UnityEngine.UI;

using Clicker.Products;

namespace Clicker.UI
{
    public class PurchaseAmount : MonoBehaviour
    {
        public static int currentAmount = 1;

        [SerializeField] Text PurchaseAmountText;
        [SerializeField] short[] purchaseAmounts = new short[5] {1, 10, 50, 100, 1000};

        private int index = 0;

        //Called from UI button
        public void UpdatePurchaseText()
        {
            index++;
            if (index >= purchaseAmounts.Length)
                index = 0;
            currentAmount = purchaseAmounts[index];
            PurchaseAmountText.text = purchaseAmounts[index].ToString() + "x";   
            Product.UpdatePurchaseAmountText(purchaseAmounts[index]);
        }
    }
}

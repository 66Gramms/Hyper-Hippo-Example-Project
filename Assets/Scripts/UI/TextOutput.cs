using UnityEngine;
using UnityEngine.UI;

using Clicker.Money;

namespace Clicker.UI
{
    public class TextOutput : MonoBehaviour
    {
        [SerializeField] Text moneyText;

        private void Update() {
            UpdateMoneyText();
        }

        public void UpdateMoneyText()
        {
            moneyText.text = MoneyManagger.totalMoney.ToString();
        }
    }    
}

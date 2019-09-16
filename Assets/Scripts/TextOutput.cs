using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Clicker.Core;

namespace Clicker.UI
{
    public class TextOutput : MonoBehaviour
    {
        [SerializeField] Text moneyText;
        MoneyManagger moneyManagger;

        private void Start() {
            CacheComponents();
        }

        private void CacheComponents()
        {
            moneyManagger = GetComponent<MoneyManagger>();
        }

        public void UpdateMoneyText()
        {
            moneyText.text = moneyManagger.totalMoney.ToString();
        }
    }    
}

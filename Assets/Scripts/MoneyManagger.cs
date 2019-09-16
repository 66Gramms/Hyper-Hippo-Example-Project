using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Clicker.Core
{
    public class MoneyManagger : MonoBehaviour
    {
        public BigInteger totalMoney { get; private set; } = 0;
        public float moneyMultiplier { get; private set; } = 1f;

        public void AddToMoney(int amount)
        {
            totalMoney += amount;
        }

        /// <summary>
        /// Returns TRUE and subtracts money if it is possible, returns FALSE if it's not.
        /// </summary>
        public bool SubtractFromMoney(int amount)
        {
            if (amount > totalMoney)    return false;
            else {
                totalMoney -= amount;
                return true;
            }

        }

        public void AddToMoneyMultiplier(float amount)
        {
            moneyMultiplier += amount;
        }

        /// <summary>
        /// Returns TRUE and subtracts multiplier if it is possible, returns FALSE if it's not.
        /// </summary>
        public bool SubtractFromMoneyMultiplier(float amount)
        {
            if (amount > moneyMultiplier)   return false;
            else {
                moneyMultiplier -= amount;
                return true;
            }
        }
    }    
}

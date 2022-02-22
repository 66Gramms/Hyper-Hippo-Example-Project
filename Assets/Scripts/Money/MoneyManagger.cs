using System.Numerics;
using UnityEngine;

namespace Clicker.Money
{
    public class MoneyManagger : MonoBehaviour
    {
        public static BigInteger totalMoney { get; private set; } = 0;
        public static float globalMoneyMultiplier { get; private set; } = 1f;
        public static int totalPassiveIncome = 1;  //Amount of money gained each second
        
        public static void GenerateProductIncome(int amount)
        {
            totalMoney += (BigInteger)((float)amount * globalMoneyMultiplier);
        }

        /// <summary>
        /// Returns TRUE and subtracts money if it is possible, returns FALSE if it's not.
        /// </summary>
        public static bool SubtractFromMoney(int amount)
        {
            if (amount > totalMoney)    return false;
            else {
                totalMoney -= amount;
                return true;
            }
        }

        /// <summary>
        /// Returns TRUE and subtracts money if it is possible, returns FALSE if it's not.
        /// </summary>
        public static bool SubtractFromMoney(BigInteger amount)
        {
            if (amount > totalMoney)
            {
                return false;
            }
            totalMoney -= amount;
            return true;
        }

        public static void AddToMoneyMultiplier(float amount)
        {
            globalMoneyMultiplier += amount;
        }

        /// <summary>
        /// Returns TRUE and subtracts multiplier if it is possible, returns FALSE if it's not.
        /// </summary>
        public static bool SubtractFromMoneyMultiplier(float amount)
        {
            if (amount > globalMoneyMultiplier)   return false;
            else {
                globalMoneyMultiplier -= amount;
                return true;
            }
        }
    }    
}

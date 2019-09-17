namespace Clicker.Core
{
    public static class ClickerMath {
        public static float Map (float rawValue, float sourceMin, float sourceMax, float targetMin, float targetMax)
        {
            float normalizedValue = (rawValue - sourceMin) * (targetMax - targetMin) / (sourceMax - sourceMin) + targetMin;
            return normalizedValue;
        }
    }
}
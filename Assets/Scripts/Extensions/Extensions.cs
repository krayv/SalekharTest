using System.Collections.Generic;
using UnityEngine;

namespace Scorewarrior.Test
{
    public static class Extensions
    {
        public static List<T> GetRandomElements<T>(this List<T> values, int range)
        {
            if (range > values.Count)
            {
                return values;
            }
            List<T> newValues = new List<T>();
            List<T> availableValue = new List<T>(values);
            for (int i = 0; i < range; i++)
            {
                int index = Random.Range(0, availableValue.Count);
                newValues.Add(availableValue[index]);
                availableValue.RemoveAt(index);
            }
            return newValues;
        }
    }
}

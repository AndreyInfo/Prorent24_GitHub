using Prorent24.Entity.Configuration.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Extentions
{
    public static class MathExtention
    {
        /// <summary>
        /// VAT Calculation
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal VATCalculation(this decimal? rate, decimal price)
        {
            if (rate.HasValue && price > 0)
            {
                decimal taxСalculation = (price * rate.Value) / 100;
                decimal calculation = price + taxСalculation;
                return calculation;
            }
            else
            {
                return price;
            }
        }

        /// <summary>
        /// Total price Calculation
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal TotalPriceCalculation(this decimal? price, decimal discount, decimal coefficient, int quantity)
        {
            decimal totalPrice = 0;
            if (price.HasValue)
            {
                totalPrice = (price.Value * quantity) * coefficient;

                if (discount > 0)
                {
                    totalPrice = totalPrice - ((totalPrice * discount) / 100);
                }
            }

            return totalPrice;
        }

        /// <summary>
        /// Total price Calculation
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal TotalPriceCalculation(this decimal price, decimal discount, decimal coefficient, int quantity)
        {
            if (coefficient <= 0)
            {
                coefficient = 1;
            }
            decimal totalPrice = (price * quantity) * coefficient;

            if (discount > 0)
            {
                totalPrice = totalPrice - ((totalPrice * discount) / 100);
            }

            return totalPrice;
        }

        /// <summary>
        /// Factor Calculation
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal FactorCalculation(this List<FactorGroupOptionEntity> factors, DateTime? from, DateTime? to)
        {
            int days = (to.Value - from.Value).Days;

            FactorGroupOptionEntity optionEntity = factors.FirstOrDefault(x => x.NumberOfDaysFrom >= days && x.NumberOfDaysTo <= days);

            if (optionEntity != null && optionEntity.Id > 0)
            {
                return optionEntity.Factor;
            }
            else
            {
                return 1;
            }
        }
    }
}

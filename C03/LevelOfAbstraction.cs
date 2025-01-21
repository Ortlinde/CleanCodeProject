namespace CleanCodeProject.C03
{
    public class LevelOfAbstraction
    {
        public int CalculateTotalCartPrice(CartItem[] cartItems)
        {
            int totalPrice = 0;

            foreach (CartItem cartItem in cartItems)
            {
                totalPrice += cartItem.Price;

                if (cartItem.IsTaxable())
                {
                    totalPrice += cartItem.Price * 0.18;
                }

                if (cartItem.Category.Equals("Electronics"))
                {
                    if (cartItem.Price > 500)
                    {
                        totalPrice -= 50;
                    }
                }
            }

            return totalPrice;
        }
    }

    public class LevelOfAbstraction1
    {
        public int CalculateTotalCartPrice(CartItem[] cartItems)
        {
            int totalPrice = 0;

            foreach (CartItem item in cartItems)
            {
                totalPrice += CalculateItemTotal(item);
            }

            return totalPrice;
        }

        private int CalculateItemTotal(CartItem item)
        {
            int totalPrice = item.Price;

            if (item.IsTaxable())
            {
                totalPrice += item.Price * 0.18;
            }

            if (item.Category.Equals("Electronics"))
            {
                if (item.Price > 500)
                {
                    totalPrice -= 50;
                }
            }

            return totalPrice;
        }
    }

    public class LevelOfAbstraction2
    {
        const int ELECTRONICS_DISCOUNT = 50;
        const int ELECTRONICS_DISCOUNT_THRESHOLD = 500;
        const double TAX_RATE = 0.18f;

        public int CalculateTotalCartPrice(CartItem[] cartItems)
        {
            int totalPrice = 0;

            foreach (CartItem item in cartItems)
            {
                totalPrice += CalculateItemTotal(item);
            }

            return totalPrice;
        }

        private int CalculateItemTotal(CartItem item)
        {
            int itemTotal = item.Price;
            itemTotal += CalculateTax(item);
            itemTotal -= CalculateDiscount(item);

            // very long code...

            return itemTotal;
        }

        private int CalculateTax(CartItem item)
        {
            if (item.IsTaxable())
            {
                return (int)(item.Price * TAX_RATE);
            }
            return 0;
        }

        private int CalculateDiscount(CartItem item)
        {
            if (IsEligibleForDiscount(item))
            {
                return ELECTRONICS_DISCOUNT;
            }
            return 0;
        }

        private bool IsEligibleForDiscount(CartItem item)
        {
            return item.Category.Equals("Electronics") && item.Price > ELECTRONICS_DISCOUNT_THRESHOLD;
        }
    }
}
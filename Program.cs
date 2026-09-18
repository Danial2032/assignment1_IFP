using System;

namespace FunctionalDeliveryCalculator
{
    public enum DeliveryType
    {
        Pickup,
        Courier,
        DoorToDoor
    }

    public enum DeliveryZone
    {
        City,
        OutsideCity,
        Remote
    }

    public record DeliveryInput
    (
        decimal BasePrice,
        int ItemCount,
        bool IsExpress,
        DeliveryType Type,
        DeliveryZone Zone
    );

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Delivery Cost Calculator ===");

            if(!TryReadInput(out DeliveryInput? input))
            {
                Console.WriteLine($"\n[Error] Calculation aborted due to invalid input.");
                return;            
            }

            decimal finalPrice = CalculateTotalCost(input!);

            Console.WriteLine($"\nFinal Delivery Price: {finalPrice:F2}");
        }

        public static bool TryReadInput(out DeliveryInput? input)
        {
            input = null;

            Console.Write("Enter base delivery price: ");
            string? basePriceInput= Console.ReadLine();
            if(!decimal.TryParse(basePriceInput, out decimal basePrice) || basePrice < 0)
            {
                Console.WriteLine("Error: Base price must be a valid non-negative number.");
                return false;
            }

            Console.Write("Enter number of items: ");
            string? itemCountInput = Console.ReadLine();
            if (!int.TryParse(itemCountInput, out int itemCount) || itemCount <= 0)
            {
                Console.WriteLine("Error: Number of items must be a positive integer.");
                return false;
            }

            Console.Write("Is this express delivery? (true/false): ");
            string? expressInput = Console.ReadLine();
            if (!bool.TryParse(expressInput, out bool isExpress))
            {
                Console.WriteLine("Error: Express delivery status must be 'true' or 'false'.");
                return false;
            }

            Console.Write("Enter delivery type (Pickup, Courier, DoorToDoor): ");
            string? typeInput = Console.ReadLine();
            if(!Enum.TryParse<DeliveryType>(typeInput, true, out var deliveryType) || !Enum.IsDefined(typeof(DeliveryType),deliveryType))
            {
                Console.WriteLine("Error: Invalid delivery type specified.");
                return false;
            }

            Console.Write("Enter delivery zone (City, OutsideCity, Remote): ");
            string? zoneInput = Console.ReadLine();
            if(!Enum.TryParse<DeliveryZone>(zoneInput, true, out var deliveryZone) || !Enum.IsDefined(typeof(DeliveryZone), deliveryZone))
            {
                Console.WriteLine("Error: Invalid delivery zone specified.");
                return false;
            }

            input = new DeliveryInput(basePrice, itemCount, isExpress, deliveryType, deliveryZone);
            return true;
        }

        public static decimal ApplyRule(decimal price, Func<decimal, decimal> rule) => rule(price);

        public static decimal CalculateTotalCost(DeliveryInput input)
        {
            decimal price = input.BasePrice;

            price = ApplyRule(price, p => ApplyItemCountRule(p, input.ItemCount));

            price = ApplyRule(price, p => ApplyDeliveryTypeRule(p,input.Type));

            price= ApplyRule(price, p => ApplyDeliveryZoneRule(p, input.Zone));

            price = ApplyRule(price, p => ApplyExpressRule(p, input.IsExpress));

            return Math.Round(price, 2, MidpointRounding.AwayFromZero);
        }

        public static decimal ApplyExpressRule(decimal price, bool isExpress) => isExpress ? price * 1.30m : price;

        public static decimal ApplyItemCountRule(decimal price, int count) => count switch
        {
            >= 1 and <= 3 => price,
            >= 4 and <= 7 => price * 1.10m,
            >= 8 => price * 1.20m,
            _ => price
        };

        public static readonly Func<decimal, DeliveryType, decimal> ApplyDeliveryTypeRule = (price,type) => type switch
        {
            DeliveryType.Pickup => price * 0.80m,
            DeliveryType.Courier => price,
            DeliveryType.DoorToDoor => price * 1.15m,
            _ => price
        };

        public static readonly Func<decimal, DeliveryZone, decimal> ApplyDeliveryZoneRule = (price, zone) => zone switch
        {
            DeliveryZone.City => price,
            DeliveryZone.OutsideCity => price * 1.25m,
            DeliveryZone.Remote => price,
            _ => price
        };
    }
}
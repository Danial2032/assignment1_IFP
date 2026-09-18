# assignment1_IFP

# Alibayev Danial, IT-2504

# README questions:

1) Which parts of your program handle user input and output?

Answer: I used Console.Write() and Console.WriteLine() to display messages to the user (output) and I used Console.ReadLine() to get input from the user. Main part is used to show final price and name of the application, while TryReadInput is used to collect user input and to calculate final price.

2) Which functions perform only delivery price calculations?

Answer: CalculateTotalCost is used to calculate price by changing the price if there is some condition. ApplyRule is used to change the price by condition (this function is used inside of CalculateTotalCost function). ApplyItemCountRule changes price based on item amount. ApplyExpressRule adds 30% of the price for express delivery. ApplyDeliveryTypeRule changes price if Courier then nothing changes, if DoorToDoor then adds 15%, if Pickup then reduces the price by 20%. ApplyDeliveryZoneRule adds 25% to the price if OutsideCity, and does not change the price if City delivery.

3) How is Func<> used to apply delivery pricing rules?

Answer: There are 2 forms in the application. Firstly, ApplyDeliveryZoneRule and ApplyDeliveryTypeRule are defined directly as Func<decimal,DeliveryType,decimal> and Func<decimal,DeliveryZone, decimal> which are more varibales than methods. Secondly, ApplyRule method expects a parameter of type Func<decimal,decimal>. During calculation, lambda expressions wrap each specific pricing rule to match this signature (for example p => ApplyDeliveryTypeRule(p, input.Type)), allowing pricing rules to be passed dynamically as executable behavior.

4) Why is TryParse useful when processing delivery data entered by the user?

Answer: TryParse helps to show message if user input is wrong. Standard parsing methods throw runtime exceptions when given text, empty string or null values. TryParse returns a bool indicating success or failure. It stops execution if the user input is wrong.

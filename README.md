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

# Test Cases (6): 

1) Price did not change

<img width="564" height="197" alt="1st i" src="https://github.com/user-attachments/assets/77e9670d-d0cf-485c-9e7a-79a843afc9be" />

I used input such as 3, false, Courier, City. These input does not change the price.

2) Added 1000 or 20% of the price because of item count.

<img width="544" height="189" alt="2nd i" src="https://github.com/user-attachments/assets/3c033c67-09fe-496e-8250-2236050595e1" />

I used input such as 10, false, Courier, City. Only 10 items changed the price by adding 20%. 

3) Added 1500 or 30% of the price.

<img width="535" height="198" alt="3rd i" src="https://github.com/user-attachments/assets/8bb25c7c-e8c0-4603-93af-415dab45295e" />

I used input such as 3, true, Courier, City. Only true changed the price by adding 30%.

4) Reduced the price by 20% or 1000.

<img width="516" height="195" alt="4th i" src="https://github.com/user-attachments/assets/320cb80a-854d-4751-8c15-b18b857c260f" />

I used input such as 3, false, Pickup, City. Only Pickup reduced the price by 20%.

5) The program shows Error  because of the text instead of integer input

<img width="500" height="133" alt="5th i" src="https://github.com/user-attachments/assets/72691c9d-9a95-4e13-8c31-2d6a38d2937f" />

I used string which cause TryParse to show the error message.

6) The program shows Error because of the empty value

<img width="475" height="145" alt="7i" src="https://github.com/user-attachments/assets/b03a0fa7-ce14-4df9-ae58-93483aa6d03c" />

I used empty value, which triggered the TryParse that shows error.

# To Run the program: 

I use terminal in which I write dotnet run and press Enter.

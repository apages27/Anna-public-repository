using System;
using FlooringProgram.Models;
using FlooringProgram.Operations;

namespace FlooringProgram.UI
{
    public class UserInteractions
    {
        private bool _keepRunning = true;

        public void Run()
        {
            while (_keepRunning)
            {
                MenuDisplay();
            }

        }

        public void MenuDisplay()
        {
            Console.Clear();

            Console.WriteLine("********************************************************************************");
            Console.WriteLine("*                         Flooring Program                                     *");
            Console.WriteLine("* 1. Display Orders                                                            *");
            Console.WriteLine("* 2. Add an Order                                                              *");
            Console.WriteLine("* 3. Edit an Order                                                             *");
            Console.WriteLine("* 4. Remove an Order                                                           *");
            Console.WriteLine("* 5. Quit                                                                      *");
            Console.WriteLine("*                                                                              *");
            Console.WriteLine("********************************************************************************");

            Prompt();
        }

        public void Prompt()
        {
            int choice;
            bool validChoice = false;
            while (!validChoice)
            {
                Console.Write("\nEnter choice: ");
                validChoice = int.TryParse(Console.ReadLine(), out choice);

                if (validChoice)
                {
                    if (choice >= 1 && choice <= 5)
                    {
                        switch (choice)
                        {
                            case 1:
                                DisplayOrders();
                                break;
                            case 2:
                                AddOrder();
                                break;
                            case 3:
                                EditOrder();
                                break;
                            case 4:
                                DeleteOrder();
                                break;
                            case 5:
                                Quit();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Enter a choice between 1 and 5");
                        validChoice = false;
                    }
                }
            }
        }

        public void DisplayOrders()
        {
            Console.Clear();
            Console.WriteLine("Display Orders");
            Console.WriteLine("***************************\n");

            bool validDate = false;
            DateTime orderDate = new DateTime();

            while (!validDate)
            {
                Console.Write("Enter a date (MM/DD/YYYY): ");
                validDate = DateTime.TryParse(Console.ReadLine(), out orderDate);
                if (!validDate)
                    Console.WriteLine("\nInvalid Date");
            }

            // Give operations class the orderDate and let it return items
            OrderOperations orderOperations = new OrderOperations();
            OrdersResult result = orderOperations.RetrieveOrdersFor(orderDate);

            if (result.Success)
            {
                foreach (var order in result.Orders)
                {
                    Console.WriteLine("\n{0}  {1}  {2}  {3}  {4}  {5}  {6:c}  {7:c}  {8:c}  {9:c}  {10:c}  {11:c}", order.OrderNumber, order.CustomerName, order.State, order.TaxRate, order.ProductType, order.Area, order.CostPerSquareFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
                }
            }
            else
            {
                Console.WriteLine("\nUnable to display orders: {0}", result.Message);
            }

            Console.WriteLine("\nPress enter to continue");
            Console.ReadLine();
        }

        public void AddOrder()
        {
            OrderOperations orderOps = new OrderOperations();

            CustomerInput custIn = new CustomerInput();
            bool isValidName = false;

            Console.Clear();
            Console.WriteLine("Add Order");
            Console.WriteLine("***************************\n");

            while (!isValidName)
            {
                Console.Write("Please enter the customer name: ");
                custIn.CustName = Console.ReadLine();
                isValidName = !string.IsNullOrEmpty(custIn.CustName);
            }

            bool isValidState = false;

            while (!isValidState)
            {
                Console.Write("\nPlease enter a state: ");
                custIn.State = Console.ReadLine().ToUpper();

                TaxRateOperations taxOps = new TaxRateOperations();
                if (taxOps.IsAllowedState(custIn.State))
                {
                    Console.WriteLine("\nThat is a valid state");
                    TaxRate rate = taxOps.GetTaxRateFor(custIn.State);
                    isValidState = true;
                    Console.WriteLine("\nThe tax rate for {0} is {1:p}", rate.State, rate.TaxPercent);
                }
                else
                {
                    Console.WriteLine("\nThat is not a valid state");
                }
            }

            bool isValidProduct = false;

            while (!isValidProduct)
            {
                Console.Write("\nPlease enter a product type: ");
                custIn.ProductType = Console.ReadLine();

                ProductOperations prodOps = new ProductOperations();
                if (prodOps.IsExistingProduct(custIn.ProductType))
                {
                    Console.WriteLine("\nThat is a valid product.");
                    Product prod = prodOps.GetProductFor(custIn.ProductType);
                    isValidProduct = true;
                }
                else
                {
                    Console.WriteLine("\nThat is not one of our products.");
                }
            }

            bool isValidArea = false;

            while (!isValidArea)
            {
                Console.Write("\nPlease enter flooring area in square feet: ");
                decimal floorArea;
                bool isDecimal = decimal.TryParse(Console.ReadLine(), out floorArea);
                if (isDecimal)
                {
                    isValidArea = true;
                    custIn.Area = floorArea;
                }
            }

            Console.Write("\nReady to create new order? Y/N ");

            if (Console.ReadLine().ToUpper() == "N")
            {
                MenuDisplay();
            }
            custIn.OrderNumber = 0;
            DateTime orderDate = default(DateTime);
            orderOps.CreateNewOrder(custIn, orderDate);
            Console.WriteLine("\nOk, your order has been created!");
            Console.ReadLine();
        }

        public void EditOrder()
        {
            Console.Clear();
            Console.WriteLine("Edit Order");
            Console.WriteLine("***************************\n");

            bool validDate = false;
  
            DateTime orderDate = new DateTime();
            int orderNumber = 0;

            while (!validDate)
            {
                Console.Write("Enter a date (MM/DD/YYYY): ");
                validDate = DateTime.TryParse(Console.ReadLine(), out orderDate);
                if (!validDate)
                    Console.WriteLine("\nInvalid Date");
            }

            OrderOperations orderOperations = new OrderOperations();
            OrdersResult result = orderOperations.RetrieveOrdersFor(orderDate);

            bool validEntry = false;

            if (result.Success)
            {
                while (!validEntry)
                {
                    bool validNumber = false;
                    while (!validNumber)
                    {
                        Console.Write("\nPlease enter an order number: ");
                        validNumber = int.TryParse(Console.ReadLine(), out orderNumber);
                        if (!validNumber)
                        {
                            Console.WriteLine("\nInvalid order number");
                        }
                        else
                        {
                            bool reply = orderOperations.CheckIfExistingOrder(orderNumber, orderDate);
                            if (reply)
                            {
                                validEntry = true;
                            }
                            else
                            {
                                Console.WriteLine("\nThere is no order for that number on that date!");
                            }
                        }
                    }
                }
                Order fullOrder = orderOperations.RetrieveOrderByNumber(orderNumber, orderDate);

                TaxRateOperations taxOps = new TaxRateOperations();
                ProductOperations prodOps = new ProductOperations();

                CustomerInput editedOrder = new CustomerInput()
                {
                    CustName = fullOrder.CustomerName,
                    Area = fullOrder.Area,
                    ProductType = fullOrder.ProductType,
                    State = fullOrder.State,
                    OrderNumber = fullOrder.OrderNumber
                };

                Console.WriteLine("\n{0} {1} {2} {3}", editedOrder.CustName, editedOrder.State, editedOrder.ProductType, editedOrder.Area);

                bool validName = false;
                string input;

                while (!validName)
                {
                    Console.Write("\nPlease enter new Customer Name, or press enter key to skip: ");
                    input = Console.ReadLine();
                    if (input != "")
                    {
                        editedOrder.CustName = input;
                        validName = true;
                    }
                    else
                    {
                        validName = true;
                    }
                }

                bool validState = false;

                while (!validState)
                {
                    Console.Write("\nPlease enter new State, or press enter key to skip: ");
                    input = Console.ReadLine().ToUpper();
                    if (input != "")
                    {
                        if (taxOps.IsAllowedState(input))
                        {
                            editedOrder.State = input;
                            validState = true;
                        }
                        else
                            Console.Write("\nThat is not a valid state");
                    }
                    else
                    {
                        validState = true;
                    }
                }

                bool validProduct = false;

                while (!validProduct)
                {
                    Console.Write("\nPlease enter new Product Type, or press enter key to skip: ");
                    input = Console.ReadLine().ToUpper();
                    if (input != "")
                    {
                        if (prodOps.IsExistingProduct(input))
                        {
                            editedOrder.ProductType = input;
                            validProduct = true;
                        }
                        else
                            Console.Write("\nThat is not one of our products");
                    }
                    else
                    {
                        validProduct = true;
                    }
                }

                bool validArea = false;

                while (!validArea)
                {
                    Console.Write("\nPlease enter new Area in square feet, or press enter key to skip: ");
                    input = Console.ReadLine();
                    if (input != "")
                    {
                        decimal userChoice;
                        bool isDecimal = decimal.TryParse(input, out userChoice);

                        if (isDecimal)
                        {
                            editedOrder.Area = userChoice;
                            validArea = true;
                        }
                        else
                            Console.WriteLine("\nThat is not a valid number");
                    }
                    else
                    {
                        validArea = true;
                    }
                }
                Console.Write("\nAre you ready to save edited order details? Y/N ");

                if (Console.ReadLine().ToUpper() == "N")
                {
                    MenuDisplay();
                }
                else
                {
                    orderOperations.CreateNewOrder(editedOrder, orderDate);

                    Order newEditedOrder = orderOperations.RetrieveOrderByNumber(orderNumber, orderDate);

                    Console.Clear();
                    Console.Write("\nYour order has been edited:\n");
                    Console.WriteLine("\n{0}  {1}  {2}  {3}  {4}  {5}  {6:c}  {7:c}  {8:c}  {9:c}  {10:c}  {11:c}", newEditedOrder.OrderNumber, newEditedOrder.CustomerName, newEditedOrder.State, newEditedOrder.TaxRate, newEditedOrder.ProductType, newEditedOrder.Area, newEditedOrder.CostPerSquareFoot, newEditedOrder.LaborCostPerSquareFoot, newEditedOrder.MaterialCost, newEditedOrder.LaborCost, newEditedOrder.Tax, newEditedOrder.Total);
                    Console.Write("\nPress Enter to return to the Main Menu");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("\nUnable to display orders: {0}", result.Message);
            }
        }

        public void DeleteOrder()
        {
            Console.Clear();
            Console.WriteLine("Delete Order");
            Console.WriteLine("***************************\n");

            bool validDate = false;

            DateTime orderDate = new DateTime();
            int orderNumber = 0;

            while (!validDate)
            {
                Console.Write("Enter a date (MM/DD/YYYY): ");
                validDate = DateTime.TryParse(Console.ReadLine(), out orderDate);
                if (!validDate)
                    Console.WriteLine("\nInvalid Date");
            }

            OrderOperations orderOperations = new OrderOperations();
            OrdersResult result = orderOperations.RetrieveOrdersFor(orderDate);

            bool validEntry = false;

            if (result.Success)
            {
                while (!validEntry)
                {
                    bool validNumber = false;
                    while (!validNumber)
                    {
                        Console.Write("\nPlease enter an order number: ");
                        validNumber = int.TryParse(Console.ReadLine(), out orderNumber);
                        if (!validNumber)
                        {
                            Console.WriteLine("\nInvalid order number");
                        }
                        else
                        {
                            bool reply = orderOperations.CheckIfExistingOrder(orderNumber, orderDate);
                            if (reply)
                            {
                                validEntry = true;
                            }
                            else
                            {
                                Console.WriteLine("\nThere is no order for that number on that date!");
                            }
                        }
                    }
                }
                Order fullOrder = orderOperations.RetrieveOrderByNumber(orderNumber, orderDate);

                Console.WriteLine("\n{0}  {1}  {2}  {3}  {4}  {5}  {6:c}  {7:c}  {8:c}  {9:c}  {10:c}  {11:c}", fullOrder.OrderNumber, fullOrder.CustomerName, fullOrder.State, fullOrder.TaxRate, fullOrder.ProductType, fullOrder.Area, fullOrder.CostPerSquareFoot, fullOrder.LaborCostPerSquareFoot, fullOrder.MaterialCost, fullOrder.LaborCost, fullOrder.Tax, fullOrder.Total);

                Console.Write("\nAre you sure you want to delete this file? Y/N ");

                if (Console.ReadLine().ToUpper() == "Y")
                {
                    if (orderOperations.CallDelete(fullOrder, orderDate))
                    {
                        Console.WriteLine("\nThe order has been deleted.");
                        Console.WriteLine("\nPress Enter to return to Main Menu");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("\nFor some reason the order failed to delete...");
                        Console.WriteLine("\nPress Enter to return to Main Menu");
                        Console.ReadLine();
                    }
                }
                else MenuDisplay();
            }
            else
            {
                Console.WriteLine("\nUnable to display orders: {0}", result.Message);
            }
        }

        public void Quit()
        {
            _keepRunning = false;
        }

    }
}

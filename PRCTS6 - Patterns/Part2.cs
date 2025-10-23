using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Part2
    {
        public static void Run()
        {
            CoffeMachine machine = new CoffeMachine();
            machine.InsertCoins();
            machine.SelectDrink();
            machine.CheckIfReadyAndTake();
        }
    }
    public class CoffeMachine
    {
        private AwaitingPaymentState awaitingPaymentClass = new();
        private AwaitingDrinkSelectionState selectionClass = new();
        private MakingDrinkState makingDrink = new();
        private DrinkReadyState dispensedDrinkState = new();
        public CoffeMachine()
        {
            awaitingPaymentClass.Activate();
        }
        public void InsertCoins()
        {
            selectionClass.Activate();
        }
        public void SelectDrink()
        {
            makingDrink.Activate();
        }
        public void CheckIfReadyAndTake()
        {
            dispensedDrinkState.Activate();
            awaitingPaymentClass.Activate();
        }
    }
    public interface ICoffeeMachineState
    {
        public void Activate();
    }
    public class AwaitingPaymentState : ICoffeeMachineState
    {
        public void Activate()
        {
            Console.WriteLine("Внесите монету.");
        }
    }
    public class AwaitingDrinkSelectionState : ICoffeeMachineState
    {
        public void Activate()
        {
            Console.WriteLine("Выберете свой напиток.");
        }
    }
    public class MakingDrinkState : ICoffeeMachineState
    {
        public void Activate()
        {
            Console.WriteLine("Делаем напиток.");
        }
    }
    public class DrinkReadyState : ICoffeeMachineState
    {
        public void Activate()
        {
            Console.WriteLine("Напиток готов.");
        }
    }
}

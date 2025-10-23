using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Part5
    {
        public static Task Run()
        {
            IService service = new RealServiceProvider();
            string message = "1234567890";
            Task result1 = service.GetData(message);
            result1.Wait();
            Task result2 = ProxyService.ProxyAccess(service, message);
            result2.Wait();
            return Task.CompletedTask;
        }
    }
    public interface IService
    {
        public Task GetData(string message);
    }
    public class RealServiceProvider : IService
    {
        public Task GetData(string message)
        {
            int messageping = message.Length * message.Length * 30;
            Task.Delay(messageping).Wait();
            Console.WriteLine("Recieved "+message+" With ping of "+ messageping+ "milisecond");
            return Task.FromResult(0);
        }
    }
    public class ProxyService
    {
        public static Task ProxyAccess(IService service, string message)
        {
            if (service == null || message == null) return Task.FromResult(0);
            
            List<string> tosend = new();
            int lenght = message.Length;

            for (int i = 0; i<lenght; i += 2)
            {
                int clampedI = Math.Clamp(i, 0, lenght);
                string split = message.Substring(clampedI, 2);
                tosend.Add(split);
            }
            Task[] tasks = new Task[tosend.Count];
            for (int i = 0;i < tosend.Count; i++)
            {
                tasks[i] = service.GetData(tosend[i]);
            }
            Task.WaitAll(tasks);
            return Task.FromResult(0);
        }
    }
}

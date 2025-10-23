using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Part3
    {
        public static void Run()
        {
            List<IChatMember> list = new List<IChatMember>
            {
                new ChatMember("Bob"),
                new ChatMember("Ross"),
                new ChatMember("Mike"),
            };
            ChatMember sender = new("Michael");
            ChatSpace space = new(list);
            sender.Send(space, "Michael: я забыл кота дома.");
        }
    }
    public interface IChatMember
    {
        public string Name { get; }
        public void OnRecieve(string message);
        public void Send(IChatMediator mediator,string message);
    }
    public interface IChatMediator
    {
        public List<IChatMember> Members { get; internal set; }
        public void SendMessageFromMember(string message);
    }
    public class ChatMember (string name) : IChatMember
    {
        public string Name => name;

        public void OnRecieve(string message)
        {
            Console.WriteLine(name+" Принял сообщение! "+message);
        }

        public void Send(IChatMediator chatMediator,string message)
        {
            Console.WriteLine(name+" Отправляет сообщение.");
            chatMediator.SendMessageFromMember(message);
        }
    }
    public class ChatSpace(List<IChatMember> members) : IChatMediator
    {
        public List<IChatMember> Members { get; set; } = members;
        public void SendMessageFromMember(string message)
        {
            for (int i = 0; i < Members.Count; i++)
            {
                IChatMember member = Members[i];
                member.OnRecieve(message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal interface IAlghoritm
    {
        internal abstract object Alghoritm(object[] args);
    }
    internal class Context
    {
        private IAlghoritm contextalghoritm;
        public Context(IAlghoritm alghoritm) => contextalghoritm = alghoritm;
        internal object ExecuteAlghoritm(object[] args)
        {
            return contextalghoritm.Alghoritm(args);
        }
    }
    internal class Add : IAlghoritm
    {
        object IAlghoritm.Alghoritm(object[] args)
        {
            if (args.Length < 2) throw new ArgumentException("Not enough arguments");
            double output = 0;
            output = args.First() is double ? (double)args.First() : 0;
            args[0] = 0d;
            foreach (var item in args)
            {
                if (item is double)
                {
                    output += (double)item;
                }
            }
            return output;
        }
    }
    internal class Substract : IAlghoritm
    {
        object IAlghoritm.Alghoritm(object[] args)
        {
            if (args.Length < 2) throw new ArgumentException("Not enough arguments");
            double output = 0;
            output = args.First() is double ? (double)args.First() : 0;
            args[0] = 0d;
            foreach (var item in args)
            {
                if (item is double)
                {
                    output -= (double)item;
                }
            }
            return output;
        }
    }
    internal class Multiply : IAlghoritm
    {
        object IAlghoritm.Alghoritm(object[] args)
        {
            if (args.Length < 2) throw new ArgumentException("Not enough arguments");
            double output = 0;
            output = args.First() is double ? (double)args.First() : 0;
            args[0] = 1d;
            foreach (var item in args)
            {
                if (item is double)
                {
                    output *= (double)item;
                }
            }
            return output;
        }
    }
}

using System;

namespace Constant
{
    public interface IKeyedConstant<out T> where T : IComparable
    {
        T Key { get; }
    }
}
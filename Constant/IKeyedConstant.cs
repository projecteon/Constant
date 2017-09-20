namespace Constant
{
    using System;

    public interface IKeyedConstant<out T> where T : IComparable
    {
        T Key { get; }
    }
}
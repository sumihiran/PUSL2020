namespace PUSL2020.Application;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class OrderAttribute : Attribute
{
    public OrderAttribute(int order)
    {
        Order = order;
    }
    public int Order { get; }
}
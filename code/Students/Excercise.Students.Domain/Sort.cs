namespace Excercise.Students.Domain
{
    public class Sort
    {
        public const string OrderByAsc = "OrderByAsc";
        public const string OrderByDesc = "OrderByDesc";

        public string TargetField { get; set; }
        public EOrderType OrderType { get; set; }
    }
}
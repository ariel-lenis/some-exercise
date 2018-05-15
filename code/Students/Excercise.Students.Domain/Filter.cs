namespace Excercise.Students.Domain
{
    public class Filter
    {
        public string Key { get; set; }
        public EOperator Operator { get; set; }
        public object Value { get; set; }
    }
}
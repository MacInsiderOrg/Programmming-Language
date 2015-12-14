namespace ProgrammingLanguage
{
    public class Variable
    {
        public dynamic Value { get; set; }
        public string Type { get; set; }

        internal Variable(dynamic value, string type)
        {
            Value = value;
            Type = type;
        } 
    }
}
namespace ProgrammingLanguage
{
    public interface IInterpretator
    {
        object [] Execute(string programText, object [] args);
    }
}
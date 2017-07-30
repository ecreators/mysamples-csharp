namespace mysamples.api.command
{
    public abstract class ParameterCommand<T> : Command
    {
        public T Parameter { get; set; }
    }
}
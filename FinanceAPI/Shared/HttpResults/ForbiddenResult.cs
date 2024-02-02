namespace FinanceAPI.Shared.HttpResults
{
    public class ForbiddenResult<T> : Result<T>
    {
        private readonly string _error;

        public ForbiddenResult(string error)
        {
            _error = error;
        }

        public override ResultType ResultType => ResultType.Forbidden;
        public override List<string> Errors => new List<string> 
        { 
            _error ?? "Your request was unauthorized" 
        };

        public override T Data => default(T);
    }
}

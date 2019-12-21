namespace RuyaTabircim.Api.Model
{
    public enum Level
    {
        Success = 0,
        Error = 1,
        Session = 2,
        MissingData = 3,
        User = 4,
        Validation = 5,
        InputRequest = 6,
        MissingFile = 7
    }

    public class Return
    {
        public object Data { get; set; }
        public string Message { get; private set; }
        public int Result { get; private set; }
        //public string Code { get; private set; }

        public void SetMessage(Level level, string message)
        {
            SetMessage(level, message, null);
        }

        public void SetMessage(Level level, string message, string code)
        {
            Message = message;
            Result = (int)level;
            //Code = code;
        }
    }

    public class ReturnLogin
    {
        public string Token { get; set; }
        public string Message { get; private set; }
        public int Result { get; private set; }
        //public string Code { get; private set; }

        public void SetMessage(Level level, string message)
        {
            SetMessage(level, message, null);
        }

        public void SetMessage(Level level, string message, string code)
        {
            Message = message;
            Result = (int)level;
            //Code = code;
        }
    }
}

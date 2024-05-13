namespace GDE.Core.Communication
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            Erros = new ResponseErrorMessages();
        }

        public string? Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Erros { get; set; }
    }

    public class ResponseErrorMessages
    {
        public ResponseErrorMessages()
        {
            Mensagens = new List<string>();
        }

        public List<string> Mensagens { get; set; }
    }
}

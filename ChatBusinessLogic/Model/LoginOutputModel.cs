namespace ChatBusinessLogic.Model
{
    public class LoginOutputModel
    {
        public string? ReturnMessage { get; set; }
        public bool IsSuccess { get; set; }

        public int UserID {  get; set; }
    }
}

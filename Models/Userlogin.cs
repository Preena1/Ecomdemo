namespace Ecomdemo.Models
{
public class loginModel
  {
    public string? RequestId {get; set;}
    public string? email {get; set;}
    public string? password {get; set;}

  public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
}

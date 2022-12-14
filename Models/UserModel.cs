using System;
namespace Ecomdemo.Models
{
    public class UserModel
    {
        public int userid { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }

        public string? RequestId { get; set; }
    }
}

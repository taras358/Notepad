namespace Notepad.Core.Models.Responses
{
    public class ProfileResponse
    {
        public UserResponse User { get; set; }
        public string ProfileId { get; set; }
        public string ProfileImage { get; set; }
        public double FixedTax { get; set; }
        public double PartialTax { get; set; }
        public double Saving { get; set; }
    }
}

namespace Notepad.Core.Models.Requests
{
    public class ProfileRequest
    {
        public string UserId { get; set; }
        public string ProfileId { get; set; }
        public string ProfileImage { get; set; }
        public double FixedTax { get; set; }
        public double PartialTax { get; set; }
        public double Saving { get; set; }
    }
}

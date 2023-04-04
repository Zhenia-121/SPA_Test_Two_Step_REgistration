using System;
namespace Application.Common.Configurations
{
    public class PasswordRequirements
    {
        public bool RequireDigit { get; set; }

        public int RequiredLength { get; set; }

        public bool RequireNonAlphanumeric { get; set; }

        public bool RequireUppercase { get; set; }

        public bool RequireLowercase { get; set; }

        public string? Regex { get; set; }
    }
}

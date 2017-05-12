using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issueTrack.Common
{
    public class Const
    {
        public const int EmailOrPhoneLength = 64;

        public const int PasswordLength_Max = 16;
        public const int PasswordLength_Min = 8;

        public const int VerificationCodeLength = 12;

        public const int NicknameLength_Max = 32;
        public const int NicknameLength_Min = 4;

        public const int RepositoryNameLength = 64;
        public const int RepositoryDescriptionLength = 2048;

        public const int LoginMethod_SUPERLOGIN = 0;
        public const int LoginMethod_Default = 1;

    }
}
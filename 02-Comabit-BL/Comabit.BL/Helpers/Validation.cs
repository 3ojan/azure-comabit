// <copyright file="Validation.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.Helpers
{
    public static class Validation
    {
        public const string IsEmailValidRegEx = @"^[a-zA-Z0-9_/#'+*~=?$&-]{1}[a-zA-Z0-9_/#'+*~=?$&\.-]*@[a-zA-Z0-9_\.-]*[a-zA-Z0-9-]{1,}\.[a-zA-Z]{2,255}$";
        public const string IsAlphaValidRegEx = @"^[^<>!%;\$:\\\[\]\|\^'0-9]*$";
        public const string IsPostalcodeDeValidRegEx = @"^[0-9]{5}$";
        public const string IsPostalcodeAtValidRegEx = @"^[0-9]{4}$";
        public const string IsPostalcodeDeOrAtValidRegEx = @"^[0-9]{4,5}$";
        public const string IsPhoneValidRegEx = @"^[-0-9+/ ()]*$";
        public const string IsAlphaNumericValidRegEx = @"^[^<>\$\\\[\]\|\^'=]*$";
    }
}
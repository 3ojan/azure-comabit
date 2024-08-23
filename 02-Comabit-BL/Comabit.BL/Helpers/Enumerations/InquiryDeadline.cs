using System.ComponentModel;

namespace Comabit.BL.Helpers.Enumerations
{
    public enum InquiryDeadline
    {
        [Description("aktiv")]
        active,

        [Description("abgelaufen")]
        over,

        [Description("ausstehend")]
        invoke,
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Comabit.DL.Data.Company
{
    public enum CompanyState
    {
        [Description("Nicht bestätigt")]
        NotAccepted,

        [Description("Abgelehnt")]
        Declined,

        [Description("Manuell bestätigt")]
        ConfirmedManually,

        [Description("Automatisch bestätigt")]
        ConfirmedAutomatically
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.DL.Data.Match
{
    public enum OfferState
    {
        [Description("offen")]
        pending,

        [Description("abgelehnt")]
        revoked,

        [Description("beauftragt")]
        ordered,

        [Description("nachbessern")]
        renew
    }
}
using Comabit.DL.Data.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comabit.BL.Match.Dto
{
    public class MessageItem
    {
        public Guid Id { get; set; }

        public MessageType Type { get; set; }

        public string Text { get; set; }

        public Guid FromUser { get; set; }

        public Guid ToUser { get; set; }

        public DateTime CreatedAt { get; set; }

        public MatchItem Match { get; set; }

        public Guid MatchId { get; set; }
    }
}
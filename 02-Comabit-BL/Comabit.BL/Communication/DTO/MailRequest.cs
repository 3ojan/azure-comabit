// <copyright file="MailRequest.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Communication.DTO
{
    public class MailRequest
    {
        public string ToEmail { get; set; }

        public string ToName { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}

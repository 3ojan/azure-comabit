// <copyright file="MessageController.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.UI.Controllers.Api
{
    using Comabit.BL.Message;
    using Comabit.BL.Message.DTO;
    using Comabit.DL.Data.Match;
    using Comabit.UI.Controllers;
    using Comabit.UI.Models.Message;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Users.Infrastructure;

    [AllowAnonymous]
    [Route("api/[controller]")]
    public class MessageController : BaseController
    {
        private readonly MessageManager _messageManager;

        public MessageController(MessageManager messageManager, ILogger<MessageController> logger) : base(logger)
        {
            this._messageManager = messageManager;
        }

        /// <summary>
        /// Liste von Matches, die Nachrichten haben anhand EK / VK GUID + Anzahl ungelesener Nachrichten
        /// </summary>
        [HttpGet("buyer/chats")]
        public async Task<IActionResult> AllMatchChatsForBuyer()
        {
            if (!User.IsInRole(Roles.Buyer) || User.GetCompanyId() == Guid.Empty)
            {
                return new JsonResult(null)
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            var vm = this.Mapper.Map<ICollection<MatchChatViewModel>>(await this._messageManager.GetMatchChatsForBuyer(User.GetCompanyId()));

            return new JsonResult(vm)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// einzelne Message auslesen + Match-Infos
        /// </summary>
        [Route("buyer/chat")]
        [HttpGet("buyer/chat")]
        public async Task<IActionResult> GetMatchChatForBuyer(Guid matchId, Guid messageId)
        {
            // neues ViewModel, das Match-Info + ChatMessageVidewModel enthält

            MatchChatViewModel vm = this.Mapper.Map<MatchChatViewModel>(await this._messageManager.GetMatchChatForBuyer(User.GetCompanyId(), matchId));
            vm.NewestMessage = this.Mapper.Map<ChatMessageViewModel>(await this._messageManager.GetChatMessage(messageId, User.GetUserId()));

            return new JsonResult(vm)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Liste von Matches, die Nachrichten haben anhand EK / VK GUID + Anzahl ungelesener Nachrichten
        /// </summary>
        [HttpGet("seller/chats")]
        public async Task<IActionResult> AllMatchChatsForSeller()
        {
            if (!User.IsInRole(Roles.Seller) || User.GetCompanyId() == Guid.Empty)
            {
                return new JsonResult(null)
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            var vm = this.Mapper.Map<ICollection<MatchChatViewModel>>(await this._messageManager.GetMatchChatsForSeller(User.GetCompanyId()));

            return new JsonResult(vm)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// einzelne Message auslesen + Match-Infos
        /// </summary>
        [HttpGet("seller/chat")]
        public async Task<IActionResult> GetMatchChatForSeller(Guid matchId, Guid messageId)
        {
            // neues ViewModel, das Match-Info + ChatMessageVidewModel enthält

            MatchChatViewModel vm = this.Mapper.Map<MatchChatViewModel>(await this._messageManager.GetMatchChatForSeller(User.GetCompanyId(), matchId));
            vm.NewestMessage = this.Mapper.Map<ChatMessageViewModel>(await this._messageManager.GetChatMessage(messageId, User.GetUserId()));

            return new JsonResult(vm)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// alle Messages für einen Match anhand Match GUID
        /// </summary>
        [Route("match")]
        [HttpGet("match/{matchId}")]
        public async Task<IActionResult> AllMessagesForMatch(Guid matchId)
        {
            // check, ob match zu eigener company gehört, wenn nicht => error
            // current user auslesen und dessen company und dann die messages der "eigenen" company immer rechts darstellen, alle anderen (gegenseite: links)
            // zb "flag", ob "eigene nachricht" oder nicht

            if (false) // TODO: obere Logik einbauen
            {
                return new JsonResult(null)
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            var vm = new List<ChatMessageViewModel>();
            var messageItems = this.Mapper.Map<ICollection<ChatMessageViewModel>>(await this._messageManager.GetChatMessageItems(matchId, User.GetUserId()));
            
            foreach(var messageItem in messageItems)
            {
                await this._messageManager.SetMessageReadForUser(messageItem.Id, User.GetUserId());
            }
            vm.AddRange(messageItems);

            return new JsonResult(vm.OrderBy(m => m.CreatedAt))
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// neue Message hinzufügen, enthält Text, Match GUID
        /// </summary>
        [HttpPost("")]
        public async Task<IActionResult> AddMessage(ChatMessageViewModel vm)
        {
            // user guid ermitteln
            // check, ob match zu eigener company gehört, wenn nicht => error
            // message hinzufügen

            if ((!User.IsInRole(Roles.Buyer) && !User.IsInRole(Roles.Seller)) || User.GetCompanyId() == Guid.Empty)
            {
                return new JsonResult(null)
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            var newChatMessageItem = await this._messageManager.AddUserChatMessage(this.Mapper.Map<ChatMessageItem>(vm), User.GetCompanyId(), User.GetUserId());

            return new JsonResult(this.Mapper.Map<ChatMessageViewModel>(newChatMessageItem))
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Anzahl ungelesener Nachrichten gesamt (System/User) anhand User GUID + Details der neuesten x Messages
        /// </summary>
        [HttpGet("unread")]
        public object UnreadMessages(bool includeDetails = false)
        {
            // user guid hier ermitteln

            return new object();
        }
    }
}
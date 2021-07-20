// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.14.0

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentBot.Bots
{
    public class Bot : ActivityHandler
    {
        Options options;
        public Bot(IConfiguration configuration, IBotServices botservices)
        {
            // connects to QnA Maker endpoint for each turn
            options = new Options(botservices, configuration);
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await options.SendResponseToUser(turnContext, cancellationToken);
            FormData data = new FormData();
            if (string.IsNullOrEmpty(turnContext.Activity.Text))
            {
                data = JsonConvert.DeserializeObject<FormData>(turnContext.Activity.Value.ToString());
                turnContext.Activity.Text = data.Type;
            }
        }




        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    turnContext.Activity.Recipient.Name = "Bot";
                    await options.MainOptions(turnContext, cancellationToken);
                }
            }
        }

    }
}

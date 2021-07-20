using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentBot
{
    public class Options
    {
        private IBotServices botServices;
        private IConfiguration configuration;
        public Options(IBotServices botServices, IConfiguration configuration)
        {
            this.botServices = botServices;
            this.configuration = configuration;
        }
        internal async Task MainOptions(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var card = new HeroCard
            {
                Text = "Please select one of the options below",
                Buttons = new List<CardAction>
{
new CardAction(){ Title = "Enter Your Profile Details", Value = "Enter Your Profile Details", Type = ActionTypes.ImBack },
new CardAction(){ Title = "Login related issues", Value = "Login related issues", Type = ActionTypes.ImBack },
new CardAction(){ Title = "FAQ's about job opoortunities", Value = "FAQ's about job opoortunities", Type = ActionTypes.ImBack }//,
//new CardAction(){ Title = "Search for request status", Value = "Search for request status", Type = ActionTypes.ImBack }



}
            };
            var reply = MessageFactory.Attachment(card.ToAttachment());
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }
        internal async Task SendResponseToUser(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            dynamic replyCard = ""; //card based reply
            var reply = new Activity(); //text based reply
            var attachments = new List<Attachment>();
            reply.Type = ActivityTypes.Message;
            reply.TextFormat = TextFormatTypes.Plain;
            FormData data = new FormData();
            //var luisResult = await botServices.luisRecognizer.RecognizeAsync<LuisHelper>(turnContext, cancellationToken);



            if (string.IsNullOrEmpty(turnContext.Activity.Text))
            {
                data = JsonConvert.DeserializeObject<FormData>(turnContext.Activity.Value.ToString());
                turnContext.Activity.Text = data.Type;
            }



            if (turnContext.Activity.Text.ToLower() == "options" || turnContext.Activity.Text.ToLower() == "main options"
            || turnContext.Activity.Text.ToLower() == "menu" || turnContext.Activity.Text.ToLower() == "main menu")
            {
                var card = new HeroCard
                {
                    Text = "Please select one of the options below",
                    Buttons = new List<CardAction>
                    {
                    new CardAction(){ Title = "Enter Your Profile Details", Value = "Enter Your Profile Details", Type = ActionTypes.ImBack },
                    new CardAction(){ Title = "Login related issues", Value = "Login related issues", Type = ActionTypes.ImBack },
                    new CardAction(){ Title = "FAQ's about job opoortunities", Value = "FAQ's about job opoortunities", Type = ActionTypes.ImBack }
                    }
                };
                var replyNew = MessageFactory.Attachment(card.ToAttachment());
                await turnContext.SendActivityAsync(replyNew, cancellationToken);
                return;
            }
            else if (turnContext.Activity.Text.ToLower() == "new user registration")
            {
                var card = new HeroCard
                {
                    //Text = "Please select one of the options below",
                    Buttons = new List<CardAction>
                    {
                    new CardAction(){ Title = "General Registration Instructions", Value = "General Registration Instructions", Type = ActionTypes.ImBack },
                    new CardAction(){ Title = "How to register to smart portal", Value = "How to register to smart portal", Type = ActionTypes.ImBack },
                    //new CardAction(){ Title = "Can I have multiple users use single registration", Value = "Can I have multiple users use single registration", Type = ActionTypes.ImBack },
                    //new CardAction(){ Title = "What extra information is needed for admin role registration", Value = "What extra information is needed for admin role registration", Type = ActionTypes.OpenUrl },
                    }
                };
                var replyNew = MessageFactory.Attachment(card.ToAttachment());
                await turnContext.SendActivityAsync(replyNew, cancellationToken);
                return;
            }
            else if (turnContext.Activity.Text.ToLower().Trim() == "login related issues")
            {
                var card = new HeroCard
                {
                    //Text = "Please select one of the options below",
                    Buttons = new List<CardAction>
{
new CardAction(){ Title = "How to reset my password", Value = "How to reset my password", Type = ActionTypes.ImBack },
new CardAction(){ Title = "What should I do if I forget my username or password", Value = "https://cioxapps.oktapreview.com/help/login#report-security", Type = ActionTypes.OpenUrl },
new CardAction(){ Title = "Sign-in to your Organization", Value = "https://cioxapps.oktapreview.com/help/login#sign-in", Type = ActionTypes.OpenUrl },
new CardAction(){ Title = "Report a Security Issue", Value = "https://cioxapps.oktapreview.com/help/login#report-issue", Type = ActionTypes.OpenUrl }
}
                };
                var replyNew = MessageFactory.Attachment(card.ToAttachment());
                await turnContext.SendActivityAsync(replyNew, cancellationToken);
                return;
            }
            else if (turnContext.Activity.Text.ToLower().Trim() == "faqs about ciox health")
            {
                var card = new HeroCard
                {
                    Buttons = new List<CardAction>
{
new CardAction(){ Title = "What is FHIR", Value = "What is FHIR", Type = ActionTypes.ImBack },
new CardAction(){ Title = "Why Do We Need FHIR", Value = "Why Do We Need FHIR", Type = ActionTypes.ImBack },
new CardAction(){ Title = "How does FHIR work", Value = "How does FHIR work", Type = ActionTypes.ImBack },
new CardAction(){ Title = "What Is Health Information Management (HIM)", Value = "What Is Health Information Management (HIM)", Type = ActionTypes.ImBack },
new CardAction(){ Title = "How Does HIM Benefit Health Organizations", Value = "How Does HIM Benefit Health Organizations", Type = ActionTypes.ImBack },
new CardAction(){ Title = "How Is the Role of HIM Changing", Value = "How Is the Role of HIM Changing", Type = ActionTypes.ImBack }
}
                };
                var replyNew = MessageFactory.Attachment(card.ToAttachment());
                await turnContext.SendActivityAsync(replyNew, cancellationToken);
                return;
            }
            else if (turnContext.Activity.Text.ToLower().Trim() == "search for request status")
            {
                var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0));
                card.Body.Add(new AdaptiveTextBlock() { Text = "Request Status", Color = AdaptiveTextColor.Default, Size = AdaptiveTextSize.Medium, IsSubtle = true, Separator = true, Wrap = true });
                var columnSet = new AdaptiveColumnSet();
                card.Body.Add(columnSet);



                var column = new AdaptiveColumn() { };
                columnSet.Columns.Add(column);
                column.Items.Add(new AdaptiveTextBlock() { Text = "Enter your request id:", Size = AdaptiveTextSize.Normal });
                column.Items.Add(new AdaptiveTextInput() { Type = "Input.Text", Spacing = AdaptiveSpacing.Small, Id = "SearchRequestInput" });
                var action = new AdaptiveSubmitAction() { Type = "Action.Submit", Title = "Submit", DataJson = "{\"Type\":\"SearchRequestStatus\"}" };
                card.Actions.Add(action);



                var adaptiveCardAttachment = new Attachment()
                {
                    ContentType = "application/vnd.microsoft.card.adaptive",
                    Content = card
                };



                //var attachments = new List<Attachment>();
                var adCard = MessageFactory.Attachment(attachments);
                adCard.Attachments.Add(adaptiveCardAttachment);
                await turnContext.SendActivityAsync(adCard, cancellationToken);
                return;
            }
            else if (turnContext.Activity.Text == "SearchRequestStatus")
            {
                data = JsonConvert.DeserializeObject<FormData>(turnContext.Activity.Value.ToString());
                turnContext.Activity.Text = data.Type;



                var messageText = "";
                if (String.IsNullOrEmpty(data.SearchRequestInput))
                {
                    messageText = "Please enter your request id";
                    await turnContext.SendActivityAsync(messageText);
                    return;
                }
                else
                {
                    //messageText = "Your request status is processed";
                    var results = await botServices.qnaMaker.GetAnswersAsync(turnContext);



                    if (results.Any() && results.First().Score >= 0.5)
                    {
                        if (results.First().Metadata.Any() && results.First().Metadata[0].Name == "link")
                        {
                            reply = MessageFactory.Text(results.First().Answer);
                            reply.Type = ActivityTypes.Message;
                            await turnContext.SendActivityAsync(reply, cancellationToken);
                        }
                        else
                        {
                            reply = MessageFactory.Text(results.First().Answer.Replace("\n\n", "\n"));
                            reply.TextFormat = TextFormatTypes.Plain;
                            await turnContext.SendActivityAsync(reply, cancellationToken);
                        }
                    }



                    //
                    var replyOptions = new Activity();
                    messageText = "Was that helpful?";
                    await turnContext.SendActivityAsync(messageText);
                    replyOptions.Type = ActivityTypes.Message;
                    replyOptions.TextFormat = TextFormatTypes.Plain;
                    replyOptions.SuggestedActions = new SuggestedActions()
                    {
                        Actions = new List<CardAction>()
{
new CardAction(){ Title = "Yes", Value="Yes", Type= ActionTypes.ImBack },
new CardAction(){ Title = "No", Value="No", Type= ActionTypes.ImBack },
}
                    };
                    await turnContext.SendActivityAsync(replyOptions, cancellationToken);



                    return;
                }
            }
            else if (turnContext.Activity.Text.ToLower() == "no")
            {
                reply.Type = ActivityTypes.Message;
                reply.TextFormat = TextFormatTypes.Plain;
                reply.SuggestedActions = new SuggestedActions()
                {
                    Actions = new List<CardAction>()
{
new CardAction(){ Title = "Log a ticket", Value="create incident", Type= ActionTypes.ImBack },
new CardAction(){ Title = "Back to main options", Value="Main options", Type= ActionTypes.ImBack },
}
                };
                await turnContext.SendActivityAsync(reply, cancellationToken);
                return;
            }

            else
            {
                var results = await botServices.qnaMaker.GetAnswersAsync(turnContext);



                if (results.Any() && results.First().Score >= 0.5)
                {
                    if (results.First().Metadata.Any() && results.First().Metadata[0].Name == "link")
                    {
                        reply = MessageFactory.Text(results.First().Answer);
                        reply.Type = ActivityTypes.Message;
                        await turnContext.SendActivityAsync(reply, cancellationToken);
                    }
                    else
                    {
                        reply = MessageFactory.Text(results.First().Answer.Replace("\n\n", "\n"));
                        reply.TextFormat = TextFormatTypes.Plain;
                        await turnContext.SendActivityAsync(reply, cancellationToken);
                    }
                }
                else
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text("I am sorry, I am unable to help you with this currently."), cancellationToken);
                }



            }
        }



    }
    public class FormData
    {
        public string Type { get; set; }
        public string Condition { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string State { get; set; }
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SearchRequestInput { get; set; }
    }
}

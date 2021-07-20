using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentBot
{
    public class BotServices :IBotServices
    {
        public BotServices(IConfiguration configuration) 
        {
            // Read the setting for cognitive services (LUIS, QnA) from the appsettings.json

            qnaMaker = new QnAMaker(new QnAMakerEndpoint
            {
                KnowledgeBaseId = configuration["QnAKnowledgebaseId"],
                EndpointKey = configuration["QnAAuthKey"],
                Host = configuration["QnAEndpointHostName"]
            });
        }



        // public LuisRecognizer luisRecognizer { get; private set; }
        public QnAMaker qnaMaker { get; private set; }
    }
}

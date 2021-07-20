using Microsoft.Bot.Builder.AI.QnA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentBot
{
    public interface IBotServices
    {
        //LuisRecognizer luisRecognizer { get; }
       QnAMaker qnaMaker { get; }

    }
}

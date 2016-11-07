using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotvaSpider.Automation.Mining;
using NUnit.Framework;

namespace BotvaSpider.Tests
{
    [TestFixture]
    public class SearchCristalResultTests
    {
        [Test]
        public void Test()
        {
            var sentences = new[]
                                {
                                    "Неудачная попытка. Может, слабо копал?",
                                    "Неудачная попытка. Может, слабо копал? Сработал ваш кулон.",
                                    
                                    "Ты успешно добыл кристалл! Какой ты молодец! ",       
                                    "Ты успешно добыл кристалл! Какой ты молодец! Сработал ваш кулон.",
                                    "Ты успешно добыл кристалл! Какой ты молодец! Ты нашёл билет на большую поляну!",
                                    "Ты успешно добыл кристалл! Какой ты молодец! Ты нашёл билет на маленькую поляну!",
                                    "Вы добыли 7 крист. Сработал ваш кулон. Ты нашёл билет на большую поляну!",
                           
                                    "Вы добыли 5 крист. Ты нашёл билет на маленькую поляну!",
     
                                };
            var resultrs = new[]
                               {
                                   new SearchCristalResult(),
                                   new SearchCristalResult {CoulombWorks = 1},
                                   new SearchCristalResult {Cristals = 1},
                                   new SearchCristalResult {Cristals = 1,CoulombWorks = 1},
                                   new SearchCristalResult {Cristals = 1,BigTicket = true},
                                   new SearchCristalResult {Cristals = 1,SmallTicket = true},
                                   new SearchCristalResult {Cristals = 7,BigTicket = true,CoulombWorks = 1},
                                   new SearchCristalResult {Cristals = 5,SmallTicket = true},
                               };
            for (var i = 0; i < sentences.Length; i++)
            {
                var result = new SearchCristalResult();
                result.ParseResult(sentences[i]);
                Assert.IsTrue(result.Equals(resultrs[i]), sentences[i]);

            }
        }


    }
}

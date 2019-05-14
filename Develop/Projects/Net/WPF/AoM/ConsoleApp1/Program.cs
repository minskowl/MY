﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ConsoleApp1;
using Newtonsoft.Json;

namespace SheetsQuickstart
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API .NET Quickstart";

        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1Pj85lRxvsw7UQFthPGbtFUIuUCUM16LI8ikxTjitN2M";

            var result = new List<Hero>();
            foreach (var page in GetPages(service, spreadsheetId).Where(e => e.EndsWith("Gear")))
            {
                result.AddRange(GetData(service, spreadsheetId, page));
            }

            File.WriteAllText(@"heroes.json", JsonConvert.SerializeObject(result));
            Console.Read();
        }

        private static IEnumerable<string> GetPages(SheetsService service, string spreadsheetId)
        {
            var request =
                service.Spreadsheets.Get(spreadsheetId);


            var response = request.Execute();
            return response.Sheets.Select(e => e.Properties.Title).ToArray();

        }

        private static List<Hero> GetData(SheetsService service, string spreadsheetId, string page)
        {
            var request = service.Spreadsheets.Values.Get(spreadsheetId, page + "!A2:I");
            var fraction = page.Split('-')[0].Trim();

            ValueRange response = request.Execute();

            var result = new List<Hero>();

            foreach (var row in response.Values)
            {
                if (result.Count == 0)
                {
                    for (int i = 2; i < row.Count; i++)
                    {
                        result.Add(new Hero
                        {
                            Index = i,
                            Name = row[i].ToString(),
                            Fraction = fraction,
                            Gears = new List<Gear>()
                        });
                    }
                }
                else
                {
                    var level = int.Parse(((string)row[0]).Substring(11));
                    var slot = int.Parse(row[1].ToString().Substring(1));
                    foreach (var hero in result)
                    {
                        if(row.Count<=hero.Index) break;
                        var name = row[hero.Index].ToString();
                        if (!string.IsNullOrWhiteSpace(name))
                            hero.Gears.Add(new Gear
                            {
                                Level = level,
                                Slot = slot,
                                Name = name.Trim()
                            });
                    }
                }

            }

            return result;
        }

        private static void GetData1(SheetsService service, string spreadsheetId)
        {
            var request = service.Spreadsheets.Values.BatchGet(spreadsheetId);


            var response = request.Execute();
            var values = response.ValueRanges;


            foreach (var v in values)
            {

            }
        }
    }
}
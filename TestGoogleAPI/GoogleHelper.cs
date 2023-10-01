using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestGoogleAPI
{
    class GoogleHelper
    {
        private readonly string token_;
        private readonly string sheetFileName_;
        private UserCredential credentials;
        private DriveService driveService;
        private SheetsService sheetsService;
        private string sheetFileId;
        private string sheetName_;

        public GoogleHelper(string token, string sheetFileName)
        {
            this.token_ = token;
            this.sheetFileName_ = sheetFileName;
        }

        public string ApplicationName { get; private set; } = "PyxisScientia";
        public string[] Scopes { get; private set; } = new string[]
        {
            DriveService.Scope.Drive,
            SheetsService.Scope.Spreadsheets
        };

        //Создание нового файла GoogkeSheets с заданным именем
        internal void Create(string sheetName)
        {
            var request = this.sheetsService.Spreadsheets.Create(new Google.Apis.Sheets.v4.Data.Spreadsheet()
            {
                Properties = new Google.Apis.Sheets.v4.Data.SpreadsheetProperties()
                {
                    Title = sheetName
                }
            });
            Google.Apis.Sheets.v4.Data.Spreadsheet response = request.Execute();
            this.sheetFileId = response.SpreadsheetId;
        }

        //Получение значения из ячейки
        internal string Get(string cellName)
        {
            var range = this.sheetName_ + "!" + cellName;

            var request = this.sheetsService.Spreadsheets.Values.Get(                
                spreadsheetId: this.sheetFileId,
                range: range);

            var responce = request.Execute();

            return responce.Values?.First()?.First()?.ToString();
        }

        //Запись значений в ячейки
        internal void Set(object cellName, object value)
        {
            var range = this.sheetName_+ "!" + cellName;
            var values = new List<List<object>> { new List<object> { value } };

            var request = this.sheetsService.Spreadsheets.Values.Update(
                new Google.Apis.Sheets.v4.Data.ValueRange { Values = new List<IList<object>>(values) },
                spreadsheetId: this.sheetFileId,
                range: range) ;
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var responce = request.Execute();
        }

        //Подключение к Google Drive и GoogleSheets 
        internal async Task<bool> Start()
        {
            string credentialPath = Path.Combine(Environment.CurrentDirectory, ".credentials", ApplicationName);
            
            using (FileStream stream = new FileStream("C:\\Users\\Julie\\Documents\\Ju\\programming\\Pyxis\\client_secret.json", FileMode.Open, FileAccess.Read))
            {

                this.credentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets: GoogleClientSecrets.FromStream(stream).Secrets,
                scopes: this.Scopes,
                user: "user",
                taskCancellationToken: CancellationToken.None,
                new FileDataStore(credentialPath, true)
                );
            }

            this.driveService = new DriveService(new Google.Apis.Services.BaseClientService.Initializer
            {
                HttpClientInitializer = this.credentials,
                ApplicationName = ApplicationName
            });
            this.sheetsService = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer
            {
                HttpClientInitializer = this.credentials,
                ApplicationName = ApplicationName
            });

            var request = this.driveService.Files.List();
            var resonse = request.Execute();

            foreach ( var file in resonse.Files ) 
            {
                if ( file.Name == this.sheetFileName_ )
                {
                    this.sheetFileId = file.Id;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(this.sheetFileId))
            {
                var sheetRequest = this.sheetsService.Spreadsheets.Get(this.sheetFileId);
                var sheetResponse = sheetRequest.Execute();

                this.sheetName_ = sheetResponse.Sheets[0].Properties.Title;

                return true;
            }

            return false;
        }
    }
}

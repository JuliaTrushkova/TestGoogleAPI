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

        //Подключение к Google Drive и GoogleSheets 
        internal async Task<bool> Start()
        {
            string credentialPath = Path.Combine(Environment.CurrentDirectory, ".credentials", ApplicationName);

            //Ссылка на полученный json в Google Cloud
            using (FileStream stream = new FileStream("C:\\Users\\Julie\\Documents\\Ju\\programming\\Pyxis\\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                //Параметры авторизации (можно прямо указать Client ID и Client Secret)
                this.credentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets: GoogleClientSecrets.FromStream(stream).Secrets,
                scopes: this.Scopes,
                user: "user",
                taskCancellationToken: CancellationToken.None,
                new FileDataStore(credentialPath, true)
                );
            }
            //Создание объектов сервиса для Drive и Sheets API
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

            //Запрос на получение файлов на Drive

            FilesResource.ListRequest request = this.driveService.Files.List();
            //Запуск запроса и получение результата запроса в виде списка файлов
            FileList? response = request.Execute();

            //Получение ID нужного файла по имени файла (тк работа ведется именно по ID файла, а не по имени)
            foreach (Google.Apis.Drive.v3.Data.File? file in response.Files)
            {
                if (file.Name == this.sheetFileName_)
                {
                    this.sheetFileId = file.Id;
                    break;
                }
            }

            //Если ID найден, формируем запрос на получение таблиц из этого файла. 
            if (!string.IsNullOrEmpty(this.sheetFileId))
            {                
                SpreadsheetsResource.GetRequest? sheetRequest = this.sheetsService.Spreadsheets.Get(this.sheetFileId);
                //Запускаем запрос и получаем результат запроса
                
                Google.Apis.Sheets.v4.Data.Spreadsheet sheetResponse = sheetRequest.Execute();
                //Получаем название первой таблицы
                this.sheetName_ = sheetResponse.Sheets[0].Properties.Title;

                return true;
            }

            return false;
        }

        //Создание нового файла GoogleSheets с заданным именем
        internal void Create(string sheetName)
        {            
            SpreadsheetsResource.CreateRequest request = this.sheetsService.Spreadsheets.Create(new Google.Apis.Sheets.v4.Data.Spreadsheet()
            {
                Properties = new Google.Apis.Sheets.v4.Data.SpreadsheetProperties()
                {
                    Title = sheetName  //Задаем имя таблицы через создание объекта свойств
                }
            });
            Google.Apis.Sheets.v4.Data.Spreadsheet response = request.Execute();
            this.sheetFileId = response.SpreadsheetId;
        }

        //Получение значения из ячейки
        internal string Get(string cellName)
        {
            string range = this.sheetName_ + "!" + cellName; 

            //Фомируем запрос на получение данных заданного диапазиона по ID файла
            SpreadsheetsResource.ValuesResource.GetRequest request = this.sheetsService.Spreadsheets.Values.Get(                
                spreadsheetId: this.sheetFileId,
                range: range);

            //Запускаем запрос и получаем результат в виде массива данных ValueRange (List<List<object>> - массив строк из массивов столбцов)
            Google.Apis.Sheets.v4.Data.ValueRange responce = request.Execute();

            //Так как в программе нужно получить значение с одной ячейки, то получаем значение с первого элемента массив строк и потом с первого элемента соответствующего массива столбцов
            return responce.Values?.First()?.First()?.ToString();
        }

        //Запись значений в ячейки
        internal void Set(object cellName, object value)
        {
            string? range = this.sheetName_+ "!" + cellName;
            //Создаем массив строк из массива столбцов с нужными заданными значениями
            List<List<object>>? values = new List<List<object>> { new List<object> { value } };
            
            SpreadsheetsResource.ValuesResource.UpdateRequest? request = this.sheetsService.Spreadsheets.Values.Update(
                new Google.Apis.Sheets.v4.Data.ValueRange { Values = new List<IList<object>>(values) },
                spreadsheetId: this.sheetFileId,
                range: range) ;

            //При записи в таблицу значения будут парситься (USERENTERED). Если .RAW, то запишутся как есть
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

            //Запускаем запрос и получаем результат в виде объекта UpdateValuesResponse, содержащий в себе таблицу обновленных значений, ID таблицы и др.
            Google.Apis.Sheets.v4.Data.UpdateValuesResponse responce = request.Execute();
            
        }

        
    }
}

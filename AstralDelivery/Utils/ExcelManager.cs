using AstralDelivery.Domain.Entities;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;

namespace AstralDelivery.Utils
{
    public static class ExcelManager
    {
        public static MemoryStream Products(IEnumerable<Product> products)
        {
            MemoryStream ms = new MemoryStream();

            using (SpreadsheetDocument spreedDoc = SpreadsheetDocument.Create(ms, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart wbPart = spreedDoc.WorkbookPart;

                wbPart = spreedDoc.AddWorkbookPart();
                wbPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = wbPart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                wbPart.Workbook.AppendChild<Sheets>(new Sheets());

                var sheet = new Sheet()
                {
                    Id = wbPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Products"
                };

                var workingSheet = ((WorksheetPart)wbPart.GetPartById(sheet.Id)).Worksheet;

                //Шапка
                Row row = new Row();
                row.RowIndex = 1;
                row.AppendChild(AddCellWithText("Артикул"));
                row.AppendChild(AddCellWithText("Наименование"));
                row.AppendChild(AddCellWithText("Количество"));
                row.AppendChild(AddCellWithText("Телефон"));
                row.AppendChild(AddCellWithText("Почта"));
                row.AppendChild(AddCellWithText("Цена"));
                row.AppendChild(AddCellWithText("Тип оплаты"));
                row.AppendChild(AddCellWithText("Тип доставки"));
                row.AppendChild(AddCellWithText("Статус доставки"));
                row.AppendChild(AddCellWithText("ФИО Курьера"));
                row.AppendChild(AddCellWithText("Дата и время доставки"));
                row.AppendChild(AddCellWithText("Адрес"));
                sheetData.AppendChild(row);

                //Данные
                int rowindex = 2;
                foreach (var item in products)
                {
                    row = new Row();
                    row.RowIndex = (UInt32)rowindex++;

                    row.AppendChild(AddCellWithText(item.Article));
                    row.AppendChild(AddCellWithText(item.Name));
                    row.AppendChild(AddCellWithText(item.Count.ToString()));
                    row.AppendChild(AddCellWithText(item.Phone));
                    row.AppendChild(AddCellWithText(item.Email));
                    row.AppendChild(AddCellWithText(item.Price.ToString()));
                    row.AppendChild(AddCellWithText(item.PaymentType.ToString()));
                    row.AppendChild(AddCellWithText(item.DeliveryType.ToString()));
                    row.AppendChild(AddCellWithText(item.DeliveryStatus.ToString()));
                    if (item.DeliveryType == DeliveryType.Courier)
                    {
                        row.AppendChild(AddCellWithText(item.Courier.FIO));
                        row.AppendChild(AddCellWithText(item.DateTime.ToString()));
                        row.AppendChild(AddCellWithText(item.Address));
                    }

                    sheetData.AppendChild(row);
                }

                wbPart.Workbook.Sheets.AppendChild(sheet);
                wbPart.Workbook.Save();
            }
            return ms;
        }

        private static Cell AddCellWithText(string text)
        {
            Cell cell = new Cell();
            cell.DataType = CellValues.InlineString;

            InlineString inlineString = new InlineString();
            Text t = new Text(text);
            inlineString.AppendChild(t);

            cell.AppendChild(inlineString);

            return cell;
        }
    }
}

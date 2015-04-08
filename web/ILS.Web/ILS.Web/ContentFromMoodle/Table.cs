using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace ILS.Web.ContentFromMoodle
{
    public class Table
    {
        public Table(string table, string database_Host_User_Pass)
        {
            SelectDataFromTable(table, database_Host_User_Pass);
        }

        //Имена полей в таблице      
        public string[,] fieldTableInStrings;
        //Записи в таблице
        public string[,] tableInStrings;
        //Кол-во записей
        public int countRecords;
        //Кол-во полей
        public int countField;

        /// <summary>
        /// Получает данные из таблицы tableName
        /// </summary>
        /// <param name="tableName"></param>
        private void SelectDataFromTable(string tableName, string D_H_U_P)
        {
            String sSQL = "SELECT * FROM " + tableName;

            String sConnectionString = D_H_U_P;
            //String sConnectionString = "Database=distanceitschool;Data Source=localhost;User Id=zhek;Password=dosSqrt03Den";
            //String sConnectionString = "Database=distanceitschool;Data Source=web.ssau.ru;User Id=distanceitschool;Password=zD6ZCZA";
            //String sConnectionString = "Database=moodle;Data Source=localhost;User Id=zhek;Password=dosSqrt03Den";

            ///в этой переменной будет содержаться рез-тат запроса
            MySqlData.MySqlExecuteData.MyResultData result = new MySqlData.MySqlExecuteData.MyResultData();
            ///выполняем запрос, который возвращает результат
            result = MySqlData.MySqlExecuteData.SqlReturnDataset(sSQL, sConnectionString);
            ///если ошибок нет
            if (result.HasError == false)
            {
                DataView dv = result.ResultData.DefaultView;
                DataTable dt = dv.Table;
                tableInStrings = new string[dt.Rows.Count, dt.Columns.Count];
                fieldTableInStrings = new string[1, dt.Columns.Count];
                countRecords = dt.Rows.Count;
                countField = dt.Columns.Count;

                for (int i = 0; i < dt.Rows.Count + 1; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (i == 0)
                            fieldTableInStrings[0, j] = dt.Columns[j].ToString();
                        else
                            //tableInStrings[i-1,j] = dt.Rows[i-1][j].ToString();
                            tableInStrings[i - 1, j] = dv[i - 1][j].ToString();
                    }
                }
            }
            ///если есть ошибка
            else
            {
                ///показываем ее в MessageBox'е
                //Console.WriteLine(result.ErrorText);
                //System.Windows.Forms.MessageBox.Show(result.ErrorText);
            }
        }
    }
}
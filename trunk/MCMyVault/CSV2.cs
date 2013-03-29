using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
//updated ImportCSV() & ToCSV() 2011-06-30

namespace MCMyVault
{
    class CSV2
    {
        public DataSet DataSetObj = new DataSet();
        public String Name = "";
        public List<String> ColumnHeader = new List<string>();

        public CSV2()
        {
        }

        public CSV2(string name)
        {
            this.Name = name;
            this.DataSetObj.DataSetName = name;
        }

        /// <summary>
        /// Reads XML schema and data into DataSetObj using specified file.
        /// </summary>
        /// <param name="fileName">Full path of file to import.</param>
        /// <returns>XmlReadMode</returns>
        public XmlReadMode ReadXml(string fileName)
        {
            try
            {
                this.DataSetObj.Clear();
                return this.DataSetObj.ReadXml(fileName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Writes the current data for the DataSetObj to the specified file.
        /// </summary>
        /// <param name="fileName">Full path of file to export</param>
        public void WriteXml(string fileName)
        {
            try
            {
                this.DataSetObj.WriteXml(fileName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Browse to a csv fileand import its data into DataSetObj.
        /// </summary>
        public bool ImportCSV()
        {
            bool successful = false;
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.InitialDirectory = Path.GetDirectoryName(Common.gLogFile);//Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                ofd.ShowDialog();

                if (ofd.FileName != string.Empty)
                {
                    successful = true;
                    //string tempcopy = File.ReadAllText(ofd.FileName).Trim();
                    List<String> FileByLineList = new List<string>(File.ReadAllLines(ofd.FileName));
                    string[] seperators = new string[] { "," };
                    this.ColumnHeader.AddRange(FileByLineList[0].Split(seperators, StringSplitOptions.RemoveEmptyEntries));
                    FileByLineList.RemoveAt(0);
                    DataTable DTable = new DataTable();
                    foreach (string columnname in ColumnHeader)
                    {
                        DTable.Columns.Add(columnname.Trim());
                    }

                    foreach (string items in FileByLineList)
                    {
                        //trim
                        string[] s_raw = items.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                        List<String> s = new List<string>();
                        //trim all data
                        foreach (String text in s_raw)
                        {
                            s.Add(text.Trim());
                        }

                        //PlayerClass create
                        //Read into the DataTable
                        DTable.Rows.Add(s.ToArray());
                    }



                    this.DataSetObj.Tables.Add(DTable);
                }
                else
                    successful = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "dataGridView Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return successful;
        }

        /// <summary>
        /// Creates a .csv file from DataSetObj.Tables[0], if none it creates an empty file
        /// </summary>
        /// <param name="newfilename">Fullpath of .csv file</param>
        public void ToCSV(String newfilename)
        {
            if (ColumnHeader.Count <= 0)
            {
                MessageBox.Show("No Column Headers to create CSV file", "No Column Header", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //create the stringbuilder that would hold the data
            StringBuilder sb = new StringBuilder();
            try
            {
                //check if there are columns in the datatable
                if (DataSetObj.Tables[0].Columns.Count != 0)
                {
                    //loop thru each of the columns for headers
                    foreach (DataColumn column in DataSetObj.Tables[0].Columns)
                    {
                        //append the column name followed by the separator
                        sb.Append(column.ColumnName + ',');
                    }
                    //append a carriage return
                    sb.Append("\r\n");

                    //loop thru each row of the datatable
                    int rowcount = 0;
                    foreach (DataRow row in DataSetObj.Tables[0].Rows)
                    {

                        //loop thru each column in the datatable
                        foreach (DataColumn column in DataSetObj.Tables[0].Columns)
                        {
                            //get the value for the row on the specified column
                            // and append the separator
                            if (row[column].ToString().Trim() != "")
                                sb.Append(row[column].ToString().Trim() + ',');
                        }
                        rowcount++;
                        //append a carriage return but don't add if that was the last row to avoid a blank line
                        if (rowcount != DataSetObj.Tables[0].Rows.Count)
                            sb.Append("\r\n");
                    }
                }

                FileStream fs = File.Create(newfilename);
                fs.Close();

                using (StreamWriter sw = new StreamWriter(newfilename))
                {
                    // Add some text to the file.
                    sw.Write(sb.ToString());
                    sw.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reset all values in the CSV2 object.
        /// </summary>
        public void Reset()
        {
            DataSetObj = new DataSet();
            Name = "";
            ColumnHeader = new List<string>();
        }
    }
}

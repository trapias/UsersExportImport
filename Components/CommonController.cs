﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using DotNetNuke.Instrumentation;

namespace forDNN.Modules.UsersExportImport
{
	public class CommonController
	{
		public static string ToXML(DataTable dt)
		{
			System.Xml.XmlDocument objDoc = new System.Xml.XmlDocument();
			System.Xml.XmlElement objRoot = objDoc.CreateElement("users");
			foreach (DataRow dr in dt.Rows)
			{
				System.Xml.XmlElement objUser = objDoc.CreateElement("user");
				foreach (DataColumn dc in dt.Columns)
				{
					System.Xml.XmlAttribute objAttr = objDoc.CreateAttribute(dc.ColumnName);
					objAttr.Value = string.Format("{0}", dr[dc.ColumnName]);
					objUser.Attributes.Append(objAttr);
				}
				objRoot.AppendChild(objUser);
			}
			objDoc.AppendChild(objRoot);
			return objDoc.InnerXml;
		}

		public static string ToCSV(DataTable dt, bool includeHeaderAsFirstRow, string separator)
		{
            //LoggerSource.Instance.GetLogger("forDNN.Modules.UsersExportImport.CommonController").Debug("ToCSV separator=" + separator);

			StringBuilder csvRows = new StringBuilder();
			StringBuilder sb = null;
			int ColumnsCount = dt.Columns.Count;

			if (includeHeaderAsFirstRow)
			{
				sb = new StringBuilder();
				for (int index = 0; index < ColumnsCount; index++)
				{
					if (dt.Columns[index].ColumnName != null)
						sb.Append(dt.Columns[index].ColumnName);

					if (index < ColumnsCount - 1)
						sb.Append(separator);
				}
				csvRows.AppendLine(sb.ToString());
			}

			foreach(DataRow dr in dt.Rows)
			{
				sb = new StringBuilder();
				for (int index = 0; index < ColumnsCount - 1; index++)
				{
					string value = string.Format(string.Format("{0}", dr[index]));
					if (dt.Columns[index].DataType == typeof(String))
					{
						//If double quotes are used in value, ensure each are replaced but 2.
						if (value.IndexOf("\"") >= 0)
							value = value.Replace("\"", "\"\"");

						//If separtor are is in value, ensure it is put in double quotes.
						if (value.IndexOf(separator) >= 0)
							value = "\"" + value + "\"";
					}
					sb.Append(value);

					if (index < ColumnsCount - 1)
						sb.Append(separator);
				}

				csvRows.AppendLine(sb.ToString());
			}
			sb = null;
			return csvRows.ToString();
		}

		public static void ResponseFile(string ContentType, byte[] lstBytes, string FileName)
		{
			HttpResponse objResponse = System.Web.HttpContext.Current.Response;
			objResponse.ClearHeaders();
			objResponse.ClearContent();
			objResponse.ContentType = ContentType;//"application/vnd.ms-excel"
			objResponse.AppendHeader("Content-Length", lstBytes.Length.ToString());
			objResponse.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", FileName));
			objResponse.OutputStream.Write(lstBytes, 0, lstBytes.Length);
			objResponse.Flush();
			objResponse.End();
		}
	}
}
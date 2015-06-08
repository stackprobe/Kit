using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ImgToCsv
{
	public class CsvFileReader : IDisposable
	{
		private StreamReader _sr;

		public CsvFileReader(string file, Encoding encoding)
		{
			_sr = new StreamReader(file, encoding);
		}

		public int ReadChar()
		{
			for (; ; )
			{
				int chr = _sr.Read();

				if (chr == '\r')
					continue;

				return chr;
			}
		}

		public bool _endOfRow;
		public bool _endOfFile;

		public string NextCell()
		{
			StringBuilder buff = new StringBuilder();
			int chr = ReadChar();

			if (chr == '"')
			{
				for (; ; )
				{
					chr = ReadChar();

					if (chr == -1)
						break;

					if (chr == '"')
					{
						chr = ReadChar();

						if (chr != '"')
							break;
					}
					buff.Append((char)chr);
				}
			}
			else
			{
				for (; ; )
				{
					if (chr == -1 || chr == '\n' || chr == ',')
						break;

					buff.Append((char)chr);
					chr = ReadChar();
				}
			}
			_endOfRow = chr == -1 || chr == '\n';
			_endOfFile = chr == -1;

			return buff.ToString();
		}

		public string[] NextRow()
		{
			List<string> row = new List<string>();

			do
			{
				row.Add(NextCell());
			}
			while (_endOfRow == false);

			if (_endOfFile && row.Count == 1 && row[0] == "")
			{
				return null;
			}
			return row.ToArray();
		}

		public string[][] ReadToEnd()
		{
			List<string[]> rows = new List<string[]>();

			for (; ; )
			{
				string[] row = NextRow();

				if (row == null)
					break;

				rows.Add(row);
			}
			return rows.ToArray();
		}

		public string[][] ReadFor(int rowcnt)
		{
			string[][] rows = new string[rowcnt][];

			for (int rowidx = 0; rowidx < rowcnt; rowidx++)
				rows[rowidx] = NextRow();

			return rows;
		}

		public void Dispose()
		{
			this.Close();
		}

		public void Close()
		{
			try
			{
				_sr.Close();
			}
			catch
			{ }
		}
	}

	public class CsvFileWriter : IDisposable
	{
		StreamWriter _sw;

		public CsvFileWriter(string file, Encoding encoding, bool addMode = false)
		{
			_sw = new StreamWriter(file, addMode, encoding);
		}

		public void WriteCell(string cell)
		{
			if (cell.IndexOf('\r') != -1 || cell.IndexOf('\n') != -1 || cell.IndexOf('"') != -1 || cell.IndexOf(',') != -1)
			{
				_sw.Write('"');

				foreach (char chr in cell)
				{
					if (chr == '"')
					{
						_sw.Write('"');
						_sw.Write('"');
					}
					else
						_sw.Write(chr);
				}
				_sw.Write('"');
			}
			else
				_sw.Write(cell);
		}

		public void EndCell()
		{
			_sw.Write(',');
		}

		public void EndRow()
		{
			_sw.Write('\n');
		}

		public void WriteCell(string cell, bool endOfRow)
		{
			WriteCell(cell);

			if (endOfRow)
				EndRow();
			else
				EndCell();
		}

		public void WriteRow(string[] row)
		{
			for (int index = 0; index < row.Length; index++)
			{
				WriteCell(row[index], index == row.Length - 1);
			}
		}

		public void WriteRows(string[][] rows)
		{
			foreach (string[] row in rows)
			{
				WriteRow(row);
			}
		}

		public void Dispose()
		{
			this.Close();
		}

		public void Close()
		{
			try
			{
				_sw.Close();
			}
			catch
			{ }
		}
	}

	public class CsvFile
	{
		private string _file;
		private Encoding _encoding;

		public CsvFile(string file, Encoding encoding)
		{
			_file = file;
			_encoding = encoding;
		}

		public string[][] ReadToEnd()
		{
			using (CsvFileReader cfr = new CsvFileReader(_file, _encoding))
			{
				return cfr.ReadToEnd();
			}
		}

		public void WriteRows(string[][] rows, bool addMode = false)
		{
			using (CsvFileWriter cfw = new CsvFileWriter(_file, _encoding, addMode))
			{
				cfw.WriteRows(rows);
			}
		}

		public static void Trim(string[][] rows)
		{
			for (int rowidx = 0; rowidx < rows.Length; rowidx++)
			{
				List<string> lRow = rows[rowidx].ToList();

				while (1 <= lRow.Count && lRow[lRow.Count - 1] == "")
				{
					lRow.RemoveAt(lRow.Count - 1);
				}
				rows[rowidx] = lRow.ToArray();
			}

			{
				List<string[]> lRows = rows.ToList();

				while (1 <= lRows.Count && lRows[lRows.Count - 1].Length == 0)
				{
					lRows.RemoveAt(lRows.Count - 1);
				}
				rows = lRows.ToArray();
			}
		}
	}
}

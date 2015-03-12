using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace Stock
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			// Ask user the stock prices
			WebClient Client = new WebClient ();

			Console.WriteLine ("Get today's current stock! What stock would you like?");
			var getStock = Console.ReadLine().ToUpper();

			string directory = @"/Users/thanhngo/documents/stockquotes/";
			string dateToday = DateTime.Now.ToString("yyyy-MM-dd+hh:mm"); 
			var URL = "http://download.finance.yahoo.com/d/quotes.csv?s=" + getStock + "&f=sl1d1t1c1hgvbap2";

			string currentURLFileName = directory + getStock + dateToday + ".csv";

			if (!File.Exists (currentURLFileName)) {
				File.Create (currentURLFileName).Close ();
				Client.DownloadFile (URL, currentURLFileName);

			}


			Console.WriteLine ("Would you like the historic date as well? Y/N ");
			var getHistoricDate = Console.ReadLine().ToUpper();


			if (getHistoricDate.Contains ("Y")) {

				var dayx = "0";
				var monthx = "0";

				Console.WriteLine ("From what year would you like your data? Please insert in YYYY form.");
				var getFromYear = Console.ReadLine();

				Console.WriteLine ("To what year would you like your data? Please insert in YYYY format.");
				var getToYear = Console.ReadLine ();

				string historicURL = string.Format("http://ichart.yahoo.com/table.csv?s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&g=d&ignore=.csv", getStock, dayx, monthx, getFromYear, dayx, monthx, getToYear);

				string historicURLFileName = directory + getStock+getFromYear+ "-" +getToYear+ ".csv";
				Console.WriteLine(historicURLFileName);

				if (!File.Exists (historicURLFileName)) {
					File.Create (historicURLFileName).Close ();
					Client.DownloadFile (historicURL, historicURLFileName);
				}
				else{
					Console.WriteLine ("This file already exists, please restart and choose another");
				}


			} else {
				Console.WriteLine ("All finished!");
			}

		}
	}
}

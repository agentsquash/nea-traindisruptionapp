﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using System.Data.SQLite;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
//using SQL

namespace JourneyPlanner
{
	class DataFetcher
	{

		private string darwin_ldb_key = "?accessToken=3be43ffc-b0b8-4e2c-bb24-28060d72e7fb";
		private string darwin_web_loc = "https://nea-nrapi.apphb.com/";

		public DataFetcher()
		{

		}

		public void FetchRouteingGuide()
		{

		}

		/// <summary>
		/// This function is to fetch requested strings from a URL.
		/// </summary>
		/// <param name="requestURL"></param>
		/// <returns></returns>
		private string FetchURL(string requestURL)
		{
			using (var webClient = new System.Net.WebClient())
			{
				return webClient.DownloadString(requestURL);
			}
		}

		public DelayInfo FetchDarwinLDBDelays(string crsDep, string crsArr)
		{
			string requestConstruct = darwin_web_loc + "delays/" + crsArr + "/from/" + crsDep + "/20" + darwin_ldb_key;
			return JsonSerializer.Deserialize<DelayInfo>(FetchURL(requestConstruct));
		}
		/// <summary>
		/// This function is to fetch the fastest service from the OpenLDBWS service. All CRS codes should be verified via VerifyCRS.
		/// </summary>
		/// <param name="crsDep"></param>
		/// <param name="crsArr"></param>
		/// <returns></returns>
		public FastestInfo FetchDarwinLDBFastest(string crsDep, string crsArr)
		{
			string requestConstruct = darwin_web_loc + "fastest/" + crsDep + "/to/" + crsArr + darwin_ldb_key;
			return JsonSerializer.Deserialize<FastestInfo>(FetchURL(requestConstruct));
		}

		/// <summary>
		/// This function is to fetch Darwin live departure data from the OpenLDBWS service.
		/// </summary>
		/// <param name="boardRequested">
		/// Either "dep", "arr", "next" or "all"</param>
		/// <param name="crsDep"></param>
		/// A valid CRS point - should be verified through VerifyCRSCode first.
		/// <returns></returns>
		public BoardInfo FetchDarwinLDBBoard(string boardRequested, string crsDep)
		{
			string requestConstruct = darwin_web_loc + boardRequested + "/" + crsDep + darwin_ldb_key;
			return JsonSerializer.Deserialize<BoardInfo>(FetchURL(requestConstruct));
		}

		public void FetchCRSData(string stationName)
		{

		}

		public bool CheckCRSData(CRSData crs)
		{
			return true;
		}
		public void ConvertRailReferences()
		{
			string ConnString = "Data Source=.\\data.db; Version=3;";

			SQLiteConnection dbconn = new SQLiteConnection(ConnString);
			dbconn.Open();

			SQLiteCommand DeleteStationTable = new SQLiteCommand("DROP TABLE stationdata",dbconn);
			SQLiteCommand CreateStationTable = new SQLiteCommand("CREATE TABLE stationdata (ATCOCode VARCHAR(11), TIPLOC VARCHAR(7), CRSCode VARCHAR(3), stationName VARCHAR(64), ConnTime INT)",dbconn);

			DeleteStationTable.ExecuteNonQuery();
			CreateStationTable.ExecuteNonQuery();

			using (TextFieldParser parser = new TextFieldParser(".\\RailReferences.csv"))
			{
				int rowno = 0;
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");
				while (!parser.EndOfData)
				{
					string[] fields = parser.ReadFields();
					fields[3] = fields[3].Replace("'", "''").Replace(" Rail Station", "");
					if (rowno != 0)
					{

						string addstation = "INSERT INTO stationdata (ATCOCode, TIPLOC, CRSCode, stationName) values ('" + fields[0] + "','" + fields[1] + "','" + fields[2] + "','" + fields[3] + "')";
						SQLiteCommand AddStation = new SQLiteCommand(addstation, dbconn);
						Console.WriteLine("{1}: Adding {0}...", fields[3], rowno);
						AddStation.ExecuteNonQuery();
					}
				}
				Console.WriteLine("Conversion completed! {0} stations changed.",rowno);
			}
			dbconn.Close();
		}
	}
}
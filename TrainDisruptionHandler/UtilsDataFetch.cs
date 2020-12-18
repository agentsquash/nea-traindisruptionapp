﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using System.Data.SQLite;
using Microsoft.VisualBasic.FileIO;

namespace TrainDisruptionHandler
{
	class UtilsDataFetch
	{

		private readonly string darwin_ldb_key = "?accessToken=3be43ffc-b0b8-4e2c-bb24-28060d72e7fb";
		private readonly string darwin_web_loc = "https://nea-nrapi.apphb.com/";

		public UtilsDataFetch()
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
		/// <summary>
		/// This function fetches the Delay Information from the Darwin OpenLDBWS service, and returns it in the DelayInfo class.
		/// </summary>
		/// <param name="crsDep">Verified CRS code for departure station</param>
		/// <param name="crsArr">Verified CRS code for arrival station</param>
		/// <returns></returns>
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

		public void InitialiseConnectionData()
		{
			SQLiteConnection dbconn = UtilsDB.InitialiseDB();
			dbconn.Open();

			SQLiteCommand CreateConnectionTable = new SQLiteCommand("CREATE TABLE IF NOT EXISTS connectiondata (crsCode VARCHAR(3) PRIMARY KEY, connectionType INT, connTime INT, connFrom VARCHAR(2), connTo VARCHAR(3))", dbconn);
			CreateConnectionTable.ExecuteNonQuery();
		}


	}
}
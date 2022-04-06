using System;
using System.Threading;
using V83;

namespace Frut1Cv2
{
	public class Com1C
    {
		public static event Action<string>? OnConnect;
		public static event Action<string>? OnError;

		public static bool IsConnected { get; private set; }
		public static dynamic База1С { get { return _база1С!; } }

		private static string? _connectionString;
		private static COMConnector? _con;
		private static dynamic? _база1С;
		private static string _error = "";

		public static string LastError
		{
			get { return _error; }
			set { _error = value; if (_error.Length > 0) { OnError?.Invoke(_error); } }
		}

		public static void ConnectTo1C(string connectionString)
		{
			_connectionString = connectionString;
			new Thread(Connect).Start();
		}
		private static void Connect()
		{
			if (IsConnected) return;
			try
			{
				_con = new COMConnector();
				_база1С = _con.Connect(_connectionString);
			}
			catch (Exception ex)
			{
				IsConnected = false;
				OnError?.Invoke("Ошибка соединения с сервером 1С.");
				LastError = ex.Message;
				return;
			}
			finally
			{
				IsConnected = true;
				OnConnect?.Invoke("Соединение с базой 1С установлено.");
				OnConnect?.Invoke("start loading data");
			}
		}
	}
}

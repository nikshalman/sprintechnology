#pragma once

#include <NickyGark.Common.HDR.hpp>



namespace NickyGark::CPP::Common
{

	extern Logger LOG;


	class LoggerMdl
	{
	protected:
		std::unique_ptr <FileHandlerMdl> pFile;
		int pFlushCount; 
		int pFlushMax;

		std::unique_ptr <BackupLogMdl> pBackup;

	public:
		std::unique_ptr <LoggerCtr> pController;

		friend LoggerCtr;
	};

	class LoggerCtr
	{
	private:
		typedef std::wstring wstring;
		typedef std::string string;

		LoggerMdl& _mdl;
		void tryBackupExistingLog();
		void tryCreateLogPath(wstring aLogPath);

	public:
		const int ERR_NO_LOG = 3309;
		const int ERR_SET_MODE = 3310;

		const wstring BACKLOG_PATH = L"backlog\\";
		const wstring LOG = L"\\log";
		const wstring EXT_DLL = L".dll";

		LoggerCtr(LoggerMdl& aMdl);


		void backupAndOpen(wstring aLogPath, int aFlushMax);
		void setup(wstring aLogPath, int aFlushMax);
		void close();

		void f(string aMsg); //write to file wothout new line
		void fl(string aMsg); //write to file with new line

		void f(wstring aMsg); //write to file wothout new line
		void fl(wstring aMsg); //write to file with new line


		void wl(string aMsg); //write to file and print to console

	};


	class Logger
	{
	private:
		typedef std::wstring wstring;
		typedef std::string string;


		std::unique_ptr <LoggerMdl> _log;

		wstring _commonLogPath;

	public:
		~Logger()
		{
			if (_log != NULL)
			{
				_log->pController->fl("LOG CLOSED");
				close();
			}
		}

		wstring getCommonLogPath()
		{
			return _commonLogPath;
		}

		void backupAndOpen(wstring aLogPath, wstring aLogFile);
		void backupAndOpen(wstring aLogPath, wstring aLogFile, int aFlushMax);


		void f(string aMsg)
		{
			_log->pController->f(aMsg);
		}

		void fl(string aMsg)
		{
			_log->pController->fl(aMsg);
		}

		void fl(wstring aMsg)
		{
			_log->pController->fl(aMsg);
		}

		void wl(string aMsg)
		{
			_log->pController->wl(aMsg);
		}

		void close()
		{
			_log->pController->close();
		}
	};


}
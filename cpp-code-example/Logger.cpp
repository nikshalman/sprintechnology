#include <NickyGark.Common.SRC.hpp>




namespace NickyGark::CPP::Common
{
	namespace fs = std::filesystem;
	using namespace NickyGark::CPP::Common::CommonUtil;
	using std::endl;


	Logger LOG; //this is global log, mostly used for debug


	LoggerCtr::LoggerCtr(LoggerMdl& aMdl) : _mdl(aMdl)
	{
		_mdl.pController = std::make_unique<LoggerCtr>(*this);
	}


	void Logger::backupAndOpen(wstring aLogPath, wstring aLogFile)
	{
		backupAndOpen(aLogPath, aLogFile, 0);
	}


	void Logger::backupAndOpen(wstring aLogPath, wstring aLogFile, int aFlushMax)
	{
		_commonLogPath = aLogPath;

		_log = Util::create_unique_model<LoggerMdl, LoggerCtr>();
		_log->pController->backupAndOpen(_commonLogPath + aLogFile, aFlushMax);
	}


	void LoggerCtr::setup(wstring aLogPath, int aFlushMax)
	{
		//int ret = _setmode(_fileno(stdout), _O_U16TEXT);
		//if (ret == -1)
		//	throw AppException(ERR_SET_MODE, "Log file path must contain /log/[log file name]: " + utf8_encode(aLogPath));

		_mdl.pFile = Util::create_unique_model<FileHandlerMdl, FileHandlerCtr>();
		_mdl.pFlushMax = aFlushMax;

		tryCreateLogPath(aLogPath);

		_mdl.pFile->pController->setup(aLogPath);

		_mdl.pBackup = Util::create_unique_model<BackupLogMdl, BackupLogCtr>();
	}


	void LoggerCtr::tryCreateLogPath(wstring aLogPath)
	{
		if(aLogPath.find(LOG) == std::string::npos)
			throw AppException(ERR_NO_LOG, "Log file path must contain /log/[log file name]: " + Util::utf8_encode(aLogPath));

		fs::path fullLogPath = aLogPath;
		wstring logPath = fullLogPath.parent_path();

		//try to create log path
		_mdl.pFile->pController->createDirectory(logPath);
	}


	void LoggerCtr::close()
	{
		_mdl.pFile->pController->flush();
		_mdl.pFile->pController->close();
	}


	void LoggerCtr::backupAndOpen(wstring aLogPath, int aFlushMax)
	{
		setup(aLogPath, aFlushMax);

		tryBackupExistingLog();

		_mdl.pFile->pController->createLogFile(); //open log file
	}


	void LoggerCtr::tryBackupExistingLog()
	{
		if (!_mdl.pFile->pController->isExist())
			return; //previous log file doesn't exist

		_mdl.pBackup->pController->backup(_mdl.pFile->getPath(), BACKLOG_PATH);
	}


	void LoggerCtr::f(string aMsg)
	{
		_mdl.pFile->pController->write(aMsg);

		if (_mdl.pFlushCount > _mdl.pFlushMax)
		{
			_mdl.pFlushCount = 0;
			_mdl.pFile->pController->flush();
		}

		_mdl.pFlushCount++;
	}


	void LoggerCtr::f(wstring aMsg)
	{
		_mdl.pFile->pController->write(aMsg);

		if (_mdl.pFlushCount > _mdl.pFlushMax)
		{
			_mdl.pFlushCount = 0;
			_mdl.pFile->pController->flush();
		}

		_mdl.pFlushCount++;
	}


	void LoggerCtr::fl(string aMsg)
	{
		f(aMsg + Const::NL);
	}


	void LoggerCtr::fl(wstring aMsg)
	{
		f(aMsg + Const::WNL);
	}


	void LoggerCtr::wl(string aMsg)
	{
		std::cout << aMsg << endl;

		fl(aMsg);
	}

}
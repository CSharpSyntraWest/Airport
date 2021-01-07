using Airport_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport_Server.Converter
{
    public class ConverterProvider : IConverterProvider
    {
        private LogicDatabaseConverter logicDatabase;
        public LogicDatabaseConverter LogicDatabase
        {
            get
            {
                if (logicDatabase == null)
                {
                    this.logicDatabase = new LogicDatabaseConverter(CommonToDb, LogicCommon);
                }

                return logicDatabase;
            }
        }

        private LogicToCommonConverter logicCommon;
        public LogicToCommonConverter LogicCommon
        {
            get
            {
                if (logicCommon == null)
                {
                    this.logicCommon = new LogicToCommonConverter();
                }

                return logicCommon;
            }
        }

        private CommonToDbConverter commonToDb;
        public CommonToDbConverter CommonToDb
        {
            get
            {
                if (commonToDb == null)
                {
                    this.commonToDb = new CommonToDbConverter();
                }

                return commonToDb;
            }
        }

    }
}

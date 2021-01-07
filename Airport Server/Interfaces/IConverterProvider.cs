using Airport_Server.Services;

namespace Airport_Server.Converter
{
    public interface IConverterProvider
    {
        CommonToDbConverter CommonToDb { get; }
        LogicToCommonConverter LogicCommon { get; }
        LogicDatabaseConverter LogicDatabase { get; }
    }
}
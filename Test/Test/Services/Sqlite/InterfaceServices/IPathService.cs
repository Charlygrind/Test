using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Services.Sqlite.InterfaceServices
{
    public interface IPathService
    {
        string GetDatabasePath(string DBName);
    }
}

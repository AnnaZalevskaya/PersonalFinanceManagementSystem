﻿namespace Accounts.DataAccess.Dapper.Exceptions
{
    public class DatabaseNotFoundException : Exception
    {
        public DatabaseNotFoundException() { }

        public DatabaseNotFoundException(string message) : base(message) { }
    }
}

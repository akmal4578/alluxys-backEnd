using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccessLayer.SqlDependency
{
    public class SubscribeSqlDependency : ISubscribeSqlDependency
    {
        private string _queryToMonitorObject = string.Empty;
        private string _queryToReturnResult = string.Empty;
        private string _connectionString = string.Empty;
        private CommandType _commandType;
        private List<SqlParameter> _parameters;
        private DataTable _oldDataTable;
        private bool IsQueryToReturnResult = false;
        private bool IsSqlDependencyStart = false;
        private object getSqlDependencyObj = null;

        IConfigurationRoot configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        public EventHandler<SqlDependencyEventChangedArgs> SqlDependencyOnChangedEventHandler;
        public EventHandler<SqlDependencyOnErrorArgs> SqlDependencyOnErrorEventHandler;

        public SubscribeSqlDependency()
        {
            this._connectionString = ConfigurationExtensions.GetConnectionString(configBuilder, "Default");
            this._queryToMonitorObject = string.Empty;
            this._queryToReturnResult = string.Empty;
            this._commandType = CommandType.Text;
            this._parameters = null;
            IsQueryToReturnResult = false;
        }

        public SubscribeSqlDependency(string queryToMonitorObject)
        {
            this._connectionString = ConfigurationExtensions.GetConnectionString(configBuilder, "Default");
            this._queryToMonitorObject = queryToMonitorObject;
            this._queryToReturnResult = queryToMonitorObject;
            this._commandType = CommandType.Text;
            this._parameters = null;
            IsQueryToReturnResult = false;
        }

        public SubscribeSqlDependency(string queryToMonitorObject, string queryToReturnResult, CommandType commandType, List<SqlParameter> parameters)
        {
            this._connectionString = ConfigurationExtensions.GetConnectionString(configBuilder, "Default");
            this._queryToMonitorObject = queryToMonitorObject;
            this._queryToReturnResult = queryToReturnResult;
            this._commandType = commandType;
            this._parameters = parameters;
            IsQueryToReturnResult = true;
        }

        public void Invoke()
        {
            try
            {
                _oldDataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {

                    using (SqlCommand command = new SqlCommand(_queryToMonitorObject, connection))
                    {
                        if (connection.State != System.Data.ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        System.Data.SqlClient.SqlDependency dependency = new System.Data.SqlClient.SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
                        getSqlDependencyObj = dependency;

                        var reader = command.ExecuteReader();

                        if (!IsQueryToReturnResult)
                        {
                            _oldDataTable.Load(reader);
                        }
                        else
                        {
                            _oldDataTable = FetchDataFromDb();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UnsubscribeSqlDependency()
        {
            try
            {
                if (getSqlDependencyObj != null)
                {
                    System.Data.SqlClient.SqlDependency dependency = getSqlDependencyObj as System.Data.SqlClient.SqlDependency;
                    dependency.OnChange -= OnDependencyChange;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool GetIsSqlDependencyStart()
        {
            return IsSqlDependencyStart;
        }

        private void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            try
            {
                // Unsubscribe the event
                System.Data.SqlClient.SqlDependency dependency = sender as System.Data.SqlClient.SqlDependency;
                dependency.OnChange -= OnDependencyChange;

                if (e.Source != SqlNotificationSource.Data)
                {
                    TriggerSqlDependencyEventHandler(SqlDependencyEventHandlers.SqlDependencyOnError, e, string.Empty);

                    if (e.Source == SqlNotificationSource.Timeout)
                        Invoke();
                }
                else
                {
                    TriggerSqlDependencyEventHandler(SqlDependencyEventHandlers.SqlDependencyOnChanged, e, string.Empty);
                }
            }
            catch (Exception ex)
            {
                TriggerSqlDependencyEventHandler(SqlDependencyEventHandlers.SqlDependencyOnError, e, ex.Message);
            }
        }

        private DataTable FetchDataFromDb()
        {
            // FetchDataAfterOnChangeEventFromDB
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    using (SqlCommand command = new SqlCommand(_queryToReturnResult, connection))
                    {
                        command.CommandText = _queryToReturnResult;
                        command.CommandType = _commandType;

                        if (_parameters != null)
                        {
                            for (int i = 0; i < _parameters.Count; i++)
                                command.Parameters.Add(_parameters[i]);
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }

                        command.Parameters.Clear();
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private IEnumerable<DataRow> GetInsertOrUpdateOrDeleteValues(string info, DataTable newDataTable, DataTable oldDataTable)
        {
            IEnumerable<DataRow> dataTable = null;

            try
            {
                switch (info.ToUpper())
                {
                    case "INSERT":
                    case "UPDATE":
                        dataTable = newDataTable.AsEnumerable().Except(oldDataTable.AsEnumerable(), DataRowComparer.Default);
                        break;
                    case "DELETE":
                        dataTable = oldDataTable.AsEnumerable().Except(newDataTable.AsEnumerable(), DataRowComparer.Default);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return dataTable;
        }

        public DataTable GetDataBeforeOnChangeValue()
        {
            DataTable dataTable = _oldDataTable;
            return dataTable;
        }

        public bool IsServiceBrokerEnabled()
        {
            DataTable dataTable = new DataTable();
            string queryToCheckServiceBrokerStatus = string.Empty;
            bool IsServiceBrokerEnabled = false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();
                    queryToCheckServiceBrokerStatus = $"SELECT * FROM sys.databases WHERE name = '{connection.Database}'";

                    using (SqlCommand command = new SqlCommand(queryToCheckServiceBrokerStatus, connection))
                    {
                        command.CommandText = queryToCheckServiceBrokerStatus;
                        command.CommandType = CommandType.Text;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                            IsServiceBrokerEnabled = Convert.ToBoolean(dataTable.AsEnumerable().SingleOrDefault()["is_broker_enabled"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    IsServiceBrokerEnabled = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return IsServiceBrokerEnabled;
        }

        public bool StartServiceBroker()
        {
            string SERVICE_BROKER_QUERY = string.Empty;

            DataTable serviceBrokerTable = new DataTable();
            bool IsServiceBrokerEnabled = false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();

                    SERVICE_BROKER_QUERY = $"BEGIN TRY alter database [{connection.Database}] set single_user WITH ROLLBACK IMMEDIATE;alter database [{connection.Database}] set ENABLE_BROKER;alter database [{connection.Database}] set multi_user WITH ROLLBACK IMMEDIATE; END TRY BEGIN CATCH SELECT ERROR_MESSAGE(); END CATCH;";

                    using (SqlCommand command = new SqlCommand(SERVICE_BROKER_QUERY, connection))
                    {
                        command.CommandText = SERVICE_BROKER_QUERY;
                        command.CommandType = CommandType.Text;
                        var result = command.ExecuteScalar();
                        IsServiceBrokerEnabled = (result == null) ? true : false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    IsServiceBrokerEnabled = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return IsServiceBrokerEnabled;
        }

        private void TriggerSqlDependencyEventHandler(SqlDependencyEventHandlers sqlDependencyEventHandlers, SqlNotificationEventArgs e, string exception)
        {
            try
            {
                string sqlNotificationInfo = string.Empty;
                string sqlNotificationSource = string.Empty;
                string sqlNotificationType = string.Empty;

                if (e != null)
                {
                    sqlNotificationInfo = e.Info.ToString().ToUpper();
                    sqlNotificationSource = e.Source.ToString().ToUpper();
                    sqlNotificationType = e.Type.ToString().ToUpper();
                }

                switch (sqlDependencyEventHandlers)
                {
                    case SqlDependencyEventHandlers.SqlDependencyOnError:
                        if (SqlDependencyOnErrorEventHandler != null)
                        {
                            SqlDependencyOnErrorEventHandler(this, new SqlDependencyOnErrorArgs
                            {
                                IsError = (string.IsNullOrEmpty(sqlNotificationSource)) ? true : (e.Source == SqlNotificationSource.Timeout) ? false : true,
                                IsTimeout = (string.IsNullOrEmpty(sqlNotificationSource)) ? false : (e.Source == SqlNotificationSource.Timeout) ? true : false,
                                SqlNotificationInfo = (string.IsNullOrEmpty(sqlNotificationInfo)) ? "NO INFO" : sqlNotificationInfo,
                                SqlNotificationSource = (string.IsNullOrEmpty(sqlNotificationSource)) ? "NO SOURCE" : sqlNotificationSource,
                                SqlNotificationType = (string.IsNullOrEmpty(sqlNotificationType)) ? "NO TYPE" : sqlNotificationType,
                                ErrorMessage = (string.IsNullOrEmpty(exception)) ? "No Error Message (Due to error is not handle by CATCH exception)" : exception,
                            });
                        }
                        break;
                    case SqlDependencyEventHandlers.SqlDependencyOnChanged:

                        var newDataTable = FetchDataFromDb();

                        if (SqlDependencyOnChangedEventHandler != null)
                        {
                            var onChangedEventDataRows = GetInsertOrUpdateOrDeleteValues(sqlNotificationInfo, newDataTable, _oldDataTable);

                            SqlDependencyOnChangedEventHandler(this, new SqlDependencyEventChangedArgs
                            {
                                NewResults = newDataTable,
                                OldResults = _oldDataTable,
                                SqlNotificationInfo = (string.IsNullOrEmpty(sqlNotificationInfo)) ? "NO INFO" : sqlNotificationInfo,
                                SqlNotificationSource = (string.IsNullOrEmpty(sqlNotificationSource)) ? "NO SOURCE" : sqlNotificationSource,
                                SqlNotificationType = (string.IsNullOrEmpty(sqlNotificationType)) ? "NO TYPE" : sqlNotificationType,
                                OnChangedEventDataRows = onChangedEventDataRows,
                            });
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public enum SqlDependencyEventHandlers
        {
            SqlDependencyOnChanged,
            SqlDependencyOnError
        }
    }
    public class SqlDependencyEventChangedArgs : EventArgs
    {
        public DataTable NewResults { get; set; }
        public DataTable OldResults { get; set; }
        public string SqlNotificationInfo { get; set; }
        public string SqlNotificationSource { get; set; }
        public string SqlNotificationType { get; set; }
        public IEnumerable<DataRow> OnChangedEventDataRows { get; set; }
    }

    public class SqlDependencyOnErrorArgs : EventArgs
    {
        public bool IsError { get; set; }
        public bool IsTimeout { get; set; }
        public string SqlNotificationInfo { get; set; }
        public string SqlNotificationSource { get; set; }
        public string SqlNotificationType { get; set; }
        public string ErrorMessage { get; set; }
        public object Subscriber { get; set; }
    }
}

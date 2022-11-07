using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Consulta.Func.Ofac.Infra.BancoDados.Extensoes
{
    public class ObjectDB
    {
        /// <summary>
        /// Objeto que representa a conexão com o banco de dados.
        /// </summary>
        private IDbConnection Conexao { get; set; }
        /// <summary>
        /// DataReader com o resultada da execução da consulta no banco de dados. 
        /// </summary>
        private IDataReader Reader { get; set; }
        /// <summary>
        /// Tipo de Retorno
        /// </summary>
        private string ReturnType { get; set; }
        /// <summary>
        /// Determina a operação a ser executada no banco de dados.
        /// </summary>
        private OperationType Operation { get; set; }
        /// <summary>
        /// Resultado da execução do CommandSql.
        /// Quando: 
        /// Operation.Insert : Retornar um valor inteiro que corresponde ao ID gerado.
        /// Operation.Update : Retornar um valor inteiro que corresponde a quantidade de linhas afetadas.
        /// Operation.Select : Retorna um DataTable com o resultado da consulta no banco de dados.
        /// </summary>
        public object Result { get; set; }

        private SqlCommand CommandSql { get; set; }

        /// <summary>
        /// Inicializa o objeto ObjectDB, esse construtor é para rodar em modo assincrono
        /// A ideia é substituir o construtor acima
        /// variavelSemValor somente foi colocada para nao ficar igual ao construtor acima
        /// </summary>
        /// <param name="variavelSemValor"></param>
        /// <param name="procedureName"></param>
        /// <param name="operation"></param>
        /// <param name="connectionString"></param>
        /// <param name="returnType"></param>
        public ObjectDB(string procedureName, OperationType operation, string connectionString, string returnType = null)
        {
            Reader = null;
            Result = null;

            //if (string.IsNullOrEmpty(connectionString))
            //{

            //    Conexao = GetConnectionSql("Data Source=s2p-db-us-h.database.windows.net;Initial Catalog=Safe2Pay-Development;User ID=safe2paydb-app-h;Password=70rPO7Eq9UxQfc3t4DMdV191wd7AGQe9;");
            //}
            //else
            //{
            Conexao = GetConnectionSql(connectionString);
            //}

            CommandSql = GetCommandSql(procedureName);
            CommandSql.CommandType = CommandType.StoredProcedure;
            CommandSql.CommandTimeout = 900;

            Operation = operation;
            ReturnType = returnType;
        }

        /// <summary>
        /// Método responsável por criar uma nova conexão com o banco de dados.
        /// </summary>
        /// <param name="connectionString">String de Conexão.</param>
        /// <returns>IDbConnection com a instancia da conexão.</returns>
        private IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Método responsável por criar uma nova conexão com o banco de dados em modo assincrono
        /// A ideia é substituir o metodo GetConnection
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private SqlConnection GetConnectionSql(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Método responsável por criar um novo comando SQL a ser executado em modo Assincróno
        /// A ideia é substituir o método IDbCommand GetCommand
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        private SqlCommand GetCommandSql(string procedureName)
        {
            return new SqlCommand(procedureName, (SqlConnection)Conexao);
        }

        /// <summary>
        /// Adiciona os parâmetros que serão enviados para a procedure.
        /// </summary>
        /// <param name="name">Nome do parâmetro</param>
        /// <param name="value">Valor do parâmetro.</param>
        /// <param name="parameterDirection">Tipo do parâmetro na procedure.</param>
        public void AddParameter(string name, object value, ParameterDirection? parameterDirection = null)
        {
            if (value != null)
            {
                var param = CreateParameter(name);
                var type = CastType(value.GetType());
                if (type == DbType.String)
                {
                    ((SqlParameter)param).Size = ((string)value).Length > 4000 ? -1 : 4000;
                    // Caso o parametro seja uma string vazia.. Seta a mesma p/ null a partir do GetString()
                    if (string.IsNullOrWhiteSpace((string)value))
                        value = ((string)value).GetString();
                }

                //Caso seja um inteiro verifica se seu valor é zero e se for de fato seta null p a variavel
                if (type == DbType.Int32)
                {
                    value = ((int)value).GetInt();
                }

                param.DbType = type;
                param.Value = value;

                if (parameterDirection.HasValue)
                    param.Direction = parameterDirection.Value;

                CommandSql.Parameters.Add(param);
            }
        }

        /// <summary>
        /// Adiciona os parâmetros que serão enviados para a procedure, utilizar em modo assincrono
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="parameterDirection"></param>
        public async Task AddParameterSql(string name, object value, ParameterDirection? parameterDirection = null)
        {
            if (value != null)
            {
                var param = await CreateParameterSql(name);
                var type = CastType(value.GetType());
                if (type == DbType.String)
                {
                    ((SqlParameter)param).Size = ((string)value).Length > 4000 ? -1 : 4000;
                    // Caso o parametro seja uma string vazia.. Seta a mesma p/ null a partir do GetString()
                    if (string.IsNullOrWhiteSpace((string)value))
                        value = ((string)value).GetString();
                }

                //Caso seja um inteiro verifica se seu valor é zero e se for de fato seta null p a variavel
                if (type == DbType.Int32)
                    value = ((int)value).GetInt();

                param.DbType = type;
                param.Value = value;

                if (parameterDirection.HasValue)
                    param.Direction = parameterDirection.Value;

                CommandSql.Parameters.Add(param);
            }
        }

        /// <summary>
        /// Adiciona os parâmetros que serão enviados para a procedure, utilizar em modo assincrono
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="parameterDirection"></param>
        public async Task AddParameterSqlTableValue<T>(string name, string type, List<T> data)
        {
            if (data != null && data.Any())
            {
                var param = (SqlParameter)CreateParameter(name);

                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = type;
                param.Value = await ConvertToDatatable(data);

                CommandSql.Parameters.Add(param);
            }
        }

        /// <summary>
        /// Adiciona os parâmetros que serão enviados para a procedure.
        /// </summary>
        /// <param name="name">Nome do parâmetro</param>
        /// <param name="value">Valor do parâmetro.</param>
        public void AddParameter(string name, byte[] value)
        {
            if (value != null)
            {
                var param = (SqlParameter)CreateParameter(name);
                param.SqlDbType = SqlDbType.Binary;
                param.Value = value;

                CommandSql.Parameters.Add(param);
            }
        }

        /// <summary>
        /// Adiciona parâmetros com SqlDbType.Time.
        /// </summary>
        /// <param name="name">Nome do parâmetro.</param>
        /// <param name="hour">Timespan com o horário parâmetro para procedure.</param>
        public void AddParameter(string name, TimeSpan? hour)
        {
            var param = (SqlParameter)CreateParameter(name);

            param.SqlDbType = SqlDbType.Time;
            param.Value = hour;

            CommandSql.Parameters.Add(param);
        }

        /// <summary>
        /// Cria a estrutura básica do parâmetro da procedure.
        /// </summary>
        /// <param name="name">Nome do parâmetro.</param>
        /// <returns>IDataParameter com os dados básicos do parâmetro.</returns>
        private IDataParameter CreateParameter(string name)
        {
            var param = (SqlParameter)CommandSql.CreateParameter();

            param.ParameterName = name;
            param.Size = 4000;

            return param;
        }

        /// <summary>
        /// Cria a estrutura básica do parâmetro da procedure, para usar de forma assincrona
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private async Task<SqlParameter> CreateParameterSql(string name)
        {
            SqlParameter param = (SqlParameter)CommandSql.CreateParameter();

            param.ParameterName = name;
            param.Size = 4000;

            return await Task.FromResult(param);
        }

        /// <summary>
        /// Adiciona os parâmetros que serão enviados para procedure considerando nome e valor das propriedades da entidade passada como parâmetro.
        /// </summary>
        /// <typeparam name="T">Entidade genérica.</typeparam>
        /// <param name="obj">Objeto que representa a tabela do banco de dados.</param>
        public void AddParameters<T>(T obj)
        {
            obj.GetType().GetProperties().Cast<PropertyInfo>().ToList().ForEach(prop =>
            {
                if (!Equals(prop.GetValue(obj), DBNull.Value))
                {
                    AddParameter(string.Format("@{0}", prop.Name), prop.GetValue(obj));
                }
            });
        }

        /// <summary>
        /// Converte Type para tipoDadoDB.
        /// </summary>
        /// <param name="type">Tipo da propriedade.</param>
        /// <returns>tipoDadoDB</returns>
        private DbType CastType(Type type)
        {
            var ret = DbType.String;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int32:
                    ret = DbType.Int32;
                    break;
                case TypeCode.Decimal:
                    ret = DbType.Decimal;
                    break;
                case TypeCode.Double:
                    ret = DbType.Double;
                    break;
                case TypeCode.DateTime:
                    ret = DbType.DateTime;
                    break;
                case TypeCode.Char:
                case TypeCode.String:
                    ret = DbType.String;
                    break;
                case TypeCode.Boolean:
                    ret = DbType.Boolean;
                    break;
                case TypeCode.Object:
                    ret = DbType.Object;
                    break;

            }

            return ret;
        }

        /// <summary>
        /// Exeucuta as procedures de INSERT and UPDATE no banco de dados.
        /// </summary>
        public void Execute()
        {
            try
            {
                Conexao.Open();

                switch (Operation)
                {
                    case OperationType.Insert:
                        if (string.IsNullOrWhiteSpace(ReturnType))
                            Result = Convert.ToInt32(CommandSql.ExecuteScalar());
                        else
                            Result = Convert.ToString(CommandSql.ExecuteScalar());
                        GetResultOutput();
                        break;
                    case OperationType.Update:
                    case OperationType.Delete:
                        Result = Convert.ToInt32(CommandSql.ExecuteNonQuery());
                        GetResultOutput();
                        break;
                    case OperationType.Select:
                        var ret = CommandSql.ExecuteReader();
                        if (!GetResultReturnValue())
                        {
                            if (ret != null)
                            {
                                var dt = new DataTable();
                                dt.Load(ret);
                                Result = dt;
                            }
                        }


                        break;
                    default:
                        break;
                }
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// Executa as procedures em modo Assincrono, futuramente substituir o public void Execute
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            try
            {
                Conexao.Open();

                switch (Operation)
                {
                    case OperationType.Insert:
                        if (string.IsNullOrWhiteSpace(ReturnType))
                            Result = Convert.ToInt32(await CommandSql.ExecuteScalarAsync());
                        else
                            Result = Convert.ToString(await CommandSql.ExecuteScalarAsync());
                        GetResultOutputSql();
                        break;
                    case OperationType.Update:
                    case OperationType.Delete:
                        Result = Convert.ToInt32(await CommandSql.ExecuteNonQueryAsync());
                        GetResultOutputSql();
                        break;
                    case OperationType.Select:
                        var ret = await CommandSql.ExecuteReaderAsync();
                        if (ret != null)
                        {
                            DataTable dt = new DataTable();
                            dt.Load(ret);
                            Result = dt;
                        }
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// Busca o resultado do parâmetro OUTPUT da procedure.
        /// </summary>
        private void GetResultOutput()
        {
            var parameters = CommandSql.Parameters.Cast<IDataParameter>().ToList();
            if (parameters != null && parameters.Any(x => x.Direction == ParameterDirection.Output))
            {
                var output = parameters.Where(x => x.Direction == ParameterDirection.Output).FirstOrDefault();
                if (output != null)
                    Result = Convert.ToInt32(output.Value);
            }
        }

        private bool GetResultReturnValue()
        {
            var ret = false;

            var parameters = CommandSql.Parameters.Cast<IDataParameter>().ToList();
            if (parameters != null && parameters.Any(x => x.Direction == ParameterDirection.ReturnValue))
            {
                var output = parameters.Where(x => x.Direction == ParameterDirection.ReturnValue).FirstOrDefault();
                if (output != null)
                {
                    ret = true;
                    Result = output.Value;
                }
            }

            return ret;

        }

        /// <summary>
        /// Busca o resultado do parâmetro OUTPUT da procedure.
        /// </summary>
        private void GetResultOutputSql()
        {
            var parameters = CommandSql.Parameters.Cast<SqlParameter>().ToList();
            if (parameters != null && parameters.Any(x => x.Direction == ParameterDirection.Output))
            {
                var output = parameters.Where(x => x.Direction == ParameterDirection.Output).FirstOrDefault();
                if (output != null)
                    Result = Convert.ToInt32(output.Value);
            }
        }

        /// <summary>
        /// Retorna uma List<DataRow> com o resultado da consulta no banco de dados.
        /// </summary>
        /// <returns>List<DataRow> com os dados encontrados.</returns>
        public List<DataRow> GetRows()
        {
            var rows = new List<DataRow>();

            if (Result != null)
            {
                var dt = (DataTable)Result;
                if (dt.Rows.Count > 0)
                    rows = dt.TableToList();
            }

            return rows;
        }

        /// <summary>
        /// Executa as procedures de consulta no banco de dados.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto que a lista deve retornar.</typeparam>
        public void Execute<T>()
        {
            try
            {
                if (Operation != OperationType.Select)
                    throw new Exception("Erro de execução da query.");

                Conexao.Open();
                Reader = CommandSql.ExecuteReader();
                MapToList<T>();
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// Fecha a conexão com o banco de dados.
        /// </summary>
        public void Close()
        {
            if (Reader != null)
            {
                Reader.Close();
                Reader.Dispose();
            }

            if (Conexao != null)
            {
                Conexao.Close();
                Conexao.Dispose();
            }
        }

        /// <summary>
        /// Realiza o mapeamento do IDataReader para o objeto passado no parâmero T.
        /// <typeparam name="T">Tipo de objeto que a lista deve retornar.</typeparam>
        /// <returns>List<T></returns>
        private void MapToList<T>()
        {
            List<T> list = new List<T>();
            T obj = default(T);

            if (Reader != null)
            {
                while (Reader.Read())
                {
                    obj = Activator.CreateInstance<T>();
                    obj.GetType().GetProperties().Cast<PropertyInfo>().ToList().ForEach(prop =>
                    {
                        if (!Equals(Reader[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, Reader[prop.Name], null);
                        }
                    });

                    list.Add(obj);
                }
            }

            Result = list;
        }

        /// <summary>
        /// Converte a lista em um datatable
        /// </summary>
        /// <typeparam name="T">Tipo da lista</typeparam>
        /// <param name="data">lista</param>
        /// <returns>Retorna um datatbale montado com os valores da lista.</returns>
        private async Task<DataTable> ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }

            return await Task.FromResult(table);
        }
    }
}
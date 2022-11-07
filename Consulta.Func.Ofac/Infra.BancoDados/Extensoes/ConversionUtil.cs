using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Consulta.Func.Ofac.Infra.BancoDados.Extensoes
{
    public static class ConversionUtil
    {
        /// <summary>
        /// Valida se o valor informado é nulo.
        /// </summary>
        /// <param name="value">Valor que será validado.</param>
        /// <returns>True se objeto for null e false se não for.</returns>
        public static bool IsNull(this object value)
        {
            return value == null || value == DBNull.Value;
        }

        /// <summary>
        /// Método genérico de conversão.
        /// </summary>
        /// <typeparam name="T">Type para o qual o valor deve ser convertido.</typeparam>
        /// <param name="value">Valor a ser convertido.</param>
        /// <returns>Valor convertido.</returns>
        public static T To<T>(this object value)
        {
            var type = Nullable.GetUnderlyingType(typeof(T));
            if (IsEnumConvert<T>(type) && value != null && value != DBNull.Value)
                return (T)Enum.ToObject(TypeToCast<T>(type), Convert.ToInt32(value));

            return value.IsNull() ? default(T) : (T)Convert.ChangeType(value, TypeToCast<T>(type));
        }

        /// <summary>
        /// Método responsável por retornar valor da coluna convertida caso a coluna exista no DataTable.
        /// </summary>
        /// <typeparam name="T">Type para o qual o valor deve ser convertido.</typeparam>
        /// <param name="row">DataRow  com os dados.</param>
        /// <param name="coloumnName">Nome da coluna que contém o valor a ser convertido.</param>
        /// <returns>Valor convertido caso existe e valor default do tipo informado caso não exista.</returns>
        public static T Get<T>(this DataRow row, string coloumnName)
        {
            return row.Table.Columns.Contains(coloumnName) ? row[coloumnName].To<T>() : default(T);
        }

        /// <summary>
        /// Método que retorna um int caso o valor seja maior que zero, senão retorna null.
        /// </summary>
        /// <param name="value">Valor inteiro a ser testado.</param>
        /// <returns>Retorna um int se valor maior que zero, senão retorna null.</returns>
        public static int? GetInt(this int value)
        {
            return value > 0 ? value : (int?)null;
        }
        /// <summary>
        /// Método que retorna um int caso o valor seja maior que zero, senão retorna 0.
        /// </summary>
        /// <param name="value">Valor inteiro a ser testado.</param>
        /// <returns>Retorna um int se valor maior que zero, senão retorna 0.</returns>
        public static int GetIntZero(this int value)
        {
            return value > 0 ? value : 0;
        }
        /// <summary>
        /// Método que retorna um decimal caso o valor seja maior que zero, senão retorna null.
        /// </summary>
        /// <param name="value">Valor decimal a ser testado.</param>
        /// <returns>Retorna um decimal se valor maior que zero, senão retorna null.</returns>
        public static decimal? GetDecimal(this decimal value)
        {
            return value > 0 ? value : (decimal?)null;
        }

        /// <summary>
        /// Método que retorna uma string se valor for diferente de vazio e nulo.
        /// </summary>
        /// <param name="value">String a ser testada.</param>
        /// <returns>Retorna uma string se valor for diferente de vazio e nulo.</returns>
        public static string GetString(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
        /// <summary>
        /// Método que retorna uma string usando o método Trim() se valor for diferente de vazio e nulo.
        /// </summary>
        /// <param name="value">String a ser testada.</param>
        /// <returns>Retorna uma string usando o método Trim() se valor for diferente de vazio e nulo.</returns>
        public static string GetStringTrim(this string value)
        {
            return !string.IsNullOrEmpty(value) ? value.Trim() : null;
        }

        /// <summary>
        /// Busca o Type usado para conversão.
        /// </summary>
        /// <typeparam name="T">Type para o qual o valor deve ser convertido.</typeparam>
        /// <param name="valueType">Type do tipo Nullable.</param>
        /// <returns>Type para conversão.</returns>
        private static Type TypeToCast<T>(Type valueType)
        {
            return valueType.IsNull() ? typeof(T) : valueType;
        }

        /// <summary>
        /// Método responsável por converter um DataTable em uma List<DataRow>.
        /// </summary>
        /// <param name="dt">DataTable com os dados.</param>
        /// <returns>Uma List<DataRow>.</DataRow></returns>
        public static List<DataRow> TableToList(this DataTable dt)
        {
            return dt.AsEnumerable().Cast<DataRow>().ToList();
        }

        #region * Double *
        public static string DoubleToString(double valor, int casasDecimais)
        {
            string auxField = "0";
            auxField = valor.ToString("0.00").Replace(",", ".");
            for (int k = 0; k < casasDecimais; k++) auxField = auxField + "0";
            auxField = auxField.Substring(0, auxField.IndexOf(".") + casasDecimais + 1);
            return auxField;
        }

        public static string DoubleToStringSemArredondar(double valor, int casasDecimais)
        {
            string auxField = "0";
            auxField = valor.ToString("F10").Replace(",", ".");
            for (int k = 0; k < casasDecimais; k++) auxField = auxField + "0";
            auxField = auxField.Substring(0, auxField.IndexOf(".") + casasDecimais + 1);
            return auxField;
        }
        #endregion * Double *

        #region * Enum *
        /// <summary>
        /// Valida se é uma conversão de tipos Enum.
        /// </summary>
        /// <typeparam name="T">Type para o qual o valor deve ser convertido.</typeparam>
        /// <param name="valueType">Type do tipo Nullable.</param>
        /// <returns>True - Conversão válida e False - Conversão Inválida.</returns>
        private static bool IsEnumConvert<T>(Type valueType)
        {
            return (!valueType.IsNull() && valueType.IsEnum) || typeof(T).IsEnum;
        }

        #endregion * Enum *

    }
}

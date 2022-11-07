using System;
using System.Runtime.InteropServices;

namespace Consulta.Func.Ofac.Dominio.Comum
{
    public static class Util
    {
        public static bool HasValue(this decimal entry) => entry > 0;
        public static bool HasValue(this string stringEntry) => !string.IsNullOrWhiteSpace(stringEntry);
        public static bool HasValue(this int intEntry) => intEntry > 0;
        public static DateTime ToBrazilTime(this DateTime dateTime) => TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "E. South America Standard Time" : "America/Sao_Paulo");
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;

namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    public class DateTimeUtility
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        public static DateTime currentDateTime;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);

        public static string todaysDateString(string _dtFormatStr)
        {
            return DateTime.Now.ToString(_dtFormatStr);
        }

        public static string expirationDateMmddyyyy(string _dtFormatStr)
        {
            return DateTime.Now.AddYears(1).ToString(_dtFormatStr);
        }

        public static string expirationDateDaysAhead(double _days, string _dtFormatStr)
        {
            return DateTime.Now.AddDays(_days).ToString(_dtFormatStr);
        }

        private static bool setSystemDateTime(string[] _dtTokens)
        {
            SYSTEMTIME st = new SYSTEMTIME();
            st.wMonth = Convert.ToInt16(_dtTokens[0]);
            st.wDay = Convert.ToInt16(_dtTokens[1]);
            st.wYear = Convert.ToInt16(_dtTokens[2]);
            st.wHour = Convert.ToInt16(_dtTokens[3]);
            st.wMinute = Convert.ToInt16(_dtTokens[4]);
            st.wSecond = Convert.ToInt16(_dtTokens[5]);
            bool bSetDT = SetSystemTime(ref st);
            return bSetDT;
        }

        private static string[] tokenizeDateTimeMetadata(string _dateTime)
        {
            string[] _words = { "", "", "", "", "", "" };
            string[] _dtTokens = { "", "", "", "", "", "" };
            _words = _dateTime.Split(new char[] { '-' });
            int count = 0;
            foreach (string _word in _words)
            {
                count += 1;
                if (count == 1) { _dtTokens[0] = _word; }
                if (count == 2) { _dtTokens[1] = _word; }
                if (count == 3) { _dtTokens[2] = _word; }
                if (count == 4) { _dtTokens[3] = _word; }
                if (count == 5) { _dtTokens[4] = _word; }
                if (count == 6) { _dtTokens[5] = _word; }
            }
            return _dtTokens;
        }

        public static string formatCurrentSystemDateTime(string _formatStr)
        {
            return DateTime.Now.ToString(_formatStr);
        }

        public static string stringifyCurrentSystemDateTime()
        {
            return DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss");
        }

        // Just return "mm-dd-yyyy" string - do NO reset system datetime
        public static string getMMddyyyyIntoFutureAddDays(int _days)
        {
            string[] _dtTokens = { "", "", "", "", "", "" };
            currentDateTime = DateTime.Now;
            DateTime _futureDateTime = currentDateTime.AddDays(_days);
            return _futureDateTime.ToString("MM/dd/yyyy");
        }

        // Actually reset system datetime
        public static bool setSystemDateTimeAddDays(int _days)
        {
            // Move system datetime ahead x # of days when test workflow needs system clock to change.
            string[] _dtTokens = { "", "", "", "", "", "" };
            currentDateTime = DateTime.Now;
            DateTime _futureDateTime = currentDateTime.AddDays(_days);
            string _futureDTStr = _futureDateTime.ToString("MM-dd-yyyy-HH-mm-ss");
            _dtTokens = tokenizeDateTimeMetadata(_futureDTStr);
            return setSystemDateTime(_dtTokens);
        }

        public static bool setSystemDateTimeSubtractDays(int _days)
        {
            int _negDays = -_days;
            // Move system datetime ahead x # of days when test workflow needs system clock to change.
            string[] _dtTokens = { "", "", "", "", "", "" };
            currentDateTime = DateTime.Now;
            DateTime _backDateTime = currentDateTime.AddDays(_negDays);
            string _backDTStr = _backDateTime.ToString("MM-dd-yyyy-HH-mm-ss");
            _dtTokens = tokenizeDateTimeMetadata(_backDTStr);
            return setSystemDateTime(_dtTokens);
        }

        public static bool resetSystemDateTime(string _dateTime)
        {
            // Set system datetime back to current datetime set at the moment when I do week-ahead verification action.
            string[] _dtTokens = { "", "", "", "", "", "" };
            _dtTokens = tokenizeDateTimeMetadata(_dateTime);
            return setSystemDateTime(_dtTokens);
        }

    }
}

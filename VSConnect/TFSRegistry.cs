using System;
using Microsoft.Win32;

public static class TFSRegistry
{
    const string userRoot = "HKEY_CURRENT_USER";
    public const string subKey = "TFS_Utility";

    public static void SetRegistryKey(string key, string value)
    {
        Registry.SetValue(userRoot + "\\" + subKey, key, value, RegistryValueKind.String);
    }

    public static string GetRegistryKey(string key)
    {
        var ret = Registry.GetValue(userRoot + "\\" + subKey, key, null);
        if (ret == null) return string.Empty;
        return ret.ToString();
    }

    public static void SetTFSMdbPath(string path)
    {
        SetRegistryKey("MDBPath", path);
    }

    public static string GetTFSMdbPath()
    {
        return GetRegistryKey("MDBPath");
    }

    public static string ReportingWeekEnd
    {
        get
        {

            string val = GetRegistryKey("ReportingWeekEnd");
            if (string.IsNullOrWhiteSpace(val))
            {
                return "Friday";
            }
            return val;
        }
        set
        {
            SetRegistryKey("ReportingWeekEnd", value);
        }
    }

    public static DayOfWeek LastDayOfWeek
    {
        get
        {
            return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), ReportingWeekEnd, true);
        }
    }

    public static DateTime ReleaseStartDate
    {
        get
        {

            string val = GetRegistryKey("ReleaseStartDate");
            if (string.IsNullOrWhiteSpace(val))
            {
                return DateTime.Today;
            }
            return Convert.ToDateTime(val).Date;
        }
        set
        {
            SetRegistryKey("ReleaseStartDate", value.Date.ToString());
        }
    }

    public static DateTime ReleaseEndDate
    {
        get
        {

            string val = GetRegistryKey("ReleaseEndDate");
            if (string.IsNullOrWhiteSpace(val))
            {
                return DateTime.Today;
            }
            return Convert.ToDateTime(val).Date;
        }
        set
        {
            SetRegistryKey("ReleaseEndDate", value.Date.ToString());
        }
    }

    public static string GetLogin()
    {
        string ret = GetRegistryKey("Login");
        return ret;
    }

    public static string GetPassword()
    {
        string ret = GetRegistryKey("Password");
        return ret;
    }

    public static string GetDomain()
    {
        string ret = GetRegistryKey("Domain");
        return ret;
    }

    public static string GetUri()
    {
        string ret = GetRegistryKey("Uri");
        return ret;
    }

    public static void SetLogin(string login)
    {
        SetRegistryKey("Login", login);
    }
    public static void SetPassword(string pwd)
    {
        SetRegistryKey("Password", pwd);
    }
    public static void SetDomain(string domain)
    {
        SetRegistryKey("Domain", domain);
    }

    public static void SetUri(string uri)
    {
        SetRegistryKey("Uri", uri);
    }


    public static int GetDefaultAreaId()
    {
        string areaid = GetRegistryKey("AreaId");
        int ret = 0;
        Int32.TryParse(areaid, out ret);
        if (ret == 0) ret = 482;
        return ret;
    }

    public static void SetDefaultAreaId(int areaId)
    {
        SetRegistryKey("AreaId", areaId.ToString());
    }

    public static int GetTaskManagementParentId()
    {
        string id = GetRegistryKey("TaskParentId");
        int ret = 0;
        Int32.TryParse(id, out ret);
        return ret;
    }

    public static void SetTaskManagementParentId(int id)
    {
        SetRegistryKey("TaskParentId", id.ToString());
    }
}

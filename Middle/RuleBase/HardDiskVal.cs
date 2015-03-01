using System;
using System.Runtime.InteropServices;

internal class HardDiskVal
{
    [DllImport("kernel32.dll")]
    private static extern int GetVolumeInformation(string lpRootPathName, string lpVolumeNameBuffer, int nVolumeNameSize, ref int lpVolumeSerialNumber, int lpMaximumComponentLength, int lpFileSystemFlags, string lpFileSystemNameBuffer, int nFileSystemNameSize);
    public static string HDVal()
    {
        int lpVolumeSerialNumber = 0;
        int lpMaximumComponentLength = 0;
        int lpFileSystemFlags = 0;
        string lpVolumeNameBuffer = null;
        string lpFileSystemNameBuffer = null;
        int num4 = GetVolumeInformation(@"c:\", lpVolumeNameBuffer, 0x100, ref lpVolumeSerialNumber, lpMaximumComponentLength, lpFileSystemFlags, lpFileSystemNameBuffer, 0x100);
        return lpVolumeSerialNumber.ToString();
    }

    public string HDVal(string drvID)
    {
        int lpVolumeSerialNumber = 0;
        int lpMaximumComponentLength = 0;
        int lpFileSystemFlags = 0;
        string lpVolumeNameBuffer = null;
        string lpFileSystemNameBuffer = null;
        int num4 = GetVolumeInformation(drvID + @":\", lpVolumeNameBuffer, 0x100, ref lpVolumeSerialNumber, lpMaximumComponentLength, lpFileSystemFlags, lpFileSystemNameBuffer, 0x100);
        return lpVolumeSerialNumber.ToString();
    }
}


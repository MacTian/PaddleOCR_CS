using System.IO;

namespace CV_app
{
    class DiskSpace
    {
        public static long GetHardDiskSpace(string str_HardDiskName)
        {
            long totalSize = 0;
            str_HardDiskName = str_HardDiskName + ":\\";
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalSize;
                }
            }
            return totalSize;
        }

        public static long GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long freeSpace = 0;
            str_HardDiskName = str_HardDiskName + ":\\";
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace;
                }
            }
            return freeSpace / 1048576;
        }
    }
}
